using System;


namespace R5T.F0003
{
    public class SyntaxNodeOperator : ISyntaxNodeOperator
    {
        #region Infrastructure

        public static SyntaxNodeOperator Instance { get; } = new();

        private SyntaxNodeOperator()
        {
        }

        #endregion
    }
}
