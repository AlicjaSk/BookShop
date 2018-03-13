using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BookShopProject.Models
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AuthorRefId { get; set; }
        [ForeignKey("AuthorRefId")]
        public Author Author { get; set; }
        public double Cost { get; set; }
        public int NumberInWarehouse { get; set; } = 0;


    }
}
