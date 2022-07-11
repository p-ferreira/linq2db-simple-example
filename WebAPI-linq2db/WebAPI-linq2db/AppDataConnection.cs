using LinqToDB.Configuration;
using LinqToDB.Data;
using LinqToDB;
using WebAPI_linq2db.Models;
using LinqToDB.Mapping;
using WebAPI_linq2db.Commands;

public class AppDataConnection : DataConnection
{
    public AppDataConnection(LinqToDBConnectionOptions<AppDataConnection> options)
        : base(options)
    {
        if (mappingSchema == null)
            mappingSchema = InitContextMappings(this.MappingSchema);
    }

    public ITable<Buyer> Buyers => this.GetTable<Buyer>();
    public ITable<Address> Addresses => this.GetTable<Address>();
    public ITable<PaymentMethod> PaymentMethods => this.GetTable<PaymentMethod>();

    private static MappingSchema mappingSchema;

    private static MappingSchema InitContextMappings(MappingSchema ms)
    {
        //Important Link: https://github.com/linq2db/linq2db/issues/193
        ms.GetFluentMappingBuilder()
            .Entity<Buyer>()
            .HasTableName("Buyer")
            .HasPrimaryKey(x => x.Id);            

        ms.GetFluentMappingBuilder()
            .Entity<Address>()
            .HasTableName("Address")
            .HasPrimaryKey(x => x.Id)
            .Association(x => x.Buyer, (address, buyer) => address.BuyerId == buyer.Id);

        ms.GetFluentMappingBuilder()
            .Entity<PaymentMethod>()
            .HasTableName("PaymentMethod")
            .HasPrimaryKey(x => x.Id)
            .Association(x => x.Buyer, (address, buyer) => address.BuyerId == buyer.Id);

        return ms;
    }
}