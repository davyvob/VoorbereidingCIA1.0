using System.ComponentModel.DataAnnotations;

namespace Howest.Prog.CoinChop.Web.Models
{
    public class CreateGroupInput
    {
        [Required]
        public string Name { get; set; }
    }
}
