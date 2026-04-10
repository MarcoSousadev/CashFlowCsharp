namespace CashFlow.Excpetion.ExceptionsBase
{
    public abstract class CashFlowException : SystemException
    {
        protected CashFlowException(string message) : base(message)
        {
            
        }

        public abstract int StatusCode { get; }

        public abstract List<string> GetErros();
    }
}
