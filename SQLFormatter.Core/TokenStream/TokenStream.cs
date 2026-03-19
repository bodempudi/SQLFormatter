using SQLFormatter.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SQLFormatter.Core.TokenStream
{
    public sealed class TokenStream
    {
        private readonly List<SqlToken> _tokens;
        private int _position;

        public TokenStream(List<SqlToken> tokens)
        {
            _tokens = tokens ?? throw new ArgumentNullException(nameof(tokens));
            _position = 0;
        }

        public int Position => _position;

        public bool End => _position >= _tokens.Count;

        public SqlToken? Current => !End ? _tokens[_position] : null;

        public SqlToken? Peek(int offset = 1)
        {
            int index = _position + offset;

            if (index < 0 || index >= _tokens.Count)
            {
                return null;
            }

            return _tokens[index];
        }

        public void Advance(int count = 1)
        {
            _position += count;

            if (_position < 0)
            {
                _position = 0;
            }

            if (_position > _tokens.Count)
            {
                _position = _tokens.Count;
            }
        }

        public void MoveTo(int position)
        {
            if (position < 0)
            {
                _position = 0;
                return;
            }

            if (position > _tokens.Count)
            {
                _position = _tokens.Count;
                return;
            }

            _position = position;
        }

        public void SkipWhitespace()
        {
            while (!End &&
                   (Current!.Type == SqlTokenType.Whitespace ||
                    Current.Type == SqlTokenType.NewLine))
            {
                Advance();
            }
        }

        public bool MatchKeyword(string keyword)
        {
            return Current != null &&
                   Current.Type == SqlTokenType.Keyword &&
                   string.Equals(Current.NormalizedValue, keyword, StringComparison.OrdinalIgnoreCase);
        }

        public bool MatchIdentifier(string identifier)
        {
            return Current != null &&
                   Current.Type == SqlTokenType.Identifier &&
                   string.Equals(Current.Value, identifier, StringComparison.OrdinalIgnoreCase);
        }

        public List<SqlToken> ReadRange(int start, int count)
        {
            if (start < 0 || start >= _tokens.Count || count <= 0)
            {
                return new List<SqlToken>();
            }

            int safeCount = Math.Min(count, _tokens.Count - start);
            return _tokens.GetRange(start, safeCount);
        }

        public void ReplaceRange(int start, int count, List<SqlToken> replacement)
        {
            if (start < 0 || start > _tokens.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            if (replacement == null)
            {
                throw new ArgumentNullException(nameof(replacement));
            }

            int safeCount = Math.Min(count, _tokens.Count - start);

            _tokens.RemoveRange(start, safeCount);
            _tokens.InsertRange(start, replacement);

            _position = start + replacement.Count;
        }

        public void InsertRange(int start, List<SqlToken> tokensToInsert)
        {
            if (start < 0 || start > _tokens.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (tokensToInsert == null)
            {
                throw new ArgumentNullException(nameof(tokensToInsert));
            }

            _tokens.InsertRange(start, tokensToInsert);

            if (_position >= start)
            {
                _position += tokensToInsert.Count;
            }
        }

        public void RemoveRange(int start, int count)
        {
            if (start < 0 || start >= _tokens.Count)
            {
                throw new ArgumentOutOfRangeException(nameof(start));
            }

            if (count < 0)
            {
                throw new ArgumentOutOfRangeException(nameof(count));
            }

            int safeCount = Math.Min(count, _tokens.Count - start);
            _tokens.RemoveRange(start, safeCount);

            if (_position > start)
            {
                _position = Math.Max(start, _position - safeCount);
            }
        }

        public List<SqlToken> GetAllTokens()
        {
            return _tokens;
        }
    }
}
