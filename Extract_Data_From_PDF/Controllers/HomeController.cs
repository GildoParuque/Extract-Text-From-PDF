using Extract_Data_From_PDF.Models;
using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Extract_Data_From_PDF.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ContextDB _context;

        public HomeController(ILogger<HomeController> logger, ContextDB context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            return View();
        }
        // Método para extrair o texto de um PDF
        private string ExtractTextFromPdf(string filePath)
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

        // Método para fazer o parsing do texto extraído
        private List<PdfData> ParsePdfData(string pdfText)
        {
            List<PdfData> dataList = new List<PdfData>();

            // Exemplo simples de parsing: divida o texto em linhas e faça a extração
            // Esta parte pode ser personalizada conforme a estrutura do seu PDF
            string[] lines = pdfText.Split('\n');
            foreach (var line in lines)
            {
                // Divida as linhas em partes, por exemplo, suponha que o PDF tenha valores separados por espaços
                var parts = line.Split(' ');
                if (parts.Length >= 2) // Certifique-se de que há dados suficientes
                {
                    dataList.Add(new PdfData
                    {
                        Field1 = parts[0], // Substitua conforme necessário
                        Field2 = parts[1]
                    });
                }
            }

            return dataList;
        }

        // Método para salvar os dados no banco de dados
        private void SaveDataToDatabase(List<PdfData> dataList)
        {
            _context.PdfDatas.AddRange(dataList);
            _context.SaveChanges();
        }

        //private void SaveDataToDatabase(List<PdfData> dataList)
        //{
        //    _context.PdfDatas.AddRange(dataList);
        //    _context.SaveChanges();
        //}

        [HttpGet]
        public IActionResult UploadPdf()
        {
            return View();
        }

        [HttpPost("upload")]
        public IActionResult UploadPdf([FromForm] IFormFile file)
        {
            if (file == null || file.Length == 0)
            {
                ViewBag.Message = "Nenhum arquivo selecionado";
                return View();
            }

            // Salve o arquivo PDF temporariamente
            var filePath = Path.GetTempFileName();
            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                file.CopyTo(stream);
            }

            // Extraia o texto do PDF
            string pdfText = ExtractTextFromPdf(filePath);

            // Faça o parsing do texto extraído
            var dataList = ParsePdfData(pdfText);

            // Salve os dados no banco de dados
            SaveDataToDatabase(dataList);
            ViewBag.Message = "Arquivo carregado e dados salvos com sucesso!";
            return View();
        }
        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
