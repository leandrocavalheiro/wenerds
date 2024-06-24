using WeNerds.Commons.Enumarators;
using WeNerds.Commons.Extensions;

namespace WeNerds.Domain.ValueObjects;
public readonly record struct Contact (ContactTypeEnum ContactType, string Description)
{    
    public bool IsValid()
    {
        //TODO Criar validações para outros tipos de contatos
        switch (ContactType)
        {            
            case ContactTypeEnum.Email:
                return Description.IsValidEmail();                
            case ContactTypeEnum.Phone:
            case ContactTypeEnum.WhatsApp:
            case ContactTypeEnum.Telegram:                
            case ContactTypeEnum.Linkedin:
                return true;
            default:
                return true; 
        }
    }
}
