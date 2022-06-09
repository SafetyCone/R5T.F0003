/// <description>
/// Provides functionality for working on member modifier tokens.
/// </description> 

using System;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;


namespace R5T.F0003
{
    public partial interface ISyntaxTokenOperator
    {
        public bool IsAbstract(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.AbstractKeyword);
            return output;
        }

        public bool IsAsync(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.AsyncKeyword);
            return output;
        }

        public bool IsConst(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.ConstKeyword);
            return output;
        }

        public bool IsExtern(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.ExternKeyword);
            return output;
        }

        public bool IsInternal(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.InternalKeyword);
            return output;
        }

        public bool IsOverride(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.OverrideKeyword);
            return output;
        }

        public bool IsPrivate(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.PrivateKeyword);
            return output;
        }

        public bool IsProtected(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.ProtectedKeyword);
            return output;
        }

        public bool IsPublic(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.PublicKeyword);
            return output;
        }

        public bool IsReadOnly(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.ReadOnlyKeyword);
            return output;
        }

        public bool IsSealed(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.SealedKeyword);
            return output;
        }

        public bool IsStatic(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.StaticKeyword);
            return output;
        }

        public bool IsUnsafe(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.UnsafeKeyword);
            return output;
        }

        public bool IsVirtual(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.VirtualKeyword);
            return output;
        }

        public bool IsVolatile(SyntaxToken syntaxToken)
        {
            var output = syntaxToken.IsKind(SyntaxKind.VolatileKeyword);
            return output;
        }

        public bool IsAccessModifier(SyntaxToken syntaxToken)
        {
            var output = false
                || this.IsPublic(syntaxToken)
                || this.IsProtected(syntaxToken)
                || this.IsInternal(syntaxToken)
                || this.IsPrivate(syntaxToken);

            return output;
        }

        public bool IsNotAccessModifier(SyntaxToken syntaxToken)
        {
            var isAccessModifier = this.IsAccessModifier(syntaxToken);

            var output = !isAccessModifier;
            return output;
        }
    }
}