using WebAPI_linq2db.Models;

namespace WebAPI_linq2db.ViewModel;

public class GetBuyerQuery
{
    public Buyer Buyer { get; set; }
    public Address Address { get; set; }
    public IEnumerable<PaymentMethod> PaymentMethods { get; set; } = Enumerable.Empty<PaymentMethod>();
}