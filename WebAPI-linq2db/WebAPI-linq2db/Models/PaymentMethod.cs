namespace WebAPI_linq2db.Models
{
    public class PaymentMethod
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string CardNumber { get; set; } = "";
        public Guid BuyerId { get; set; }
        public Buyer Buyer { get; set; }
    }

    public enum CardType
    {
        Amex,
        Visa,
        MasterCard
    }
}
