
﻿using Microsoft.EntityFrameworkCore;
using ShopOnline.Api.Data;
using ShopOnline.Api.Entities;
using ShopOnline.Api.Interfaces;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Repository
{
    public class ShoppingCartRepository : IShoppingCartRepository
    {
        private readonly DataContext _context;

        public ShoppingCartRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<bool> CartItemExists(int cartId, int productId)
        {
           return  await _context.CartItems.AnyAsync(ci=>ci.CartId==cartId && ci.ProductId==productId);  
        }

        public async Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto)
        {
            var product = await _context.Products.FirstOrDefaultAsync(p => p.Id == cartItemToAddDto.ProductId);

            if( await CartItemExists(cartItemToAddDto.CartId, cartItemToAddDto.ProductId))
            {

                var existingCartItem = await _context
                    .CartItems
                    .FirstOrDefaultAsync(ci =>ci.CartId == cartItemToAddDto.CartId && ci.ProductId == cartItemToAddDto.ProductId);


                    if (existingCartItem != null)
                     {
                         existingCartItem.Qty += cartItemToAddDto.Qty;
                         await _context.SaveChangesAsync();
                         return existingCartItem;

                      }

            }
            else
            {
                // Si el producto no está en el carrito, agregarlo como un nuevo CartItem
                var cartItem = new CartItem()
                {

                    CartId = cartItemToAddDto.CartId,
                    ProductId = cartItemToAddDto.ProductId,
                    Qty = cartItemToAddDto.Qty,

                };
                // Agregar el nuevo CartItem al contexto y guardar los cambios
                _context.CartItems.Add(cartItem);
                await _context.SaveChangesAsync();
                return cartItem;

            }


            return null; // Este punto no debería alcanzarse, pero es necesario para cumplir con la firma del método
        }

            
        public async Task<CartItem> DeleteItem(int id)
        {
           var cartItem= await _context.CartItems.FindAsync(id);

            if(cartItem!= null){


                _context.CartItems.Remove(cartItem);

                await _context.SaveChangesAsync();

                return cartItem;
            
            }

            return null;
        }

      

        public async Task<IEnumerable<CartItem>> GetItems(int userId)
        {

            // Buscar el carrito asociado al usuario
            var cart =await _context.Carts.FirstOrDefaultAsync(c => c.UserId == userId);

            if (cart == null)
            {
                // Si el carrito no existe, devolver una lista vacía
                return new List<CartItem>();
            }

            // Obtener los CartItems asociados a este carrito

            var cartItems = await _context.CartItems.
                Include(ci=>ci.Product).Where(c => c.CartId == cart.Id)
                .ToListAsync();
            return cartItems;
        }

       

        public async Task<CartItem> GetItem(int id)
        {
           var carItem= await _context.CartItems.Include(ci => ci.Product).FirstOrDefaultAsync(ci => ci.Id == id);

            return carItem;
        }

        public async Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto)
        {
            // Buscar el CartItem por su ID
            var cartItem = await _context.CartItems.FirstOrDefaultAsync(ci => ci.Id == id);

            if (cartItem != null)
            {
                // Actualizar la cantidad
                cartItem.Qty = cartItemQtyUpdateDto.Qty;

                // Guardar los cambios en la base de datos
                await _context.SaveChangesAsync();

                return cartItem;
            }

            return null; // Retornar null si no se encontró el CartItem
        }
    }
}

