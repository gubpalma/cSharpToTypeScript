using System.Collections.Generic;
using TypeScript.Modeller;

namespace Sample.Assembly.One
{
    [TypeScriptViewModel]
    public class RegistrationViewModel
    {
        public IEnumerable<string> Digits { get; set; }
    }
}
