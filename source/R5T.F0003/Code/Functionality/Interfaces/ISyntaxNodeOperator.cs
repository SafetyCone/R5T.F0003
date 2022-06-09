/// <description>
/// Main file provides class documentation and attributes.
/// </description>

using System;
using System.Threading.Tasks;

using Microsoft.CodeAnalysis;

using R5T.T0132;


namespace R5T.F0003 /// <see cref="R5T.F0003.Documentation"/>
{
    /// <summary>
    /// Provides functionality for working on syntax nodes.
    /// </summary>
    [FunctionalityMarker]
    public partial interface ISyntaxNodeOperator : IFunctionalityMarker
    {
        public TNode Annotate<TNode>(
            TNode node,
            Func<SyntaxAnnotation> annotationConstructor,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = annotationConstructor();

            var output = node.WithAdditionalAnnotations(annotation);
            return output;
        }

        public TNode Annotate<TNode>(
            TNode node,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = new SyntaxAnnotation();

            var output = node.WithAdditionalAnnotations(annotation);
            return output;
        }

        public void WriteToFile_Synchronous<TNode>(
            TNode node,
            string filePath)
            where TNode : SyntaxNode
        {
            Instances.SyntaxOperator.WriteToFile_Synchronous(
                node,
                filePath,
                (xNode, xTextWriter) => xNode.WriteTo(xTextWriter));
        }

        public Task WriteToFile<TNode>(
            TNode node,
            string filePath)
            where TNode : SyntaxNode
        {
            return Instances.SyntaxOperator.WriteToFile(
                node,
                filePath,
                (xNode, xTextWriter) => xNode.WriteTo(xTextWriter));
        }
    }
}