namespace DesafioMundi.Entities
{
    public class CreditCard
    {
        public string Id { get; set; }
        public string Tag { get; set; }
        public int LestNumbers { get; set; }
        public Customer Customer { get; set; }
    }
}