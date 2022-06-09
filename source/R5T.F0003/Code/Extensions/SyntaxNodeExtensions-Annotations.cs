using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;

using R5T.Magyar;

using Instances = R5T.F0003.Instances;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        public static TNode Annotate_Untyped<TNode>(this TNode node,
            Func<SyntaxAnnotation> annotationConstructor,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            return Instances.SyntaxNodeOperator.Annotate(
                node,
                annotationConstructor,
                out annotation);
        }

        public static TNode Annotate_Untyped<TNode>(this TNode node,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            return Instances.SyntaxNodeOperator.Annotate(
                node,
                out annotation);
        }

        public static TNode AnnotateNode_Untyped<TNode>(this TNode node,
            SyntaxNode descendantNode,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = new SyntaxAnnotation();

            var newDescendantNode = descendantNode.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceNode_Better(descendantNode, newDescendantNode);

            return outputSyntaxNode;
        }

        public static TNode AnnotateNodes_Untyped<TNode, TDescendantNode>(this TNode node,
            IEnumerable<TDescendantNode> descendantNodes,
            out Dictionary<TDescendantNode, SyntaxAnnotation> annotationsByInputNode)
            where TNode : SyntaxNode
            where TDescendantNode : SyntaxNode
        {
            // Temporary variable is required for use in anonymous method below.
            var tempAnotationsByInputNodes = new Dictionary<TDescendantNode, SyntaxAnnotation>();

            var outputSyntaxNode = node.ReplaceNodes_Better(
                descendantNodes,
                (originalNode, possiblyRewrittenNode) =>
                {
                    var outputNode = possiblyRewrittenNode.Annotate_Untyped(out var annotation);

                    tempAnotationsByInputNodes.Add(originalNode, annotation);

                    return outputNode;
                });

            annotationsByInputNode = tempAnotationsByInputNodes;

            return outputSyntaxNode;
        }

        public static TNode AnnotateToken_Untyped<TNode>(this TNode node,
            SyntaxToken descendantToken,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = new SyntaxAnnotation();

            var newDescendantToken = descendantToken.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceToken_Better(descendantToken, newDescendantToken);

            return outputSyntaxNode;
        }

        public static TNode AnnotateTokens_Untyped<TNode>(this TNode node,
            IEnumerable<SyntaxToken> syntaxTokens,
            out Dictionary<SyntaxToken, SyntaxAnnotation> annotationsByInputTokens)
            where TNode : SyntaxNode
        {
            // Temporary variable is required for use in anonymous method below.
            var tempAnotationsByInputTokens = new Dictionary<SyntaxToken, SyntaxAnnotation>();

            var outputSyntaxNode = node.ReplaceTokens_Better(
                syntaxTokens,
                (originalToken, possiblyRewrittenToken) =>
                {
                    var outputToken = possiblyRewrittenToken.Annotate_Untyped(out var annotation);

                    tempAnotationsByInputTokens.Add(originalToken, annotation);

                    return outputToken;
                });

            annotationsByInputTokens = tempAnotationsByInputTokens;

            return outputSyntaxNode;
        }

        public static TNode AnnotateTrivia_Untyped<TNode>(this TNode node,
            SyntaxTrivia descendantTrivia,
            out SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            annotation = new SyntaxAnnotation();

            var newTrivia = descendantTrivia.WithAdditionalAnnotations(annotation);

            var outputSyntaxNode = node.ReplaceTrivia_Better(descendantTrivia, newTrivia);

            return outputSyntaxNode;
        }

        public static TNode AnnotateTrivias_Untyped<TNode>(this TNode node,
            IEnumerable<SyntaxTrivia> trivias,
            out Dictionary<SyntaxTrivia, SyntaxAnnotation> annotationsByInputTrivias)
            where TNode : SyntaxNode
        {
            // Temporary variable is required for use in anonymous method below.
            var tempAannotationsByInputTrivias = new Dictionary<SyntaxTrivia, SyntaxAnnotation>();

            var outputSyntaxNode = node.ReplaceTrivias_Better(
                trivias,
                (originalTrivia, _) =>
                {
                    var outputTrivia = originalTrivia.Annotate_Untyped(out var annotation);

                    tempAannotationsByInputTrivias.Add(originalTrivia, annotation);

                    return outputTrivia;
                });

            annotationsByInputTrivias = tempAannotationsByInputTrivias;

            return outputSyntaxNode;
        }

        public static SyntaxNode GetAnnotatedNode_Untyped(this SyntaxNode parentNode,
            SyntaxAnnotation annotation)
        {
            var hasAnnotatedNode = parentNode.HasAnnotatedNode(annotation);
            if(!hasAnnotatedNode)
            {
                throw new Exception("Node with annotation not found in parent node.");
            }

            return hasAnnotatedNode.Result;
        }

        public static TNode GetAnnotatedNodeOfType<TNode>(this SyntaxNode parentNode,
            SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            var hasAnnotatedNode = parentNode.HasAnnotatedNodeOfType<TNode>(annotation);
            if (!hasAnnotatedNode)
            {
                throw new Exception("Node with annotation not found in parent node.");
            }

            return hasAnnotatedNode.Result;
        }

        public static SyntaxToken GetAnnotatedToken(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var hasAnnotatedToken = syntaxNode.HasAnnotatedToken(annotation);
            if (!hasAnnotatedToken)
            {
                throw new Exception("No token with annotation found.");
            }

            return hasAnnotatedToken.Result;
        }

        /// <summary>
        /// Awkward naming since "trivia" is both singular and plural, thus the Microsoft methods returning and enumerable do not allow differentiation between singular and plural.
        /// </summary>
        public static SyntaxTrivia GetAnnotatedTriviaSingle(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var hasAnnotatedTrivia = syntaxNode.HasAnnotatedTrivia(annotation);
            if (!hasAnnotatedTrivia)
            {
                throw new Exception("No trivia with annotation found.");
            }

            return hasAnnotatedTrivia.Result;
        }

        public static WasFound<TNode> HasAnnotatedNodeOfType_SingleOrDefault<TNode>(this SyntaxNode node,
            SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            var annotatedNodeOrDefault = node.GetAnnotatedNodes(annotation)
                .OfType<TNode>()
                .SingleOrDefault();

            var output = WasFound.From(annotatedNodeOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAnnotatedNodeOfType_SingleOrDefault{TNode}(SyntaxNode, SyntaxAnnotation)"/>  as the default.
        /// </summary>
        public static WasFound<TNode> HasAnnotatedNodeOfType<TNode>(this SyntaxNode node,
            SyntaxAnnotation annotation)
            where TNode : SyntaxNode
        {
            var output = node.HasAnnotatedNodeOfType_SingleOrDefault<TNode>(annotation);
            return output;
        }

        public static WasFound<SyntaxNode> HasAnnotatedNode_SingleOrDefault(this SyntaxNode node,
            SyntaxAnnotation annotation)
        {
            var annotatedNodeOrDefault = node.GetAnnotatedNodes(annotation)
                .SingleOrDefault();

            var output = WasFound.From(annotatedNodeOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAnnotatedNode_SingleOrDefault(SyntaxNode, SyntaxAnnotation)"/> as the default.
        /// </summary>
        public static WasFound<SyntaxNode> HasAnnotatedNode(this SyntaxNode node,
            SyntaxAnnotation annotation)
        {
            var output = node.HasAnnotatedNode_SingleOrDefault(annotation);
            return output;
        }

        public static WasFound<SyntaxToken> HasAnnotatedToken_SingleOrDefault(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var annotatedTokenOrDefault = syntaxNode.GetAnnotatedTokens(annotation)
                .SingleOrDefault();

            var output = WasFound.From(annotatedTokenOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAnnotatedToken_SingleOrDefault(SyntaxNode, SyntaxAnnotation)"/> as the default.
        /// </summary>
        public static WasFound<SyntaxToken> HasAnnotatedToken(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var output = syntaxNode.HasAnnotatedToken_SingleOrDefault(annotation);
            return output;
        }

        public static WasFound<SyntaxTrivia> HasAnnotatedTrivia_SingleOrDefault(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var triviaOrDefault = syntaxNode.GetAnnotatedTrivia(annotation)
                .SingleOrDefault();

            var output = WasFound.From(triviaOrDefault);
            return output;
        }

        public static WasFound<SyntaxTrivia> HasAnnotatedTrivia_FirstOrDefault(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var triviaOrDefault = syntaxNode.GetAnnotatedTrivia(annotation)
                .FirstOrDefault();

            var output = WasFound.From(triviaOrDefault);
            return output;
        }

        /// <summary>
        /// Chooses <see cref="HasAnnotatedTrivia_SingleOrDefault(SyntaxNode, SyntaxAnnotation)"/> as the default.
        /// </summary>
        public static WasFound<SyntaxTrivia> HasAnnotatedTrivia(this SyntaxNode syntaxNode,
            SyntaxAnnotation annotation)
        {
            var output = syntaxNode.HasAnnotatedTrivia_SingleOrDefault(annotation);
            return output;
        }

        public static TNode ModifyToken<TNode>(this TNode node,
            SyntaxAnnotation annotation,
            Func<SyntaxToken, SyntaxToken> tokenModifer)
            where TNode : SyntaxNode
        {
            var output = annotation.ModifyToken(node, tokenModifer);
            return output;
        }

        public static void VerifyHasAnnotation(this SyntaxNode node,
            SyntaxAnnotation annotation)
        {
            var hasAnnotation = node.HasAnnotation(annotation);
            if(!hasAnnotation)
            {
                throw new Exception("Has annotation verfication failed: node does not have annotation.");
            }
        }
    }
}
