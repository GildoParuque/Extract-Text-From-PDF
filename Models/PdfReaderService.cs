using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace Extract_Data_From_PDF.Models
{
    public class PdfReaderService
    {
        public string ExtractTextFromPdf(string filePath)
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
    }
}
