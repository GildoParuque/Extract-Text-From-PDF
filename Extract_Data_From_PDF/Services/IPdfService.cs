using Extract_Data_From_PDF.Models;

namespace Extract_Data_From_PDF.Services;

public interface IPdfService
{
    Task<ItRequest> ProcessarPdfAsync(IFormFile arquivo);


}
