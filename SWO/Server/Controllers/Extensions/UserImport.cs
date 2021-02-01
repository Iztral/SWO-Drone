using LumenWorks.Framework.IO.Csv;
using Microsoft.AspNetCore.Components;
using SWO.Models.DataModels;
using SWO.Shared.Models.AuthModels;
using SWO.Shared.Models.ViewModels;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Net.Http;
using System.Threading.Tasks;

namespace SWO.Server.Controllers.Extensions
{
    public static class UserImport
    {
        public static async Task<RegisterResult> ImportUsers(HttpClient _httpClient, Stream fileStream)
        {
            List<MemberViewModel> userList = LoadUserList(fileStream);
            RegisterResult result = new RegisterResult();
            foreach (MemberViewModel user in userList)
            {
                result = await ImportUser(_httpClient, user);
                if (!result.Successful)
                {
                    return result;
                }
            }
            return result;
        }

        private static List<MemberViewModel> LoadUserList(Stream fileStream)
        {
            DataTable csvTable = new DataTable();
            using (CsvReader csvReader = new CsvReader(new StreamReader(fileStream), true))
                csvTable.Load(csvReader);

            List<MemberViewModel> userList = new List<MemberViewModel>();
            foreach (DataRow dataRow in csvTable.Rows)
                userList.Add(ParseUser(dataRow));

            return userList;
        }

        private static MemberViewModel ParseUser(DataRow dataRow)
        {
            MemberViewModel user = new MemberViewModel
            {
                Email = dataRow[2].ToString(),
                Password = dataRow[3].ToString(),
                Name = dataRow[4].ToString(),
                Surname = dataRow[5].ToString(),
                Role = (Role)dataRow[6]
            };
            return user;
        }

        private static async Task<RegisterResult> ImportUser(HttpClient _httpClient, MemberViewModel user)
        {
            var result = await _httpClient.PostJsonAsync<RegisterResult>("api/User", user);
            return result;
        }
    }
}
