using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace DesafioMundi.Entities
{
    public class ItemOrder
    {
        public string ItemId { get; set; }
        public Item Item { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }

    }
}
