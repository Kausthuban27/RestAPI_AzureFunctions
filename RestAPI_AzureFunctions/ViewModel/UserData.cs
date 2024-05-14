using System.ComponentModel.DataAnnotations.Schema;

namespace RestAPI_AzureFunctions.ViewModel
{
    public class UserData
    {
        public string Firstname { get; set; } = null!;
        public string Lastname { get; set; } = null!;
        public string Username { get; set; } = null!;
        public string Password { get; set; } = null!;
    }
}
