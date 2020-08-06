using System.Collections.Generic;

namespace DesafioMundi.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }
        public string ChargeId { get; set; }
        public Charge Charge { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
