using System;
using System.Collections.Generic;
using TypeScript.Modeller;

namespace Sample.Assembly.One
{
    [TypeScriptViewModel]
    public class PersonViewModel
    {
        public Guid ThisIsAGuid { get; set; }

        public Guid? ThisIsANullableGuid { get; set; }

        public string StringField { get; set; }

        public DateTime? NullableDate { get; set; }

        public IEnumerable<CarViewModel> Cars { get; set; }
    }
}
