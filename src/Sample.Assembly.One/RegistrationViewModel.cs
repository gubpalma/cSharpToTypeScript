using System.Collections.Generic;
using Typescript.Modeller;

namespace Sample.Assembly.One
{
    [TypeScriptViewModel]
    public class RegistrationViewModel
    {
        public IEnumerable<string> Digits { get; set; }
    }
}
