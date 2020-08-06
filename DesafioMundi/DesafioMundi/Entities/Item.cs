namespace DesafioMundi.Entities
{
    public class Item
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; } 
        public string OrderId { get; set; }
        public Order Order { get; set; }

    }
}
