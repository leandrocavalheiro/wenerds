using Microsoft.Extensions.Localization;
using WeNerds.Services.Implementation;

namespace WeNerds.Services.Interfaces;

public interface IWeBabel : IStringLocalizer
{
    WeBabel SetContext(string context);
    WeBabel SetFileResource(string path, string name, string context = null, string culture = null);
}


