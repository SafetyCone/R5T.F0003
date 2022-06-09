using System;
using System.Collections.Generic;
using System.Linq;

using Microsoft.CodeAnalysis;


namespace System
{
    public static partial class SyntaxNodeExtensions
    {
        // No "Node" needed in name since only syntax nodes have types, so if a type parameter is specified it must be a node.
        // In addition, syntax nodes are the default type, so the token and trivia methods would have -Token and -Trivia in the method name.

        public static TChild GetChildAsType_SingleOrDefault<TChild>(this SyntaxNode syntaxNode)
            where TChild : class
        {
            var output = syntaxNode.ChildNodes()
                .SingleOrDefault()
                as TChild;

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChildAsType_SingleOrDefault{TChild}(SyntaxNode)"/> as the default.
        /// </summary>
        public static TChild GetChildAsType<TChild>(this SyntaxNode syntaxNode)
            where TChild : class
        {
            var output = syntaxNode.GetChildAsType_SingleOrDefault<TChild>();
            return output;
        }

        public static IEnumerable<TChild> GetChildrenOfType<TChild>(this SyntaxNode syntaxNode)
            where TChild : SyntaxNode
        {
            var output = syntaxNode.ChildNodes()
                .OfType<TChild>()
                ;

            return output;
        }

        /// <summary>
        /// Returns a single child, or errors if more than one.
        /// Note: this is slow, as it evaluates all child nodes of the to ensure there is only one.
        /// </summary>
        public static TChild GetChildOfType_Single<TChild>(this SyntaxNode syntaxNode)
            where TChild : SyntaxNode
        {
            var output = syntaxNode.GetChildrenOfType<TChild>()
                .Single();

            return output;
        }

        /// <summary>
        /// Returns the first child, and assumes there is only one.
        /// <first-is-fast>Note: this is fast since children of the node are only evaluated until a match is found, rather than evaluating all child to make sure only a single match is found.</first-is-fast>
        /// </summary>
        public static TChild GetChildOfType_First<TChild>(this SyntaxNode syntaxNode)
            where TChild : SyntaxNode
        {
            var output = syntaxNode.GetChildrenOfType<TChild>()
                .First();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetChildOfType_First{TChild}(SyntaxNode)"/> as the default.
        /// <inheritdoc cref="GetChildOfType_First{TChild}(SyntaxNode)" path="/summary/first-is-fast"/>
        /// </summary>
        public static TChild GetChildOfType<TChild>(this SyntaxNode syntaxNode)
            where TChild : SyntaxNode
        {
            var output = syntaxNode.GetChildOfType_First<TChild>();
            return output;
        }

        public static IEnumerable<TDescendant> GetDescendantsOfType<TDescendant>(this SyntaxNode syntaxNode)
            where TDescendant : SyntaxNode
        {
            var output = syntaxNode.DescendantNodes()
                .OfType<TDescendant>()
                ;

            return output;
        }

        /// <summary>
        /// Returns a single child, or errors if more than one.
        /// Note: this is slow, as it evaluates all child nodes of the to ensure there is only one.
        /// </summary>
        public static TDescendant GetDescendantOfType_Single<TDescendant>(this SyntaxNode syntaxNode)
            where TDescendant : SyntaxNode
        {
            var output = syntaxNode.GetDescendantsOfType<TDescendant>()
                .Single();

            return output;
        }

        /// <summary>
        /// Returns the first child, and assumes there is only one.
        /// <inheritdoc cref="GetChildOfType_First{TChild}(SyntaxNode)" path="/summary/first-is-fast"/>
        /// </summary>
        public static TDescendant GetDescendantOfType_First<TDescendant>(this SyntaxNode syntaxNode)
            where TDescendant : SyntaxNode
        {
            var output = syntaxNode.GetDescendantsOfType<TDescendant>()
                .First();

            return output;
        }

        /// <summary>
        /// Chooses <see cref="GetDescendantOfType_First{TChild}(SyntaxNode)"/> as the default.
        /// <inheritdoc cref="GetChildOfType_First{TChild}(SyntaxNode)" path="/summary/first-is-fast"/>
        /// </summary>
        public static TDescendant GetDescendantOfType<TDescendant>(this SyntaxNode syntaxNode)
            where TDescendant : SyntaxNode
        {
            var output = syntaxNode.GetDescendantOfType_First<TDescendant>();
            return output;
        }

        public static SyntaxToken GetFirstToken_HandleDocumentationComments(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.GetFirstToken(includeDocumentationComments: true);
            return output;
        }

        public static bool HasChildOfType<TChild>(this SyntaxNode syntaxNode)
        {
            var output = syntaxNode.ChildNodes()
                .OfType<TChild>()
                .Any();

            return output;
        }

        public static TNode ReplaceNode_Better<TNode>(this TNode node,
            SyntaxNode oldDescendantNode,
            SyntaxNode newDescendantNode)
            where TNode : SyntaxNode
        {
            var nodeWasFound = false;

            var output = node.ReplaceNodes(
                EnumerableHelper.From(oldDescendantNode),
                (originalNode, possiblyRewrittenNode) =>
                {
                    if (originalNode == oldDescendantNode)
                    {
                        nodeWasFound = true;
                    }

                    return newDescendantNode;
                });

            if (!nodeWasFound)
            {
                throw new Exception("Node was not found in call to replace node.");
            }

            return output;
        }

        /// <inheritdoc cref="Microsoft.CodeAnalysis.SyntaxNodeExtensions.ReplaceNodes{TRoot, TNode}(TRoot, IEnumerable{TNode}, Func{TNode, TNode, SyntaxNode})"/>
        public static TRootNode ReplaceNodes_Better<TRootNode, TNode>(this TRootNode rootNode,
            IEnumerable<TNode> oldNodes,
            Func<TNode, TNode, SyntaxNode> computeReplacementNode)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var nodesHash = new HashSet<SyntaxNode>(oldNodes);

            var output = rootNode.ReplaceNodes(
                oldNodes,
                (originalNode, possiblyRewrittenNode) =>
                {
                    nodesHash.Remove(originalNode);

                    var outputNode = computeReplacementNode(originalNode, possiblyRewrittenNode);
                    return outputNode;
                });

            if (nodesHash.Any())
            {
                throw new Exception("Some nodes to replace were not found.");
            }

            return output;
        }

        public static TRootNode ReplaceNodes_Better<TRootNode, TNode>(this TRootNode rootNode,
            IDictionary<TNode, TNode> replacements)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var output = rootNode.ReplaceNodes_Better(
                replacements.Keys,
                (originalNode, _) => replacements[originalNode]);

            return output;
        }

