using System.Collections.Generic;

namespace LocalTokenizer.Entities.Models;

public class EnvironmentProperty
{
    public string EnvName { get; set; }
    public IList<EnvironmentPropertyToken> Tokens { get; set; }
}
