using SQLFormatter.Core.Models;
using SQLFormatter.Core.Rules;
using SQLFormatter.Core.Tokenizer; 

var sql = "select t1.Col1, t2.Col2 from dbo.Table1 t1";

var tokenizer = new SqlTokenizer();
var tokens = tokenizer.Tokenize(sql);

var keywords = new[]
{
    "SELECT",
    "FROM",
    "WHERE",
    "INNER",
    "JOIN",
    "LEFT",
    "RIGHT",
    "CASE",
    "WHEN",
    "THEN",
    "ELSE",
    "END",
    "AND",
    "OR",
    "INSERT",
    "UPDATE",
    "DELETE"
};

var detector = new SqlKeywordDetector(keywords);
detector.Apply(tokens);

foreach (SqlToken token in tokens)
{
    Console.WriteLine($"{token.Type,-12} : {token.NormalizedValue}");
}