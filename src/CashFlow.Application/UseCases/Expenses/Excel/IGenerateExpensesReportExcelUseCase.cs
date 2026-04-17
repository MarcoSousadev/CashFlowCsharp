using CashFlow.Communication.Requests;

namespace CashFlow.Application.UseCases.Expenses.Excel
{
    public interface IGenerateExpensesReportExcelUseCase
    {
        Task<byte[]> Execute(DateOnly month);
    }
}
