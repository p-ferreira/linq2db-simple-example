using LinqToDB;
using Microsoft.AspNetCore.Mvc;
using WebAPI_linq2db.Commands;
using WebAPI_linq2db.Models;
using WebAPI_linq2db.ViewModel;

namespace WebAPI_linq2db.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BuyerController : ControllerBase
    {
        private readonly AppDataConnection _appDataConnection;

        public BuyerController(AppDataConnection appDataConnection)
        {
            _appDataConnection = appDataConnection;
        }

        [HttpGet("method-syntax")]
        public GetBuyerQuery GetBuyerMethodSyntax(Guid id)
        {
            var query = _appDataConnection
                .Buyers
                .Join(_appDataConnection.Addresses,
                    buyer => buyer.Id,
                    address => address.BuyerId,
                    (buyer, address) => new {buyer, address})
                .Single(x => x.buyer.Id == id);

            var buyer = query.buyer;
            var address = query.address;
            var paymentMethods = _appDataConnection
                .PaymentMethods
                .Where(x => x.BuyerId == buyer.Id)
                .AsEnumerable();
            
            return new GetBuyerQuery
            {
                Buyer = buyer,
                Address = address,
                PaymentMethods = paymentMethods
            };
        }
        
        
        [HttpGet("query-syntax")]
        public GetBuyerQuery GetBuyerQuerySyntax(Guid id)
        {
            var query = from buyer in _appDataConnection.Buyers
                join address in _appDataConnection.Addresses on buyer.Id equals address.BuyerId
                where buyer.Id == id
                select new { buyer, address };
            
            var queryResult = query.Single();
            
            var paymentMethods = _appDataConnection
                .PaymentMethods
                .Where(x => x.BuyerId == id)
                .AsEnumerable();
            
            return new GetBuyerQuery
            {
                Buyer = queryResult.buyer,
                Address = queryResult.address,
                PaymentMethods = paymentMethods
            };
        }


        [HttpPut]
        public async Task<int> CreateBuyer(CreateBuyerCommand createBuyerCommand)
        {
            var buyerId = Guid.NewGuid();
            var buyer = new Buyer
            {
                Id = buyerId,
                Name = createBuyerCommand.Name,
                Birthday = createBuyerCommand.Birthday
            };
            
            await _appDataConnection.InsertAsync(buyer);

            var address = new Address()
            {
                Street = createBuyerCommand.Street,
                Province = createBuyerCommand.Province,
                Country = createBuyerCommand.Country,
                BuyerId = buyerId
            };
            
            await _appDataConnection.InsertAsync(address);

            foreach (var method in createBuyerCommand.PaymentMethods)
            {
                var paymentMethod = new PaymentMethod
                {
                    BuyerId = buyerId,
                    CardNumber = method.CardNumber
                };

                await _appDataConnection.InsertAsync(paymentMethod);
            }
            
            return 1;
        }
    }
}