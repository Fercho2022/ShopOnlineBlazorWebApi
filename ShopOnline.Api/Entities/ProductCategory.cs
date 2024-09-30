namespace ShopOnline.Api.Entities
{
    public class ProductCategory
    {

        public int Id { get; set; }

        public string Name { get; set; }

        public string IconCSS { get; set; }

        // Propiedad de navegación inversa para acceder a los productos de una categoría
        public ICollection<Product> Products { get; set; }
    }
}
