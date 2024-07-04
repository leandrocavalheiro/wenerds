using Microsoft.Extensions.Localization;
using WeNerds.Services.Implementation;

namespace WeNerds.Services.Interfaces;

public interface IWeBabel : IStringLocalizer
{
    WeBabel SetContext(string context);
}


