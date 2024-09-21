using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShopOnline.Models.Dtos
{
   public class PaymentRequestDto
    {
        public string Title { get; set; }
        public int Quantity { get; set; }
        public decimal Price { get; set; }

       
    }
}
