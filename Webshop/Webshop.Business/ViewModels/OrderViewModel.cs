using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Webshop.Logic.ViewModels
{
    public class OrderViewModel
    {
        public int Id { get; set; }
        public string OrderReference { get; set; }

        [Required]
        [DataType(DataType.EmailAddress)]
        public string Adress { get; set; }
        [Required]
        [DataType(DataType.PostalCode)]
        public string Postcode { get; set; }
        [Required]
        public string City { get; set; }

        public IEnumerable<CartProductViewModel> products { get; set; }
    }
}
