using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Api.Entities
{
    public class CartItem

    {

        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Qty { get; set; }

        // Propiedad de navegación con anotación de datos
        [ForeignKey("ProductId")]
        public Product Product { get; set; }

    }


}
