using System.Collections.Generic;

namespace DesafioMundi.Entities
{
    public class Customer
    {
        public string Name { get; set; } 
        public string Id { get; set; }   
        public string Email { get; set; } 
        public string Type { get; set; }  
        public string Gender { get; set; } 
        public string Document { get; set; } 
        public ICollection<Charge> Charges { get; set; }
        public ICollection<CreditCard> CreditCard { get; set; } 
    }

}
