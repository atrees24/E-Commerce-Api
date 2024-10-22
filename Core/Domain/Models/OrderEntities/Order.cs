namespace Domain.Models.OrderEntities
{
    public class Order : BaseEntity<Guid>
    {
        public Order()
        {
            
        }
        public Order(string userEmail,Address shippingAddress,
                    ICollection<OrderItem> orderItems,
                   decimal subtotal, DelveryMethod delveryMethod )
        {
            UserEmail = userEmail;
            ShippingAddress = shippingAddress;
            OrderItems = orderItems;
            DelveryMethod = delveryMethod;
            Subtotal = subtotal;

        }

        public string UserEmail { get; set; }
        public Address ShippingAddress { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; } = new List<OrderItem>();
        public OrderPaymentStatus PaymentStatus { get; set; } = OrderPaymentStatus.Pending;
        public DelveryMethod DelveryMethod { get; set; }
        public decimal Subtotal { get; set; }
        public DateTimeOffset OrderDate { get; set; } = DateTimeOffset.Now;
        public string PaymentIntentId { get; set; } = string.Empty;
    }
}
