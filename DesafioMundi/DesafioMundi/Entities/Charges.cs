namespace DesafioMundi.Entities
{
    public class Charges
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public int Amount { get; set; }
        public CreditCard CardId { get; set; }
        public Customer Customer { get; set; }
    }
}