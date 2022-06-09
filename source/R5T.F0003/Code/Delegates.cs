using System;


namespace Microsoft.CodeAnalysis.CSharp.Syntax
{
    public delegate T ModifiersModifier<T>(T member, SyntaxTokenList modifiers, out SyntaxTokenList modifiedModifiers)
        where T : MemberDeclarationSyntax;
}
