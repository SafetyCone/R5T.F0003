using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxTokenExtensions
    {
        public static void WriteToFile_Synchronous(this SyntaxToken token,
            string filePath)
        {
            Instances.SyntaxTokenOperator.WriteToFile_Synchronous(token, filePath);
        }

        public static Task WriteToFile(this SyntaxToken token,
            string filePath)
        {
            return Instances.SyntaxTokenOperator.WriteToFile(token, filePath);
        }
    }
}
