using CashFlow.Domain.Entities;
using CashFlow.Domain.Repositories.Expenses;
using CashFlow.Excpetion;
using CashFlow.Excpetion.ExceptionsBase;

namespace CashFlow.Application.UseCases.Expenses.ListExpenses
{
    internal class GetByIdUseCase : IGetByIdUseCase
    {
        private readonly IReadOnlyRepository _repository;
        public GetByIdUseCase(IReadOnlyRepository repository)
        {
            _repository = repository;
        }
        public async Task<Expense> Execute(long id)
        {
            var result = await _repository.GetById(id); 

            if(result == null)
            {
                throw new NotFoundException(ResourceErrorMessages.RESOURCE_NOT_FOUND);
            }

            return new Expense
            {
                Amount = result.Amount,
                Date = result.Date,
                Description = result.Description,
                Id = result.Id,
                PaymentType = result.PaymentType,
                Title = result.Title
            };
        }
    }
}
