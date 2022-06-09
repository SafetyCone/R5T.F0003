using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace System
{
    public static class SyntaxTriviaListExtensions
    {
        public static SyntaxTriviaList AddLeadingTrivia(this SyntaxTriviaList trivia,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var output = trivia.InsertRange(0, trivias);
            return output;
        }

        public static SyntaxTriviaList AddLeadingTrivia(this SyntaxTriviaList trivia,
            params SyntaxTrivia[] trivias)
        {
            var output = trivia.AddLeadingTrivia(trivias.AsEnumerable());
            return output;
        }

        public static SyntaxTriviaList AddTrailingTrivia(this SyntaxTriviaList trivia,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var output = trivia.AddRange(trivias);
            return output;
        }

        public static SyntaxTriviaList AddTrailingTrivia(this SyntaxTriviaList trivia,
            params SyntaxTrivia[] trivias)
        {
            var output = trivia.AddTrailingTrivia(trivias.AsEnumerable());
            return output;
        }

        public static SyntaxTriviaList Append(this SyntaxTriviaList trivia,
            params SyntaxTrivia[] trivias)
        {
            var output = trivia.Append(trivias.AsEnumerable()); ;
            return output;
        }

        public static SyntaxTriviaList Append(this SyntaxTriviaList trivia,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var output = trivia.AddRange(trivias);
            return output;
        }

        public static SyntaxTriviaList Append(this SyntaxTriviaList trivia,
            SyntaxTriviaList appendix)
        {
            var output = trivia.Append(appendix.AsEnumerable());
            return output;
        }

        public static SyntaxTriviaList Prepend(this SyntaxTriviaList trivias,
            SyntaxTrivia trivia)
        {
            var output = trivias.Insert(0, trivia);
            return output;
        }

        public static SyntaxTriviaList Prepend(this SyntaxTriviaList trivias,
            SyntaxTriviaList beginningTrivia)
        {
            var output = new SyntaxTriviaList(
                beginningTrivia.AsEnumerable()
                    .Concat(trivias));

            return output;
        }
    }
}
