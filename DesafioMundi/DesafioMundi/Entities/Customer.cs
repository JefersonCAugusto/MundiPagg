using System.Collections.Generic;

namespace DesafioMundi.Entities
{
    public class Customer
    {
        public string Name { get; set; } 
        public string Id { get; set; }  // esse id será o Id fornecido pela API da mundi
        public string Email { get; set; }// dado unico
        public string Type { get; set; } // indivicual ou company (Criar Enum)
        public string Gender { get; set; } //male ou female (criar enum)
        public string Document { get; set; } //CPF ou CNPJ
        public ICollection<CreditCard> creditCard { get; set; } // reelacionamento com cartão
    }
}
