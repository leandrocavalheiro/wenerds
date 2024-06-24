using WeNerds.Commons.Enumarators;
using WeNerds.Domain.Abstractions.Entities;
using WeNerds.Domain.Persons.Entities;

namespace WeNerds.Domain.Accounties.Entities;

public class Account : Entity
{
    public Guid? ParentId { get; set; }
    public Guid? PersonId { get; set; }
    public string Initials { get; set; }
    public bool IsBlocked { get; set; }
    public Person Person {get; set;}
    public Account(Guid id, Guid? personId, string initials, bool isBlocked = false, Guid? parentId = null) : base(id, id)
    {               
        Initials = initials;
        IsBlocked = isBlocked;
        ParentId = parentId;
        PersonId = personId;        
    }

    public void CreateNewPerson(PersonTypeEnum personType, string name)
    {

    }


}
