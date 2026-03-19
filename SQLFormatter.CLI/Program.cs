using SQLFormatter.Core.Output;
using SQLFormatter.Core.Rules;
using SQLFormatter.Core.Tokenizer;
using SQLFormatter.Core.TokenStream;

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

var stream = new TokenStream(tokens);

Console.WriteLine("Current token at start: " + stream.Current?.NormalizedValue);

var builder = new SqlOutputBuilder();
string rebuiltSql = builder.Build(tokens);

Console.WriteLine("---- REBUILT SQL ----");
Console.WriteLine(rebuiltSql);
Console.WriteLine("---------------------");