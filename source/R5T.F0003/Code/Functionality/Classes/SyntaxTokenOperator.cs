using System;


namespace R5T.F0003
{

    public class SyntaxTokenOperator : ISyntaxTokenOperator
    {
        #region Infrastructure

        public static SyntaxTokenOperator Instance { get; } = new();

        private SyntaxTokenOperator()
        {
        }

        #endregion
    }
}