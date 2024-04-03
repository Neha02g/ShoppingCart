using EasyShop.Models;

namespace EasyShop.IRepositories{
    public interface ICartRepository{
        
        Customer FindCustomerByEmail(string email);
        List<Cart> CartData(int customerId);

        Product FindProductById(int productIDd);

        Cart ItemAlreadyPresent(int customerId,int productId);

        void AddItemToCart(Cart cartItem);
        void RemoveItemFromCart(Cart cartItem);

        void ReduceQuantity(Cart cartItem);
        void IncreaseQuantity(Cart cartItem);
    }

}