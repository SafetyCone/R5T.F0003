using System;
using System.IO;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;


namespace R5T.F0003
{
    public partial interface ISyntaxOperator
    {
        public void WriteToFile_Synchronous<TSyntax>(
            TSyntax syntax,
            string filePath,
            Action<TSyntax, StreamWriter> writer)
        {
            using var fileWriter = new StreamWriter(filePath);

            writer(syntax, fileWriter);
        }

        public void WriteToFile_Synchronous<TNode>(
            TNode node,
            string filePath)
            where TNode : SyntaxNode
        {
            this.WriteToFile_Synchronous(
                node,
                filePath,
                (xNode, xTextWriter) => xNode.WriteTo(xTextWriter));
        }

        public void WriteToFile_Synchronous(
            SyntaxToken token,
            string filePath)
        {
            this.WriteToFile_Synchronous(
                token,
                filePath,
                (xToken, xTextWriter) => xToken.WriteTo(xTextWriter));
        }

        public void WriteToFile_Synchronous(
            SyntaxTrivia trivia,
            string filePath)
        {
            this.WriteToFile_Synchronous(
                trivia,
                filePath,
                (xTrivia, xTextWriter) => xTrivia.WriteTo(xTextWriter));
        }

        public async Task WriteToFile<TSyntax>(
            TSyntax syntax,
            string filePath,
            Action<TSyntax, StreamWriter> writer)
        {
            using var memoryStream = new MemoryStream();
            using var memoryStreamWriter = new StreamWriter(memoryStream);

            writer(syntax, memoryStreamWriter);

            // Synchronous flush since this is working on an in-memory stream.
            memoryStreamWriter.Flush();

            // Reset for reading.
            memoryStream.Position = 0;

            using var fileStream = new FileStream(
                filePath,
                FileMode.Create);

            await memoryStream.CopyToAsync(fileStream);

            // No need to flush the file stream.
        }
    }
}