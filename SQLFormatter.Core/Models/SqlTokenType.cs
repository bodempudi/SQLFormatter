using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatter.Core.Models
{
    public enum SqlTokenType
    {
        Keyword,
        Identifier,
        StringLiteral,
        Number,
        Comma,
        Dot,
        OpenParen,
        CloseParen,
        Operator,
        Comment,
        Whitespace,
        NewLine,
        Unknown
    }
}
