/// <description>
/// Main file provides class documentation and attributes.
/// </description>

using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using R5T.T0132;


namespace R5T.F0003
{
    /// <summary>
    /// Provides functionality for working on syntax nodes.
    /// </summary>
    [FunctionalityMarker]
    public partial interface ISyntaxTriviaOperator : IFunctionalityMarker
    {
        public void WriteToFile_Synchronous(
            SyntaxTrivia trivia,
            string filePath)
        {
            Instances.SyntaxOperator.WriteToFile_Synchronous(
                trivia,
                filePath,
                (xTrivia, xTextWriter) => xTrivia.WriteTo(xTextWriter));
        }

        public Task WriteToFile(
            SyntaxTrivia trivia,
            string filePath)
        {
            return Instances.SyntaxOperator.WriteToFile(
                trivia,
                filePath,
                (xTrivia, xTextWriter) => xTrivia.WriteTo(xTextWriter));
        }
    }
}