
﻿using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;
namespace ShopOnline.Api.Extensions
{
    public static class ProductExtensions
    {
        public static IEnumerable<ProductDto> ConvertToDto(this IEnumerable<Product> products)
        {

            // Mapear productos a ProductDto incluyendo el nombre de la categoría
            return products.Select(product => new ProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.ProductCategory.Id,
                CategoryName = product.ProductCategory.Name
            }).ToList();

        }

        public static ProductDto ConvertToDto(this Product product)
        {

            // Mapear productos a ProductDto incluyendo el nombre de la categoría
            return new ProductDto()
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                ImageURL = product.ImageURL,
                Price = product.Price,
                Qty = product.Qty,
                CategoryId = product.ProductCategory.Id,
                CategoryName = product.ProductCategory.Name
            };

        }

        public static IEnumerable<CartItemDto> ConvertToDto(this IEnumerable<CartItem> cartItems,
                                                         IEnumerable<Product> products)
        {

            //// Mapear  a IEnumerable<CartItems> a IEnumerable<CartItemDto> incluyendo el nombre de la categoría

            var carttItemDtos = new List<CartItemDto>();

            foreach (var cartItem in cartItems)
            {

                var product = products.FirstOrDefault(p => p.Id == cartItem.ProductId);

                if (product != null)
                {
                    var cartItemDto = new CartItemDto()
                    {
                        Id = cartItem.Id,

                        ProductId = cartItem.ProductId,

                        CartId = cartItem.CartId,

                        ProductName = product.Name,

                        ProductDescription = product.Description,

                        ProductImageURL = product.ImageURL,

                        Price = product.Price,

                        TotalPrice = product.Price * cartItem.Qty,

                        Qty = cartItem.Qty,

                    };

                    carttItemDtos.Add(cartItemDto);

                }

            }

            return carttItemDtos;


        }

        public static CartItemDto ConvertToDto(this CartItem cartItem, Product product)
        {
            return new CartItemDto()
            {
                Id = cartItem.Id,
                ProductId = cartItem.ProductId,
                ProductName = product.Name,
                ProductDescription = product.Description,
                ProductImageURL = product.ImageURL,
                Price = product.Price,
                Qty = cartItem.Qty,
                TotalPrice = cartItem.Qty * product.Price,
            };
        }

        public static IEnumerable<ProductCategoryDto> ConvertToDto(this IEnumerable<ProductCategory> productCategories)
        {
            return productCategories.Select(pc => new ProductCategoryDto
            {
                Id = pc.Id,
                Name = pc.Name,
                IconCSS = pc.IconCSS,

            }).ToList();
        }



    }

}