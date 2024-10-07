using Extract_Data_From_PDF.Models;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace Extract_Data_From_PDF.Services;

public class PdfService : IPdfService
{
    private readonly ContextDB _context;
    public PdfService(ContextDB context)
    {
        _context = context;
    }
    public async Task<ItRequest> ProcessarPdfAsync(IFormFile arquivo)
    {
        // Salva o arquivo temporariamente
        var filePath = Path.GetTempFileName();
        using (var stream = new FileStream(filePath, FileMode.Create))
        {
            await arquivo.CopyToAsync(stream);
        }

        // Extrai o texto do PDF
        string pdfText = ExtrairTextoDoPdf(filePath);

        // Faz o parsing do texto extraído
        var itRequest = ParsePdfData(pdfText);

        // Salva os dados no banco de dados
        await SalvarDadosNoBancoAsync(itRequest);

        // Opcional: Remova o arquivo temporário
        if (File.Exists(filePath))
        {
            File.Delete(filePath);
        }

        return itRequest;
    }
    private string ExtrairTextoDoPdf(string filePath)
    {
        using (PdfReader reader = new PdfReader(filePath))
        {
            using (PdfDocument pdfDoc = new PdfDocument(reader))
            {
                string text = "";
                for (int page = 1; page <= pdfDoc.GetNumberOfPages(); page++)
                {
                    text += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(page));
                }
                return text;
            }
        }
    }

    private ItRequest ParsePdfData(string pdfText)
    {
        // Cria o objeto ItRequest para armazenar as informações extraídas
        ItRequest request = new ItRequest();

        // Quebra o texto em linhas
        var linhas = pdfText.Split('\n');
        foreach (var linha in linhas)
        {
            if (linha.StartsWith("REQUESTOR"))
            {
                request.Requestor = linha.Replace("REQUESTOR", "").Trim();
            }
            else if (linha.StartsWith("DEPARTMENT"))
            {
                request.Department = linha.Replace("DEPARTMENT", "").Trim();
            }
            else if (linha.StartsWith("DATE"))
            {
                request.RequestDate = linha.Replace("DATE", "").Trim();
            }
            else if (linha.StartsWith("REASON FOR REQUEST"))
            {
                request.ReasonForRequest = linha.Replace("REASON FOR REQUEST", "").Trim();
            }
            else if (linha.StartsWith("NAME") && linha.Contains("COMPANY AUTHORIZATION"))
            {
                request.AuthorizedBy = linha.Replace("NAME", "").Trim();
            }
            else if (linha.StartsWith("DATE") && linha.Contains("COMPANY AUTHORIZATION"))
            {
                request.AuthorizationDate = linha.Replace("DATE", "").Trim();
            }
            // Adicione mais campos conforme necessário
        }

        //_logger.LogInformation("Dados extraídos: {@request}", request);
        return request;
    }

    private async Task SalvarDadosNoBancoAsync(ItRequest itRequest)
    {
        if (itRequest != null)
        {
            _context.ItRequests.Add(itRequest);
            await _context.SaveChangesAsync();
        }
    }
}
