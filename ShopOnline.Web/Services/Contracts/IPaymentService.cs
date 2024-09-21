using ShopOnline.Models.Dtos;

namespace ShopOnline.Web.Services.Contracts
{
    public interface IPaymentService
    {
        Task<string> CreatePayment(PaymentRequestDto paymentRequestDto);
    }
}
