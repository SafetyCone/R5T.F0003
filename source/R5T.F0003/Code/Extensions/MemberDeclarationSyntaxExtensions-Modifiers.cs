using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class MemberDeclarationSyntaxExtensions
    {
        public static SyntaxTokenList GetModifiers(this MemberDeclarationSyntax member)
        {
            return member.Modifiers;
        }

        public static IEnumerable<SyntaxToken> GetModifiers_Enumerable(this MemberDeclarationSyntax member)
        {
            return Instances.SyntaxOperator.GetModifiers_AsEnumerable(member);
        }

        public static WasFound<SyntaxToken> HasLastModifier(this MemberDeclarationSyntax member)
        {
            var hasModifiers = member.HasModifiers();
            if(hasModifiers)
            {
                var lastModifier = hasModifiers.Result.Last();

                var output = WasFound.From(lastModifier);
                return output;
            }
            else
            {
                return WasFound.NotFound<SyntaxToken>();
            }
        }

        public static WasFound<SyntaxTokenList> HasModifiers(this MemberDeclarationSyntax member)
        {
            var exists = member.Modifiers.Any();

            var output = WasFound.From(exists, member.Modifiers);
            return output;
        }

        public static WasFound<SyntaxToken> HasModifier(this MemberDeclarationSyntax member,
            Func<SyntaxToken, bool> predicate)
        {
            return Instances.SyntaxOperator.HasModifier(
                member,
                predicate);
        }

        /// <summary>
        /// As opposed to <see cref="HasModifier(MemberDeclarationSyntax, Func{SyntaxToken, bool})"/>, this method does not return the modifier, just determines whether the member has the modifier.
        /// </summary>
        public static bool HasModifier_YesOrNo(this MemberDeclarationSyntax member,
            Func<SyntaxToken, bool> predicate)
        {
            var output = member.GetModifiers_Enumerable()
                .Where(predicate)
                .Any();

            return output;
        }

        public static WasFound<SyntaxToken> HasPartialModifier(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsPartial());
            return output;
        }

        public static WasFound<SyntaxToken> HasPublicModifier(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsPublic());
            return output;
        }

        public static WasFound<SyntaxToken> HasStaticModifier(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier(x => x.IsStatic());
            return output;
        }

        public static bool IsAbstract(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsAbstract());
            return output;
        }

        public static bool IsAsync(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsAsync());
            return output;
        }

        public static bool IsConst(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsConst());
            return output;
        }

        public static bool IsExtern(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsExtern());
            return output;
        }

        public static bool IsInternal(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsInternal());
            return output;
        }

        public static bool IsOverride(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsOverride());
            return output;
        }

        public static bool IsPrivate(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsPrivate());
            return output;
        }

        public static bool IsPartial(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsPartial());
            return output;
        }

        public static bool IsProtected(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsProtected());
            return output;
        }

        public static bool IsPublic(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsPublic());
            return output;
        }

        public static bool IsReadOnly(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsReadOnly());
            return output;
        }

        public static bool IsSealed(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsSealed());
            return output;
        }

        public static bool IsStatic(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsStatic());
            return output;
        }

        public static bool IsUnsafe(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsUnsafe());
            return output;
        }

        public static bool IsVirtual(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsVirtual());
            return output;
        }

        public static bool IsVolatile(this MemberDeclarationSyntax member)
        {
            var output = member.HasModifier_YesOrNo(x => x.IsVolatile());
            return output;
        }

        /// <summary>
        /// Modifies the modifiers of the member.
        /// </summary>
        /// <remarks>
        /// Cannot use a typed annotation since syntax token lists do not have annotations.
        /// </remarks>
        public static T ModifyModifiers<T>(this T member,
            Func<SyntaxTokenList, SyntaxTokenList> modifersModifier)
            where T : MemberDeclarationSyntax
        {
            var modifiers = member.GetModifiers();

            var modifiedModifiers = modifersModifier(modifiers);

            member = member.WithModifiers(modifiedModifiers) as T;

            return member;
        }

        /// <summary>
        /// Modifies both the modifiers of the member, and the member itself.
        /// </summary>
        /// <remarks>
        /// Cannot use a typed annotation since syntax token lists do not have annotations.
        /// </remarks>
        public static T ModifyModifiers<T>(this T member,
            ModifiersModifier<T> modifersModifier)
            where T : MemberDeclarationSyntax
        {
            var modifiers = member.GetModifiers();

            member = modifersModifier(
                member,
                modifiers,
                out var modifiedModifiers);

            member = member.WithModifiers(modifiedModifiers) as T;

            return member;
        }

        /// <inheritdoc cref="R5T.F0003.ISyntaxOperator.RemoveAccessModifiers{T}(T)"/>
        public static T RemoveAccessModifiers<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            var modified = member.WithModifiers(
                member.Modifiers.RemoveAccessModifiers());

            var output = modified as T;
            return output;
        }

        /// <inheritdoc cref="R5T.F0003.ISyntaxOperator.WithoutModifiers{T}(T)"/>
        public static T WithoutModifiers<T>(this T member)
            where T : MemberDeclarationSyntax
        {
            var emptySyntaxTokenList = new SyntaxTokenList();

            var output = member.WithModifiers(emptySyntaxTokenList) as T;
            return output;
        }
    }
}
