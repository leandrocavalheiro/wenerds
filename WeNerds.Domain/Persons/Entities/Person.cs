using WeNerds.Commons.Enumarators;
using WeNerds.Domain.Abstractions.Entities;
using WeNerds.Domain.ValueObjects;

namespace WeNerds.Domain.Persons.Entities;

public class Person : Entity
{
    public int Code { get; set; }
    public string Name { get; set; }
    public PersonTypeEnum PersonType { get; set; }
    public NaturalInformation NaturalInformation { get; set; }
    public LegalInformation LegalInformation { get; set; }
    public ICollection<PersonContact> PersonContacts { get; set; }
    
    public Person()
    {        
    }
    public Person(Guid id, Guid accountId, string name, PersonTypeEnum personType = PersonTypeEnum.Natural) : base(id, accountId)
    {
        Name = name;
        PersonType = personType;
    }
    public static Person NewPerson(Guid id, Guid accountId, string name, PersonTypeEnum personType = PersonTypeEnum.Natural)
        => new(id, accountId, name, personType);
    public virtual Person AddNaturalInformation(DateOnly? dateOfBirth, DocumentTypeEnum documentType, string document)
    { 
        NaturalInformation = new(dateOfBirth, new(documentType, document));
        return this;
    }
    public virtual Person AddLegalInformation(string legalName, DocumentTypeEnum documentType, string document, DateOnly? establishedAt)
    {
        if (PersonType == PersonTypeEnum.Natural)
        {
            //Notifications.Add(new());
        }
        LegalInformation = new(legalName, new(documentType, document), establishedAt);
        return this;
    }
    public virtual bool IsNaturalPerson()
        => PersonType == PersonTypeEnum.Natural;
    public virtual bool IsLegalPerson()
        => PersonType == PersonTypeEnum.Legal;
    public virtual Document GetDocument()
        => PersonType == PersonTypeEnum.Natural ? NaturalInformation.Document : LegalInformation.Document;
    public virtual ICollection<PersonContact> GetContacts(ContactTypeEnum contactType = ContactTypeEnum.Email)
        => PersonContacts.Where(p => p.Contact.ContactType == contactType).ToList();
    

    
}
