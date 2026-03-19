using SQLFormatter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatter.Core.Tokenizer
{
    public sealed class SqlTokenizer
    {
        public List<SqlToken> Tokenize(string sql)
        {
            var tokens = new List<SqlToken>();
            int i = 0;

            while (i < sql.Length)
            {
                char c = sql[i];

                if (c == '\r')
                {
                    i++;
                    continue;
                }

                if (c == '\n')
                {
                    tokens.Add(new SqlToken(SqlTokenType.NewLine, "\n"));
                    i++;
                    continue;
                }

                if (c == ' ' || c == '\t')
                {
                    tokens.Add(new SqlToken(SqlTokenType.Whitespace, c.ToString()));
                    i++;
                    continue;
                }

                if (char.IsLetter(c) || c == '_')
                {
                    var sb = new StringBuilder();

                    while (i < sql.Length &&
                           (char.IsLetterOrDigit(sql[i]) || sql[i] == '_'))
                    {
                        sb.Append(sql[i]);
                        i++;
                    }

                    tokens.Add(new SqlToken(SqlTokenType.Identifier, sb.ToString()));
                    continue;
                }

                if (char.IsDigit(c))
                {
                    var sb = new StringBuilder();

                    while (i < sql.Length && char.IsDigit(sql[i]))
                    {
                        sb.Append(sql[i]);
                        i++;
                    }

                    tokens.Add(new SqlToken(SqlTokenType.Number, sb.ToString()));
                    continue;
                }

                if (c == '\'')
                {
                    var sb = new StringBuilder();
                    sb.Append(c);
                    i++;

                    while (i < sql.Length)
                    {
                        sb.Append(sql[i]);

                        if (sql[i] == '\'')
                        {
                            i++;
                            break;
                        }

                        i++;
                    }

                    tokens.Add(new SqlToken(SqlTokenType.StringLiteral, sb.ToString()));
                    continue;
                }

                switch (c)
                {
                    case ',':
                        tokens.Add(new SqlToken(SqlTokenType.Comma, ","));
                        break;

                    case '.':
                        tokens.Add(new SqlToken(SqlTokenType.Dot, "."));
                        break;

                    case '(':
                        tokens.Add(new SqlToken(SqlTokenType.OpenParen, "("));
                        break;

                    case ')':
                        tokens.Add(new SqlToken(SqlTokenType.CloseParen, ")"));
                        break;

                    default:
                        tokens.Add(new SqlToken(SqlTokenType.Operator, c.ToString()));
                        break;
                }

                i++;
            }

            return tokens;
        }
    }
}
