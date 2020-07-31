using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Metadata.Ecma335;
using System.Threading.Tasks;

namespace DesafioMundi.Entities
{
    public class Item
    {
        public string Id { get; set; }
        public int Amount { get; set; }
        public string Description { get; set; }
        public int Quantity { get; set; }
        // public string Categoria { get; set; }
        public string OrderId { get; set; }
        public Order Order { get; set; }

    }
}
