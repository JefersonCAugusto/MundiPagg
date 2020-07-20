namespace DesafioMundi.Entities
{
    public class CreditCard
    {
        public string Number { get; set; }
        public int ExpMonth { get; set; }
        public int ExpYear { get; set; }
        public string CVV { get; set; }
        public string  Id { get; set; }

        public string Brand { get; set; }
        public int LestNumbers { get; set; }  //recupera com os 4 ultimos digitos do cartao
        public string CustomerId { get; set; }      //recupero com o reponse do createCard
        public Customer Customer { get; set; }


    }
}