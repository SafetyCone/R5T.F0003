using System;


namespace R5T.F0003
{
    public class SyntaxTriviaOperator : ISyntaxTriviaOperator
    {
        #region Infrastructure

        public static SyntaxTriviaOperator Instance { get; } = new();

        private SyntaxTriviaOperator()
        {
        }

        #endregion
    }
}
