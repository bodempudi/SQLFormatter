using SQLFormatter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatter.Core.Output
{
    public sealed class SqlOutputBuilder
    {
        public string Build(List<SqlToken> tokens)
        {
            var sb = new StringBuilder();

            foreach (SqlToken token in tokens)
            {
                switch (token.Type)
                {
                    case SqlTokenType.NewLine:
                        sb.AppendLine();
                        break;

                    default:
                        sb.Append(token.NormalizedValue);
                        break;
                }
            }

            return sb.ToString();
        }
    }
}
