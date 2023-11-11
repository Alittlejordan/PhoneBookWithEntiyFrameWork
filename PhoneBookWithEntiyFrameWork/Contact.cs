using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PhoneBookWithEntiyFrameWork
{
    internal class Contact
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string Email { get; set; }

        [Required]
        [RegularExpression(@"^[0-9]+$", ErrorMessage = "Invalid Phone Number")]
        public string PhoneNumber { get; set; }
    }
}
