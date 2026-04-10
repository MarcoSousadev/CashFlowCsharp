using CashFlow.Communication.Requests;
using CashFlow.Communication.Responses;
using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Excpetion;
using CashFlow.Excpetion.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.Update
{
    public class UpdateUseCase : IUpdateUseCase
    {
        private readonly IUpdateOnlyRepository _repository;

        private readonly IUnitOfWork _unitOfWork;

        public UpdateUseCase(IUpdateOnlyRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;

        }
        public async Task Execute(long id, RequestExpenseJson request)
        {
            Validate(request);

            var expense = await _repository.GetById(id);
            if (expense == null)
            {
              throw new NotFoundException(ResourceErrorMessages.RESOURCE_NOT_FOUND);
            }

            expense.Title = request.Title;
            expense.Description = request.Description;
            expense.Amount = request.Amount;
            expense.Date = request.Date;
            expense.PaymentType = (CashFlow.Domain.Enums.PaymentType)request.PaymentType;

            _repository.Update(expense);

            await _unitOfWork.Commit();
        }


        private void Validate(RequestExpenseJson request)
        {
            var validator = new ExpenseValidator();

            var result = validator.Validate(request);

            if (result.IsValid == false)
            {

                var errorMessages = result.Errors.Select(f => f.ErrorMessage).ToList();

                throw new ErrorOnValidationException(errorMessages);
            }

        }

    }
}
