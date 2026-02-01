using System.ComponentModel.DataAnnotations;

namespace OptionsPattern.Config
{
    public class AccountServiceConfig
    {
        public static string AccountService = "AccountService";
        public string Url { get; set; } = string.Empty;
        public string UserName { get; set; } = string.Empty;

        [MinLength(5, ErrorMessage = "Min length of 5 characters")]
        [MaxLength(200, ErrorMessage = "Max length of 200 characters")]
        public string Password { get; set; } = string.Empty;
    }
 }
 