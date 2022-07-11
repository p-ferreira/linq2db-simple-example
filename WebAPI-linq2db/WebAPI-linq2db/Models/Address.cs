namespace WebAPI_linq2db.Models
{
    public class Address
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Street { get; set; } = "";
        public string Province { get; set; } = "";
        public string Country { get; set; } = "";
        public Guid BuyerId { get; set; }
        public Buyer Buyer { get; set; }
    }
}
