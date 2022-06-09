using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxTriviaExtensions
    {
        public static bool IsEndOfLine(this SyntaxTrivia trivia)
        {
            var output = trivia.IsKind(SyntaxKind.EndOfLineTrivia);
            return output;
        }

        public static bool IsInLeadingTrivia(this SyntaxTrivia trivia)
        {
            var output = trivia.Token.LeadingTrivia.Contains(trivia);
            return output;
        }

        public static bool IsInTrailingTrivia(this SyntaxTrivia trivia)
        {
            var output = trivia.Token.TrailingTrivia.Contains(trivia);
            return output;
        }

        public static bool IsNewLine(this SyntaxTrivia trivia)
        {
            var output = trivia.IsEndOfLine();
            return output;
        }

        public static bool IsEmpty(this SyntaxTrivia trivia)
        {
            var output = trivia.Span.IsEmpty;
            return output;
        }

        public static bool IsNotEmpty(this SyntaxTrivia trivia)
        {
            var isEmpty = trivia.IsEmpty();

            var output = !isEmpty;
            return output;
        }
    }
}


namespace System.Linq
{
    public static partial class SyntaxTriviaExtensions
    {
        /// <summary>
        /// Note: already exists at <see cref="Microsoft.CodeAnalysis.CSharp.SyntaxExtensions.ToSyntaxTriviaList(IEnumerable{SyntaxTrivia})"/>, but that is an annoying namespace.
        /// </summary>
        public static SyntaxTriviaList ToSyntaxTriviaList(this IEnumerable<SyntaxTrivia> trivias)
        {
            var output = new SyntaxTriviaList(trivias);
            return output;
        }

        public static SyntaxTriviaList ToSyntaxTriviaList(this SyntaxTrivia trivia)
        {
            var output = new SyntaxTriviaList(trivia);
            return output;
        }
    }
}