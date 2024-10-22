using AutoMapper;
using Domain.Exceptions;
using Domain.Interfaces;
using Domain.Models;
using Domain.Models.OrderEntities;
using Service.Abstraction;
using Services.Spcefications;
using Shared.OrderModels;
using Address = Domain.Models.OrderEntities.Address;

namespace Services
{
    internal class OrderService(IUniteOfWork uniteOfWork,
        IMapper mapper, IBasketRepository basketRepository)
        : IOrderService
    {
        public async Task<OrderResult> CreateOrderAsync(OrderRequst requst, string userEmail)
        {
            var address = mapper.Map<Address>(requst.ShippingAddress);
            var basket = await basketRepository.GetBasketAsync(requst.BasketId)
                ?? throw new BasketNotFoundException(requst.BasketId);
            var orderItems = new List<OrderItem>();
            foreach (var item in basket.Items)
            {
                var product = await uniteOfWork.GetRepository<Product, int>()
                    .GetAsync(item.Id) ?? throw new ProductNotFoundException(item.Id);
                orderItems.Add(CreateOrderItem(item, product));
            }


            var deliveryMethod = await uniteOfWork.GetRepository<DelveryMethod, int>().GetAsync(requst.DeliveryMethod)

            ?? throw new DelviryMethodNotFoundException(requst.DeliveryMethod);

            var subtotal = orderItems.Sum(item => item.Price * item.Quantity);

            var order = new Order(userEmail, address, orderItems, subtotal, deliveryMethod);

            await uniteOfWork.GetRepository<Order, Guid>()
                .AddAsync(order);

            await uniteOfWork.SaveChangesAsync();

            return mapper.Map<OrderResult>(order);

        }



        private OrderItem CreateOrderItem(BasketItem item, Product product) =>
         new OrderItem(item.Quantity, product.Price,
             new ProductInOrderItem(product.Id, product.Name,
             product.PictureUrl));



        public async Task<IEnumerable<DelveryMethodResult>> GetDelveryMethodsAsync()
        {
            var methods = await uniteOfWork.GetRepository<DelveryMethod, int>().GetAllAsync();
            return mapper.Map<IEnumerable<DelveryMethodResult>>(methods);
        }
        public async Task<OrderResult> GetOrderByIdAsync(Guid id)
        {
            var order = await uniteOfWork.GetRepository<Order, Guid>()

            .GetAsync(new OrderWithIncludeSpecification(id))
            ?? throw new OrderNotfoundException(id);

            return mapper.Map<OrderResult>(source: order);
        }


        public async Task<IEnumerable<OrderResult>> GetOrdersByEmailAsync(string email)
        {
            var orders = await uniteOfWork.GetRepository<Order, Guid>().
            GetAllAsync(new OrderWithIncludeSpecification(email));

            return mapper.Map<IEnumerable<OrderResult>>(orders);
        }

    }
}
