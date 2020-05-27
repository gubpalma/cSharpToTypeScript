using System;
using Typescript.Modeller;

namespace Sample.Assembly.One
{
    [TypeScriptViewModel]
    public class UnmarkedViewModel
    {
        public Guid ThisIsAGuid { get; set; }
    }
}