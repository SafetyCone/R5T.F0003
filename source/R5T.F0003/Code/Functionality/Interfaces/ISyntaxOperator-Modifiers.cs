using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace R5T.F0003
{
    public partial interface ISyntaxOperator
    {
        /// <summary>
        /// Returns <see cref="IndexHelper.Zero"/>, since access modifiers are always first.
        /// </summary>
        public int GetIndexForAccessModifer()
        {
            var output = IndexHelper.Zero;
            return output;
        }

        public int GetIndexForPublicAccessModifer(
            IEnumerable<SyntaxToken> _)
        {
            var output = this.GetIndexForAccessModifer();
            return output;
        }

        public int GetIndexForPublicAccessModifer(
            SyntaxTokenList _)
        {
            var output = this.GetIndexForAccessModifer();
            return output;
        }

        /// <summary>
        /// The static modifier always comes one after the access modifier.
        /// * static ...
        /// * public static ...
        /// </summary>
        public int GetIndexForStaticAccessModifer(
            SyntaxTokenList modifiers)
        {
            var accessModifierIndex = modifiers.IndexWhere(
                Instances.SyntaxTokenOperator.IsAccessModifier);

            var hasAccessModifier = IndexHelper.IsFound(accessModifierIndex);

            var output = hasAccessModifier
                ? accessModifierIndex + 1
                : IndexHelper.Zero
                ;

            return output;
        }

        public IEnumerable<SyntaxToken> GetModifiers_AsEnumerable(
            MemberDeclarationSyntax member)
        {
            return member.Modifiers;
        }

        public WasFound<SyntaxToken> HasModifier(
            MemberDeclarationSyntax member,
            Func<SyntaxToken, bool> predicate)
        {
            var output = this.HasModifer(
                member.GetModifiers_Enumerable(),
                predicate);

            return output;
        }

        public WasFound<SyntaxToken> HasModifer(
            IEnumerable<SyntaxToken> modifiers,
            Func<SyntaxToken, bool> predicate)
        {
            // Use first (not single) to allow the predicate to select multiple modifiers.
            // In general, the predicate will only be used to select a single modifier (since the return type is only a single syntax token). But we stil

            // Use single (not first) even though the predicate could be used to select multiple modifier syntax tokens.
            // Since the return type is only a single syntax token, choose correctness over robustness to keep with the assumption of only a single syntax token.
            var singleOrDefault = modifiers
                .Where(predicate)
                .SingleOrDefault();

            var output = WasFound.From(singleOrDefault);
            return output;
        }

        /// <summary>
        /// Removes all access modifiers (public, private, internal).
        /// </summary>
        public T RemoveAccessModifiers<T>(
            T member)
            where T : MemberDeclarationSyntax
        {
            return member.RemoveAccessModifiers();
        }

        public SyntaxTokenList RemoveAccessModifiers(SyntaxTokenList modifers)
        {
            var output = modifers.AsEnumerable()
                .RemoveAccessModifiers()
                .ToSyntaxTokenList();

            return output;
        }

        public IEnumerable<SyntaxToken> RemoveAccessModifiers(IEnumerable<SyntaxToken> modifiers)
        {
            // Only keep non-access modifiers.
            var output = modifiers
                .Where(x => x.IsNotAccessModifier())
                ;

            return output;
        }

        /// <summary>
        /// Removes all modifiers.
        /// </summary>
        public T WithoutModifiers<T>(T member)
            where T : MemberDeclarationSyntax
        {
            return member.WithoutModifiers();
        }
    }
}