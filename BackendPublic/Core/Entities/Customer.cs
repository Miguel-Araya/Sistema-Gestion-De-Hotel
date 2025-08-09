using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Entities
{
    public class Customer
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int CustomerID { get; set; }
        public string CustomerName { get; set; }
        public string CustomerLastName { get; set; }
        public string CustomerEmail { get; set; }
        public string CardNumber { get; set; }

        public Customer(string customerName, string customerLastName, string customerEmail, string cardNumber)
        {
            CustomerName = customerName;
            CustomerLastName = customerLastName;
            CustomerEmail = customerEmail;
            CardNumber = cardNumber;
        }
    }
}
