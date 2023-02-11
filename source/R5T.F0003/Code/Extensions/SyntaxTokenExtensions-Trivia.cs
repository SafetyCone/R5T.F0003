using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.Magyar;


namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        public static SyntaxToken AddLeadingLeadingTrivia(this SyntaxToken token,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var newLeadingTrivia = token.HasLeadingTrivia
                ? token.LeadingTrivia.AddLeadingTrivia(trivias)
                : new SyntaxTriviaList(trivias);

            var output = token.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static SyntaxToken AddLeadingLeadingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivias)
        {
            var output = token.AddLeadingLeadingTrivia(trivias.AsEnumerable());
            return output;
        }

        public static SyntaxToken AddLeadingLeadingTrivia(this SyntaxToken token,
            SyntaxTriviaList trivia)
        {
            var output = token.AddLeadingLeadingTrivia(trivia.AsEnumerable());
            return output;
        }

        public static SyntaxToken AddTrailingLeadingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var newLeadingTrivia = token.HasLeadingTrivia
                ? token.LeadingTrivia.AddTrailingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = token.WithLeadingTrivia(newLeadingTrivia);
            return output;
        }

        public static SyntaxToken AddLeadingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var output = token.AddLeadingLeadingTrivia(trivia);
            return output;
        }

        public static SyntaxToken AddTrailingTrailingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var newTrailingTrivia = token.HasTrailingTrivia
                ? token.TrailingTrivia.AddTrailingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = token.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static SyntaxToken AddLeadingTrailingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var newTrailingTrivia = token.HasTrailingTrivia
                ? token.TrailingTrivia.AddLeadingTrivia(trivia)
                : new SyntaxTriviaList(trivia);

            var output = token.WithTrailingTrivia(newTrailingTrivia);
            return output;
        }

        public static SyntaxToken AddTrailingTrivia(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var output = token.AddTrailingTrailingTrivia(trivia);
            return output;
        }

        public static IEnumerable<SyntaxTrivia> GetTrailingSeparatingTrivia(this SyntaxToken token,
            SyntaxToken nextToken)
        {
            var output = token.TrailingTrivia.AsEnumerable()
                .Append(nextToken.LeadingTrivia.AsEnumerable())
                // Strangely, many things have empty trivia.
                .Where(xTrivia => xTrivia.IsNotEmpty())
                ;

            return output;
        }

        public static IEnumerable<SyntaxTrivia> GetTrailingSeparatingTrivia(this SyntaxToken token)
        {
            var nextToken = token.GetNextToken();

            var output = token.GetTrailingSeparatingTrivia(nextToken);
            return output;
        }

        public static bool HasAnyTrailingSeparatingTrivia(this SyntaxToken token,
            SyntaxToken nextToken)
        {
            var trailingSeparatingTrivia = token.GetTrailingSeparatingTrivia(nextToken);

            var output = trailingSeparatingTrivia.Any();
            return output;
        }

        public static bool HasAnyTrailingSeparatingTrivia(this SyntaxToken token)
        {
            var trailingSeparatingTrivia = token.GetTrailingSeparatingTrivia();

            var output = trailingSeparatingTrivia.Any();
            return output;
        }

        public static SyntaxToken Prepend(this SyntaxToken token,
            params SyntaxTrivia[] trivia)
        {
            var output = token.AddLeadingLeadingTrivia(trivia);
            return output;
        }

        public static SyntaxToken Prepend(this SyntaxToken token,
            IEnumerable<SyntaxTrivia> trivias)
        {
            var output = token.AddLeadingLeadingTrivia(trivias);
            return output;
        }

        public static SyntaxToken Prepend(this SyntaxToken token,
            SyntaxTriviaList trivia)
        {
            var output = token.AddLeadingLeadingTrivia(trivia);
            return output;
        }

        public static SyntaxToken WithoutTrailingTrivia(this SyntaxToken token)
        {
            var output = token.WithTrailingTrivia();
            return output;
        }
    }
}
