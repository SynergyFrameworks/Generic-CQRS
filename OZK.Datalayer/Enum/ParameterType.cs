using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OZK.Datalayer.Enum
{
    public struct ParameterType
    {
        private ParameterType(string value) { Value = value; }

        public string Value { get; private set; }

        public override string ToString()
        {
            return this.Value;
        }

        public static ParameterType Parse = new ParameterType("Parse");
        public static ParameterType Restricted = new ParameterType("Restricted");

        public static implicit operator string(ParameterType action) { return action.ToString(); }
    }
}
