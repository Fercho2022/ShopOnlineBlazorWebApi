using System.ComponentModel.DataAnnotations.Schema;

namespace ShopOnline.Api.Entities
{
    public class Product
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public string ImageURL { get; set; }

        public decimal Price { get; set; }

        public int Qty { get; set; }

        // Clave foránea que indica la categoría a la que pertenece el producto
        public int CategoryId { get; set; }

        // Propiedad de navegación para acceder a la entidad ProductCategory relacionada
        public ProductCategory ProductCategory { get; set; }
    }
}
