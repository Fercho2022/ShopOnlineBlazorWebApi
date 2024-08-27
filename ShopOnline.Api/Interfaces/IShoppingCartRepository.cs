
﻿using ShopOnline.Api.Entities;
using ShopOnline.Models.Dtos;

namespace ShopOnline.Api.Interfaces
{
    public interface IShoppingCartRepository
    {

        Task<CartItem> AddItem(CartItemToAddDto cartItemToAddDto);

        //Task<CartItem> UpdateQty(int id, CartItemQtyUpdateDto cartItemQtyUpdateDto);

        Task<CartItem> DeleteItem(int id); 

        Task<CartItem> GetItem(int id);

        Task<IEnumerable<CartItem>> GetItems(int userId);

        Task<bool> CartItemExists(int cartId, int productId);

    }

}
