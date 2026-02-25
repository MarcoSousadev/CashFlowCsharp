using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Requests;

namespace Validators.tests.Expenses.Register
{
    public class RegisterExpensesValidatorTests 
    {
        [Fact]
        public void Success()
        {

            var validator = new RegisterExpenseValidator();
            var request = new RequestRegisterExpensesJSON
            {
                Amount = 100,
                Date = DateTime.Now.AddDays(-1),
                Description = "Description",
                Title = "Apple",
                PaymentType = CashFlow.Communication.Enums.PaymentType.Pix
            };


            var result = validator.Validate(request);

            Assert.True(result.IsValid);

        }
    }
}
