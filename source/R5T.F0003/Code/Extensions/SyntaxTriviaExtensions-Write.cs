using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxTriviaExtensions
    {
        public static void WriteToFile_Synchronous(this SyntaxTrivia trivia,
            string filePath)
        {
            Instances.SyntaxTriviaOperator.WriteToFile_Synchronous(trivia, filePath);
        }

        public static Task WriteToFile(this SyntaxTrivia trivia,
            string filePath)
        {
            return Instances.SyntaxTriviaOperator.WriteToFile(trivia, filePath);
        }
    }
}
