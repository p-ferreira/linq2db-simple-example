namespace WebAPI_linq2db.Commands
{
    public class CreateBuyerCommand
    {        
        public string Name { get; set; } = "";
        public DateTime Birthday { get; set; }
        
        public string Street { get; set; } = "";
        public string Province { get; set; } = "";
        public string Country { get; set; } = "";
        
        public IEnumerable<PaymentMethodDto> PaymentMethods { get; set; } = new List<PaymentMethodDto>();
    }

    public class PaymentMethodDto
    {        
        public string CardNumber { get; set; } = "";
    }   
}