        public static TRootNode ReplaceNodes_Better<TRootNode, TNode>(this TRootNode rootNode,
            IEnumerable<(TNode OriginalNode, TNode ReplacementNode)> replacements)
            where TRootNode : SyntaxNode
            where TNode : SyntaxNode
        {
            var replacementsDictionary = replacements
                .ToDictionary(
                    x => x.OriginalNode,
                    x => x.ReplacementNode);

            var output = rootNode.ReplaceNodes_Better(replacementsDictionary);
            return output;
        }

        public static TNode ReplaceToken_Better<TNode>(this TNode node,
            SyntaxToken oldDescendantToken,
            SyntaxToken newDescendantToken)
            where TNode : SyntaxNode
        {
            var tokenWasFound = false;

            var output = node.ReplaceTokens(
                EnumerableHelper.From(oldDescendantToken),
                (originalToken, possiblyRewrittenToken) =>
                {
                    if (originalToken == oldDescendantToken)
                    {
                        tokenWasFound = true;
                    }

                    return newDescendantToken;
                });

            if (!tokenWasFound)
            {
                throw new Exception("Token was not found in call to replace Token.");
            }

            return output;
        }

        public static TNode ReplaceTokens_Better<TNode>(this TNode node,
            IEnumerable<SyntaxToken> oldTokens,
            Func<SyntaxToken, SyntaxToken, SyntaxToken> computeReplacementToken)
            where TNode : SyntaxNode
        {
            var tokensHash = new HashSet<SyntaxToken>(oldTokens);

            var output = node.ReplaceTokens(
                oldTokens,
                (originalToken, possiblyRewrittenToken) =>
                {
                    tokensHash.Remove(originalToken);

                    var outputToken = computeReplacementToken(originalToken, possiblyRewrittenToken);
                    return outputToken;
                });

            if (tokensHash.Any())
            {
                throw new Exception("Some tokens to replace were not found.");
            }

            return output;
        }

