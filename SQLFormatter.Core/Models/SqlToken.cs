using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatter.Core.Models
{
    public sealed class SqlToken
    {
        public SqlTokenType Type { get; set; }

        public string Value { get; set; }

        public string NormalizedValue { get; set; }

        public SqlToken(SqlTokenType type, string value)
        {
            Type = type;
            Value = value;
            NormalizedValue = value;
        }

        public override string ToString()
        {
            return $"{Type}: {Value}";
        }
    }
}
