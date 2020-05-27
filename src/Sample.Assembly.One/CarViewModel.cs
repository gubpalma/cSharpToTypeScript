using System;
using TypeScript.Modeller;

namespace Sample.Assembly.One
{
    [TypeScriptViewModel]
    public class CarViewModel
    {
        public string StringWithLongCamelCase { get; set; }

        public Single SingleDataType { get; set; }

        public long? Year { get; set; }

        public PersonViewModel Owner { get; set; }

        public RegistrationViewModel Registration { get; set; }
    }
}
