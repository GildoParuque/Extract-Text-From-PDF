namespace Extract_Data_From_PDF.Models;

public class ItRequest
{
    public int Id { get; set; }
    public string? Requestor { get; set; }
    public string? Department { get; set; }
    public string? RequestDate { get; set; }
    public string? EquipmentRequested { get; set; }
    public string? ReasonForRequest { get; set; }
    public string? AuthorizedBy { get; set; }
    public string? AuthorizationDate { get; set; }
}
