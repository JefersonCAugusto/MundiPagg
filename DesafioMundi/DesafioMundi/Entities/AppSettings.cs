namespace DesafioMundi.Entities
{
    public class AppSettings
    {

        public string Secret { get; set; }
        public int ExpireHours { get; set; }
        public string Issuer { get; set; } 
        public string ValidIn { get; set; }
       
    }
}
