using WeNerds.Commons.Enumarators;
using WeNerds.Domain.Abstractions.Entities;
using WeNerds.Domain.ValueObjects;

namespace WeNerds.Domain.Persons.Entities;

public class PersonAddress : Entity
{
    public Guid? PersonId { get; set; }
    public Address Address { get; set; }
    public PersonAddress()
    {        
    }
    public PersonAddress(Guid id, Guid accountId, Guid? personId, AddressTypeEnum addressType, Guid? cityId, string zipCode, string street, string number, string district, string complement) : base(id, accountId)
    {
        PersonId = personId;
        SetAddress(addressType, cityId, zipCode, street, number, district, complement);
    }
    public void SetAddress(AddressTypeEnum addressType, Guid? cityId, string zipCode, string street, string number, string district, string complement)
        => Address = new Address(addressType, cityId, zipCode, street, number, district, complement);
    
}
