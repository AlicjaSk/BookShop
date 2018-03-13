using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace BookShopProject.Models
{
    public class Purchase
    {
        public Purchase(Book b, Client c)
        {
            this.BookRefId = b.BookId;
            this.Book = b;

            this.ClientRefId = c.ClientId;
            this.Client = c;
        }

        public Purchase()
        {

        }

        [Key]
        public int PurchaseId { get; set; }
        public int ClientRefId { get; set; }
        [ForeignKey("ClientRefId")]
        public Client Client { get; set; }
        public int BookRefId { get; set; }
        [ForeignKey("BookRefId")]
        public Book Book { get; set; }

    }
}