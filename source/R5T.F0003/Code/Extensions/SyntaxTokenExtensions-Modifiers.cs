using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;

using R5T.Magyar;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        /// <summary>
        /// Returns the index of the last access modifer, or <see cref="IndexHelper.NotFound"/> if no access modifier is found.
        /// </summary>
        public static int GetIndexOfLastAccessModifier(this IEnumerable<SyntaxToken> modifiers)
        {
            var output = modifiers.LastIndexWhere(x => x.IsAccessModifier());
            return output;
        }

        /// <summary>
        /// Returns <see cref="IndexHelper.Zero"/>, since access modifiers are always first.
        /// </summary>
        public static int GetIndexForAccessModifer(this IEnumerable<SyntaxToken> _)
        {
            return Instances.SyntaxOperator.GetIndexForAccessModifer();
        }

        /// <summary>
        /// Quality-of-life overload for <see cref="GetIndexForAccessModifer(IEnumerable{SyntaxToken})"/>.
        /// </summary>
        public static int GetIndexForPublicAccessModifer(this IEnumerable<SyntaxToken> modifiers)
        {
            return Instances.SyntaxOperator.GetIndexForPublicAccessModifer(modifiers);
        }

        public static bool IsAccessModifier(this SyntaxToken token)
        {
            var output = false
                || token.IsPublic()
                || token.IsProtected()
                || token.IsInternal()
                || token.IsPrivate();

            return output;
        }

        public static bool IsNotAccessModifier(this SyntaxToken token)
        {
            var isAccessModifier = token.IsAccessModifier();

            var output = !isAccessModifier;
            return output;
        }

        public static bool IsAbstract(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsAbstract(syntaxToken);
            return output;
        }

        public static bool IsAsync(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsAsync(syntaxToken);
            return output;
        }

        public static bool IsConst(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsConst(syntaxToken);
            return output;
        }

        public static bool IsExtern(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsExtern(syntaxToken);
            return output;
        }

        public static bool IsInternal(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsInternal(syntaxToken);
            return output;
        }

        public static bool IsOverride(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsOverride(syntaxToken);
            return output;
        }

        public static bool IsPartial(this SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.PartialKeyword);
            return output;
        }

        public static bool IsPrivate(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsPrivate(syntaxToken);
            return output;
        }

        public static bool IsProtected(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsProtected(syntaxToken);
            return output;
        }

        public static bool IsPublic(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsPublic(syntaxToken);
            return output;
        }

        public static bool IsReadOnly(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsReadOnly(syntaxToken);
            return output;
        }

        public static bool IsSealed(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsSealed(syntaxToken);
            return output;
        }

        public static bool IsStatic(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsStatic(syntaxToken);
            return output;
        }

        public static bool IsUnsafe(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsUnsafe(syntaxToken);
            return output;
        }

        public static bool IsVirtual(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsVirtual(syntaxToken);
            return output;
        }

        public static bool IsVolatile(this SyntaxToken syntaxToken)
        {
            var output = Instances.SyntaxTokenOperator.IsVolatile(syntaxToken);
            return output;
        }
    }
}


namespace System.Linq
{
    public static partial class SyntaxTokenExtensions
    {
        public static WasFound<SyntaxToken> HasModifier(this IEnumerable<SyntaxToken> modifiers,
            Func<SyntaxToken, bool> predicate)
        {
            return Instances.SyntaxOperator.HasModifer(
                modifiers,
                predicate);
        }

        public static IEnumerable<SyntaxToken> RemoveAccessModifiers(this IEnumerable<SyntaxToken> modifiers)
        {
            return Instances.SyntaxOperator.RemoveAccessModifiers(modifiers);
        }
    }
}