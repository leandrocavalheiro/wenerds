using WeNerds.Commons.Enumarators;

namespace WeNerds.Domain.ValueObjects;

public readonly record struct Document(DocumentTypeEnum DocumentType, string Documento)
{
    public bool IsValid()
    {
        //TODO Implementar Validacoes de documentos
        switch (DocumentType)
        {
            case DocumentTypeEnum.CPF:
            case DocumentTypeEnum.RG:
            case DocumentTypeEnum.CNH:
            case DocumentTypeEnum.Other:
                return true;
            default:
                return true;
        }
    }
}