        public static TNode ReplaceTokens_Better<TNode>(this TNode node,
            IDictionary<SyntaxToken, SyntaxToken> replacements)
            where TNode : SyntaxNode
        {
            var output = node.ReplaceTokens_Better(
                replacements.Keys,
                (originalToken, _) => replacements[originalToken]);

            return output;
        }

        public static TNode ReplaceTokens_Better<TNode>(this TNode node,
            IEnumerable<(SyntaxToken, SyntaxToken)> replacements)
            where TNode : SyntaxNode
        {
            var replacementsDictionary = replacements
                .ToDictionary(
                    x => x.Item1,
                    x => x.Item2);

            var output = node.ReplaceTokens_Better(replacementsDictionary);
            return output;
        }

        public static TNode ReplaceTrivia_Better<TNode>(this TNode node,
            SyntaxTrivia oldDescendantTrivia,
            SyntaxTrivia newDescendantTrivia)
            where TNode : SyntaxNode
        {
            var triviaWasFound = false;

            var output = node.ReplaceTrivia(
                EnumerableHelper.From(oldDescendantTrivia),
                (originalTrivia, possiblyRewrittenTrivia) =>
                {
                    if (originalTrivia == oldDescendantTrivia)
                    {
                        triviaWasFound = true;
                    }

                    return newDescendantTrivia;
                });

            if (!triviaWasFound)
            {
                throw new Exception("Trivia was not found in call to replace trivia.");
            }

            return output;
        }

        public static TNode ReplaceTrivias_Better<TNode>(this TNode node,
            IEnumerable<SyntaxTrivia> oldTrivias,
            Func<SyntaxTrivia, SyntaxTrivia, SyntaxTrivia> computeReplacementTrivia)
            where TNode : SyntaxNode
        {
            var triviasHash = new HashSet<SyntaxTrivia>(oldTrivias);

            var output = node.ReplaceTrivia(
                oldTrivias,
                (originalTrivia, possiblyRewrittenTrivia) =>
                {
                    triviasHash.Remove(originalTrivia);

                    var outputTrivia = computeReplacementTrivia(originalTrivia, possiblyRewrittenTrivia);
                    return outputTrivia;
                });

            if (triviasHash.Any())
            {
                throw new Exception("Some trivias to replace were not found.");
            }

            return output;
        }

        public static TNode ReplaceTrivias_Better<TNode>(this TNode node,
            IDictionary<SyntaxTrivia, SyntaxTrivia> replacements)
            where TNode : SyntaxNode
        {
            var output = node.ReplaceTrivias_Better(
                replacements.Keys,
                (originalTrivia, _) => replacements[originalTrivia]);

            return output;
        }

        public static TNode ReplaceTrivias_Better<TNode>(this TNode node,
            IEnumerable<(SyntaxTrivia, SyntaxTrivia)> replacements)
            where TNode : SyntaxNode
        {
            var replacementsDictionary = replacements
                .ToDictionary(
                    x => x.Item1,
                    x => x.Item2);

            var output = node.ReplaceTrivias_Better(replacementsDictionary);
            return output;
        }
    }
}
