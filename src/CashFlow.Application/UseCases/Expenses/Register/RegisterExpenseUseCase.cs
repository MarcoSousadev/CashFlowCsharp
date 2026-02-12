using CashFlow.Communication.Enums;
using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;

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

            if (result.IsValid)
            {
                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();
                throw new ArgumentException();
            }

        }
    }
}
