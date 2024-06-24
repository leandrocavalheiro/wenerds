using WeNerds.Commons.Enumarators;
using WeNerds.Domain.Abstractions.Entities;
using WeNerds.Domain.ValueObjects;

namespace WeNerds.Domain.Persons.Entities;

public class PersonContact : Entity
{
    public Guid? PersonId { get; set; }
    public bool IsMain { get; set; }
    public Contact Contact { get; set; }
    public Person Person { get; set; }
    public PersonContact()
    {        
    }
    public PersonContact(Guid accountId, Guid id, Guid? personId, string description, ContactTypeEnum contactType = ContactTypeEnum.Email, bool isMain = false) : base(accountId)
    {
        Id = id;
        PersonId = personId;
        Contact = new(contactType, description);
        IsMain = isMain;
    }
}
