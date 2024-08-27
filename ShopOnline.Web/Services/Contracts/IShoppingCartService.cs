


using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IShoppingCartService
    {
        Task<IEnumerable<CartItemDto>> GetItems(int userId);

        Task<CartItemDto> GetItem(int id);

        Task<CartItemDto> PostItem(CartItemToAddDto cartItemToAddDto);

        Task<CartItemDto> DeleteItem(int id);


    }
}
