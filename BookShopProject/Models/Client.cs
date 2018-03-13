using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopProject.Models
{
    public class Client
    {
        [Key]
        public int ClientId { get; set; }
        [Required()]
        public string Name { get; set; }
        [Required()]
        public string Surname { get; set; }
        [Required()]
        public string Street { get; set; }
        [Required(ErrorMessage = "Street number is required")]
        [DisplayName("Street number")]
        public string NumberStreet { get; set; }
        [Required()]
        public string City { get; set; }


    }
}
