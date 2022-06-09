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
    /// Provides functionality for working on syntax tokens.
    /// </summary>
    [FunctionalityMarker]
    public partial interface ISyntaxTokenOperator : IFunctionalityMarker
    {
        public void WriteToFile_Synchronous(
            SyntaxToken token,
            string filePath)
        {
            Instances.SyntaxOperator.WriteToFile_Synchronous(
                token,
                filePath,
                (xToken, xTextWriter) => xToken.WriteTo(xTextWriter));
        }

        public Task WriteToFile(
            SyntaxToken token,
            string filePath)
        {
            return Instances.SyntaxOperator.WriteToFile(
                token,
                filePath,
                (xToken, xTextWriter) => xToken.WriteTo(xTextWriter));
        }
    }
}