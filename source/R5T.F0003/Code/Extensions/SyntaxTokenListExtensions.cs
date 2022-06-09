using System;
using System.Collections.Generic;

using Microsoft.CodeAnalysis;

using Instances = R5T.F0003.Instances;


namespace System.Linq
{
    public static class SyntaxTokenListExtensions
    {
        public static SyntaxTokenList RemoveAccessModifiers(this SyntaxTokenList modifers)
        {
            return Instances.SyntaxOperator.RemoveAccessModifiers(modifers);
        }
    }
}
