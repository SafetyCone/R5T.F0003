using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;
using Microsoft.CodeAnalysis.CSharp;
using Microsoft.CodeAnalysis.CSharp.Syntax;

using R5T.Magyar;


namespace System
{
    public static partial class MemberDeclarationSyntaxExtensions
    {
        public static WasFound<AttributeListSyntax> HasAttributeList_SingleOrDefault(this MemberDeclarationSyntax member)
        {
            var attributeListOrDefault = member.AttributeLists.SingleOrDefault();

            var output = WasFound.From(attributeListOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAttributeList_SingleOrDefault(MemberDeclarationSyntax)"/> as the default.
        /// </summary>
        public static WasFound<AttributeListSyntax> HasAttributeList(this MemberDeclarationSyntax member)
        {
            var output = member.HasAttributeList_SingleOrDefault();
            return output;
        }

        public static T WithoutAttributeLists<T>(this T memberDeclaration)
            where T : MemberDeclarationSyntax
        {
            var emptyAttributeListList = new SyntaxList<AttributeListSyntax>();

            var output = memberDeclaration.WithAttributeLists(emptyAttributeListList) as T;
            return output;
        }
    }
}
