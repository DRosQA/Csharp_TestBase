
using System.Globalization;
using System.Reflection;
using CsvHelper;

namespace Csharp_TestBase.Utils
{
    public class UsersHelpers
    {
        private static IEnumerable<User> ReadUsers(string fileName) => CsvReader.FromFile(fileName).GetRecords<User>();
        public static User? GetUserByIndex(string filename, int index = 0) => ReadUsers(filename).ElementAtOrDefault(index);
        public static User? GetUserByProperty(string filename, string value, string propertyName) => ReadUsers(filename).FirstOrDefault(x => x.GetType().GetProperty(propertyName)?.GetValue(x)?.ToString() == value);
        public static User? GetUserByProperty(string filename, int value, string propertyName) => ReadUsers(filename).FirstOrDefault(x => x.GetType().GetProperty(propertyName)?.GetValue(x) as int? == value);

        public static User GetValidUser(string fileName) => GetUserByProperty(fileName, 1, "userExists") ?? throw new Exception("No users found");
        public static User GetIncorrectUserData(string fileName) => GetUserByProperty(fileName, 0, "userExists") ?? throw new Exception("No user data found");
    }

    [CsvRecord]
    public partial class User
    {
        [CsvColumn("login")]
        public string Login { get; set; } = "";
        [CsvColumn("password")]
        public string Password { get; set; } = "";
        [CsvColumn("name")]
        public string Name { get; set; } = "";
        [CsvColumn("correct")]
        public string userExists { get; set; } = "1";
    }

}