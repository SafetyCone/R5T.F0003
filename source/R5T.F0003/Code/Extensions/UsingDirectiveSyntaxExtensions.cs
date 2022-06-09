using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;



namespace System
{
    public static class UsingDirectiveSyntaxExtensions
    {
        public static string GetDestinationName(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.GetNameEqualsValue();
            return output;
        }

        public static (string destinationName, string sourceNameExpression) GetNameAliasValues(this UsingDirectiveSyntax usingDirective)
        {
            var destinationName = usingDirective.GetDestinationName();
            var sourceNameExpression = usingDirective.GetSourceNameExpression();

            return (destinationName, sourceNameExpression);
        }

        public static string GetNameEqualsValue(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.Alias
                .GetChildOfType<IdentifierNameSyntax>()
                .Identifier.ToString();

            return output;
        }

        public static string GetNamespaceName(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.GetQualifiedName();
            return output;
        }

        public static string GetQualifiedName(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.Name.ToString();
            return output;
        }

        public static string GetSourceNameExpression(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.GetQualifiedName();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNameAliasDirectiveSyntaxes(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNameAliasDirective())
                ;

            return output;
        }

        public static bool HasNameEqualsChild(this UsingDirectiveSyntax usingDirective)
        {
            var output = usingDirective.HasChildOfType<NameEqualsSyntax>();
            return output;
        }

        public static bool IsUsingNameAliasDirective(this UsingDirectiveSyntax usingDirective)
        {
            // If the node has a NameEquals child node, then it is a name alias directive. Else, it is a using namespace directive.
            var output = usingDirective.HasNameEqualsChild();
            return output;
        }

        public static bool IsUsingNamespaceDirective(this UsingDirectiveSyntax usingDirective)
        {
            // If the node has a NameEquals child node, then it is a name alias directive. Else, it is a using namespace directive.
            var output = !usingDirective.HasNameEqualsChild();
            return output;
        }
    }
}
