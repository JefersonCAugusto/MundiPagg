namespace DesafioMundi.Entities
{
    public class Charge
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }

        public string OrderId { get; set; }
        public Order Order { get; set; }
        public string CreditCardId { get; set; }
        public CreditCard CreditCard { get; set; }
        public string CustomerId { get; set; }
        public Customer Customer { get; set; }
    }
}