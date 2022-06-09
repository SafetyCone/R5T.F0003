using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static void WriteToFile_Synchronous(this SyntaxNode node,
            string filePath)
        {
            Instances.SyntaxNodeOperator.WriteToFile_Synchronous(node, filePath);
        }

        public static Task WriteToFile(this SyntaxNode node,
            string filePath)
        {
            return Instances.SyntaxNodeOperator.WriteToFile(node, filePath);
        }
    }
}
