using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Excpetion.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseUseCase
    {

        public ResponseRegisteredExpensesJSON Execute(RequestRegisterExpensesJSON request)
        {

            Validate(request);

            return new ResponseRegisteredExpensesJSON
            {
                Title = request.Title
            };
         
        }

        private void Validate(RequestRegisterExpensesJSON request)
        {
            var validator = new RegisterExpenseValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {
                
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                
                throw new ErrorOnValidationException(errorMessages);
            }

        }
    }
}
