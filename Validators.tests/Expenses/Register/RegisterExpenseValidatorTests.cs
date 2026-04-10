using CashFlow.Application.UseCases.Expenses.Register;
using CashFlow.Communication.Enums;
using CashFlow.Excpetion;
using CommonTestUtilities.Requests;
using FluentAssertions;

namespace Validators.tests.Expenses.Register
{
    public class RegisterExpenseValidatorTests
    {
        [Fact]

        public void Success()
        {
            var validator = new ExpenseValidator();

            var request = RequestRegisterExpensesJSONBuilder.Build();

            var result = validator.Validate(request);

            result.IsValid.Should().BeTrue();
        }

        [Theory]
        [InlineData("")]
        [InlineData("    ")]
        [InlineData(null)]
        public void ErrorTitleEmpty(string title)
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpensesJSONBuilder.Build();
            request.Title = title;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.TITLE_REQUIRED));
        }

        [Fact]
        public void ErrorDateFuture()
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpensesJSONBuilder.Build();
            request.Date = DateTime.UtcNow.AddDays(1);

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.EXPENSES_CANNOT_FOR_THE_FUTURE));
        }

        [Fact]
        public void ErrorPaymentInvalid()
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpensesJSONBuilder.Build();
            request.PaymentType = (PaymentType)700;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.PAYMENT_TYPE_INVALID));
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-1)]
        [InlineData(-2)]
        public void ErrorAmountInvalid(decimal amount)
        {
            var validator = new ExpenseValidator();
            var request = RequestRegisterExpensesJSONBuilder.Build();
            request.Amount = amount;

            var result = validator.Validate(request);

            result.IsValid.Should().BeFalse();
            result.Errors.Should().ContainSingle().And.Contain(e => e.ErrorMessage.Equals(ResourceErrorMessages.AMOUT_MUST_BE_GREATER_THAN_ZERO));
        }

    }
}
