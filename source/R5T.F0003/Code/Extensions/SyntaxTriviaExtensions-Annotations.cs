using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxTriviaExtensions
    {
        public static SyntaxTrivia Annotate_Untyped(this SyntaxTrivia trivia,
            Func<SyntaxAnnotation> annotationConstructor,
            out SyntaxAnnotation annotation)
        {
            annotation = annotationConstructor();

            var output = trivia.WithAdditionalAnnotations(annotation);
            return output;
        }

        public static SyntaxTrivia Annotate_Untyped(this SyntaxTrivia trivia,
            out SyntaxAnnotation annotation)
        {
            annotation = new SyntaxAnnotation();

            var output = trivia.WithAdditionalAnnotations(annotation);
            return output;
        }
    }
}
