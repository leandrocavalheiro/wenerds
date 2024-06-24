using WeNerds.Commons.Enumarators;

namespace WeNerds.Domain.ValueObjects;

public readonly record struct Address(AddressTypeEnum AddressType, Guid? CityId, string ZipCode, string Street, string Number, string District, string Complement);
