using CashFlow.Communication.Requests;
using FluentValidation;

namespace CashFlow.Application.UseCases.Expenses.Register
{
    public class RegisterExpenseValidator : AbstractValidator<RequestRegisterExpensesJSON>
    {
        public RegisterExpenseValidator()
        {
            RuleFor(expense => expense.Title).NotEmpty().WithMessage("Title is required");
            RuleFor(expense => expense.Amount).LessThanOrEqualTo(0).WithMessage("The Amount must be greater than 0");
            RuleFor(expense => expense.Date).LessThanOrEqualTo(DateTime.UtcNow).WithMessage("Can not register a future expenses");
            RuleFor(expense => expense.PaymentType).IsInEnum().WithMessage("Payment type its not valid.");
        }

    }
}
