using CashFlow.Domain.Enums;
using CashFlow.Domain.Reports;
using CashFlow.Domain.Repositories.Expenses;
using ClosedXML.Excel;

namespace CashFlow.Application.UseCases.Expenses.Excel
{
    public class GenereteExpenseReportExcelUseCase : IGenerateExpensesReportExcelUseCase
    {
        private const string CURRENCY_SYMBOL = "R$";
        private readonly IReadOnlyRepository _repository;
        public GenereteExpenseReportExcelUseCase(IReadOnlyRepository repository)
        {
            _repository = repository;
        }
        public async Task<byte[]> Execute(DateOnly month)
        {
            var expenses = await _repository.FilterByMonth(month);
            if(expenses.Count== 0)
            {
                return [];
            }

            using var workbook = new XLWorkbook();

            workbook.Author = "Marco Sousa";
            workbook.Style.Font.FontSize = 12;
            workbook.Style.Font.FontName = "Times New Roman";

            var worksheet = workbook.Worksheets.Add(month.ToString("Y"));

            IsenrtHeader(worksheet);

            var row = 2;

            foreach(var expense in expenses)
            {
                worksheet.Cell($"A{row}").Value = expense.Title;
                worksheet.Cell($"B{row}").Value = expense.Date;
                worksheet.Cell($"C{row}").Value = ConvertPaymentType(expense.PaymentType);
                
                worksheet.Cell($"D{row}").Value = expense.Amount;
                worksheet.Cell($"D{row}").Style.NumberFormat.Format = $"-{CURRENCY_SYMBOL} #,##0.00";

                worksheet.Cell($"E{row}").Value = expense.Description;

                row++;
            }

            worksheet.Columns().AdjustToContents();

            var file = new MemoryStream();

            workbook.SaveAs(file);


            return file.ToArray();
        }

        private string ConvertPaymentType(PaymentType payment)
        {
            return payment switch
            {
                PaymentType.Cash => "Dinheiro",
                PaymentType.CreditCard => "Cartão de Credito",
                PaymentType.DebitCard => "Cartão de Débito",
                PaymentType.Pix => "Pix",
                _ => string.Empty
            };
        }

        private void IsenrtHeader(IXLWorksheet worksheet)
        {
            worksheet.Cell("A1").Value = ResourceReportGenerationMessage.TITLE;
            worksheet.Cell("B1").Value = ResourceReportGenerationMessage.DATE;
            worksheet.Cell("C1").Value = ResourceReportGenerationMessage.PAYMENT_TYPE;
            worksheet.Cell("D1").Value = ResourceReportGenerationMessage.AMOUNT;
            worksheet.Cell("E1").Value = ResourceReportGenerationMessage.DESCRIPTION;

            worksheet.Cells("A1:E1").Style.Font.Bold = true;
            worksheet.Cells("A1:E1").Style.Fill.BackgroundColor = XLColor.FromHtml("#82E0AA");

            worksheet.Cell("A1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("B1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("C1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);
            worksheet.Cell("D1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Right);
            worksheet.Cell("E1").Style.Alignment.SetHorizontal(XLAlignmentHorizontalValues.Center);




        }
    }
}
