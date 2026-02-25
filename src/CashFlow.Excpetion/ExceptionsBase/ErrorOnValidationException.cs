namespace CashFlow.Excpetion.ExceptionsBase
{
    public class ErrorOnValidationException : CashFlowException
    {
        public List<string> Erros { get; set; }
        public ErrorOnValidationException(List<string> errorMessages)
        {
            Erros = errorMessages;
        }
    }
}
