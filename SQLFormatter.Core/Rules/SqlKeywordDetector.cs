using SQLFormatter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatter.Core.Rules
{
    public sealed class SqlKeywordDetector
    {
        private readonly HashSet<string> _keywords;

        public SqlKeywordDetector(IEnumerable<string> keywords)
        {
            _keywords = new HashSet<string>(keywords, StringComparer.OrdinalIgnoreCase);
        }

        public void Apply(List<SqlToken> tokens)
        {
            for (int i = 0; i < tokens.Count; i++)
            {
                SqlToken token = tokens[i];

                if (token.Type == SqlTokenType.Identifier &&
                    _keywords.Contains(token.Value))
                {
                    token.Type = SqlTokenType.Keyword;
                    token.NormalizedValue = token.Value.ToUpperInvariant();
                }
            }
        }
    }
}
