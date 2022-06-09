using System;
using System.Collections.Generic;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        public static bool IsFirstTokenInCompilationUnit(this SyntaxToken token)
        {
            var previousToken = token.GetPreviousToken();

            var output = previousToken.IsNone();
            return output;
        }

        public static bool IsNone(this SyntaxToken token)
        {
            var output = token.IsKind(SyntaxKind.None);
            return output;
        }

        public static bool IsNotNone(this SyntaxToken token)
        {
            var isNone = token.IsNone();

            var output = !isNone;
            return output;
        }
    }
}


namespace System.Linq
{
    public static partial class SyntaxTokenExtensions
    {
        public static SyntaxTokenList ToSyntaxTokenList(this IEnumerable<SyntaxToken> tokens)
        {
            var output = new SyntaxTokenList(tokens);
            return output;
        }
    }
}
