using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis.CSharp.Syntax;


namespace System
{
    public static partial class CompilationUnitSyntaxExtensions
    {
        public static UsingDirectiveSyntax[] GetChildUsingDirectives(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.ChildNodes()
                .OfType<UsingDirectiveSyntax>()
                .ToArray();

            return output;
        }

        public static IEnumerable<string> GetMissingNamespaceNames(this IEnumerable<UsingDirectiveSyntax> usingDirectives,
            IEnumerable<string> namespaceNames)
        {
            var currentNamespaceNames = usingDirectives.GetUsingNamespaceNames();

            var output = namespaceNames.Except(currentNamespaceNames);
            return output;
        }

        public static IEnumerable<string> GetMissingUsingNamespaceNames(this CompilationUnitSyntax compilationUnit,
            IEnumerable<string> namespaceNames)
        {
            var output = compilationUnit.GetUsingNamespaceDirectiveSyntaxes().GetMissingNamespaceNames(namespaceNames);
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxes(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsings().GetUsingNamespaceDirectiveSyntaxes();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNamespaceDirectiveSyntaxes(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .Where(xUsingDirective => xUsingDirective.IsUsingNamespaceDirective())
                ;

            return output;
        }

        public static IEnumerable<string> GetUsingNamespaceNames(this IEnumerable<UsingDirectiveSyntax> usingDirectives)
        {
            var output = usingDirectives
                .GetUsingNamespaceDirectiveSyntaxes()
                .Select(x => x.GetNamespaceName())
                ;

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChildUsingDirectives(CompilationUnitSyntax)"/> as the default.
        /// Only child using directives are returned (as opposed to descendant using directives, which may exist in descendant namespaces).
        /// </summary>
        public static UsingDirectiveSyntax[] GetUsings(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetChildUsingDirectives();
            return output;
        }

        public static IEnumerable<UsingDirectiveSyntax> GetUsingNameAliasDirectiveSyntaxes(this CompilationUnitSyntax compilationUnit)
        {
            var output = compilationUnit.GetUsings().GetUsingNameAliasDirectiveSyntaxes();
            return output;
        }
    }
}
