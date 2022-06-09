using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using R5T.F0003;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        public static SyntaxToken Annotate_Untyped(this SyntaxToken token,
            Func<SyntaxAnnotation> annotationConstructor,
            out SyntaxAnnotation annotation)
        {
            annotation = annotationConstructor();

            var output = token.WithAdditionalAnnotations(annotation);
            return output;
        }

        public static SyntaxToken Annotate_Untyped(this SyntaxToken token,
            out SyntaxAnnotation annotation)
        {
            annotation = new SyntaxAnnotation();

            var output = token.WithAdditionalAnnotations(annotation);
            return output;
        }
    }
}
