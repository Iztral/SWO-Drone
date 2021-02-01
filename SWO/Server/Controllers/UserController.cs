using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SWO.Models.DataModels;
using SWO.Server.Controllers.Extensions;
using SWO.Server.Data;
using SWO.Shared.Models.AuthModels;
using SWO.Shared.Models.ViewModels;
using SWO.Shared.Resources;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace SWO.Server.Controllers
{
    [Authorize(Roles = "Admin")]
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApplicationDBContext _context;
        private readonly IMapper _mapper;
        private readonly UserManager<IdentityUser> _userManager;
        private readonly HttpClient _httpClient;

        public UserController(ApplicationDBContext context, HttpClient httpClient, IMapper mapper, UserManager<IdentityUser> userManager)
        {
            _httpClient = httpClient;
            _mapper = mapper;
            _context = context;
            _userManager = userManager;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var users = await _context.Members.ToListAsync();
            var userModels = _mapper.Map<List<Member>, List<MemberViewModel>>(users);
            return Ok(userModels);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            var user = await _context.Members.FirstOrDefaultAsync(a => a.ID == id);
            var userModel = _mapper.Map<Member, MemberViewModel>(user);
            return Ok(userModel);
        }

        [HttpPost]
        public async Task<IActionResult> Post(MemberViewModel userModel)
        {
            var identity = await _userManager.FindByNameAsync(userModel.Email);
            if (identity == null)
            {
                var result = await AddUserIdentity(userModel);
                if (result.Succeeded == true)
                {
                    var user = _mapper.Map<MemberViewModel, Member>(userModel);
                    user.IdentityID = _userManager.FindByNameAsync(user.Email).Result.Id;
                    _context.Add(user);
                    await _context.SaveChangesAsync();

                    await AddTrainingPersonel(user);

                    return Ok(new RegisterResult { Successful = true });

                }
                else
                {
                    return Ok(new RegisterResult { Successful = false, Error = ViewResources.Register_Failure });
                }
            }
            else
            {
                return Ok(new RegisterResult { Successful = false, Error = ViewResources.User_Exists });
            }
        }

        [HttpPost("import")]
        public async Task<IActionResult> ImportUsers(IFormFile file)
        {
            try
            {
                await UserImport.ImportUsers(_httpClient, file.OpenReadStream());

                return Ok(new RegisterResult { Successful = true });
            }
            catch
            {
                return Ok(new RegisterResult { Successful = false, Error = ViewResources.Edit_Failure });
            }
        }

        [HttpPut]
        public async Task<IActionResult> Put(MemberViewModel userModel)
        {
            var identity = await _userManager.FindByNameAsync(userModel.Email);
            if (identity == null)
            {
                var result = await EditUserIdentity(userModel);
                if (result.Succeeded == true)
                {
                    var user = _mapper.Map<MemberViewModel, Member>(userModel);
                    _context.Entry(user).State = EntityState.Modified;
                    await _context.SaveChangesAsync();

                    return Ok(new RegisterResult { Successful = true });
                }
                else
                {
                    return Ok(new RegisterResult { Successful = false, Error = ViewResources.Edit_Failure });
                }
            }
            else
            {
                return Ok(new RegisterResult { Successful = false, Error = ViewResources.User_Exists });
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var user = _context.Members.First(x => x.ID == id);
            await DeleteUserIdentity(user);
            _context.Remove(user);
            await _context.SaveChangesAsync();
            return NoContent();
        }

        public async Task<IActionResult> AddTrainingPersonel(Member user)
        {
            switch (user.Role)
            {
                case Role.Trainee:
                    _context.Add(new Trainee
                    {
                        UserID = user.ID
                    });
                    break;
                case Role.Instructor:
                    _context.Add(new Instructor
                    {
                        UserID = user.ID
                    });
                    break;
            }

            await _context.SaveChangesAsync();

            return Ok(user);
        }

        public async Task<IdentityResult> AddUserIdentity(MemberViewModel userModel)
        {
            var newUser = new IdentityUser { UserName = userModel.Email };

            var result = await _userManager.CreateAsync(newUser, userModel.Password);

            if (!result.Succeeded)
                return result;

            result = await _userManager.AddToRoleAsync(newUser, userModel.Role.ToString());

            return result;
        }

        public async Task<IdentityResult> EditUserIdentity(MemberViewModel userModel)
        {
            IdentityResult result = new IdentityResult();

            var user = _context.Members.First(x => x.ID == userModel.ID);
            var identityUser = await _userManager.FindByIdAsync(user.IdentityID);
            _context.Entry(user).State = EntityState.Detached;

            if (!String.Equals(identityUser.UserName, userModel.Email))
            {
                identityUser.UserName = userModel.Email;
            }
            if (!String.IsNullOrEmpty(userModel.Password))
            {
                var token = await _userManager.GeneratePasswordResetTokenAsync(identityUser);
                result = await _userManager.ResetPasswordAsync(identityUser, token, userModel.Password);
                if (!result.Succeeded)
                    return result;
            }
            result = await _userManager.UpdateAsync(identityUser);

            return result;
        }

        public async Task<IdentityResult> DeleteUserIdentity(Member user)
        {
            var userIdentity = await _userManager.FindByNameAsync(user.Email);
            var result = await _userManager.DeleteAsync(userIdentity);
            return result;
        }
    }
}
