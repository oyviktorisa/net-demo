using System.ComponentModel.DataAnnotations;

namespace techtest.project.Users.Dto
{
    public class ChangeUserLanguageDto
    {
        [Required]
        public string LanguageName { get; set; }
    }
}