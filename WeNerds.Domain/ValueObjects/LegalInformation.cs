namespace WeNerds.Domain.ValueObjects;

public readonly record struct LegalInformation(string LegalName, Document Document, DateOnly? EstablishedAt);