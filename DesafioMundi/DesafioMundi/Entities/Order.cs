using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Entities
{
    public class Order
    {
        public string Id { get; set; }
        public string Code { get; set; }
        public string Status { get; set; }

        public string ItemId { get; set; }
        public string ChargeId { get; set; }
        public Charges charge { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}
