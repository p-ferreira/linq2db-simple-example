using System;
using LinqToDB.Mapping;

namespace WebAPI_linq2db.Models
{
    public class Buyer
    {
        public Guid Id { get; set; } = Guid.NewGuid();
        public string Name { get; set; } = "";
        public DateTime Birthday { get; set; }
    }
}