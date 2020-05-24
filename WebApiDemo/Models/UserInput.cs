using System.ComponentModel.DataAnnotations;

namespace WebApiDemo.Models
{
    public class UserInput
    {
        [Required]
        public string Name { get; set; }

        [Required]
        public int Age { get; set; }
    }
}
