using Newtonsoft.Json;

namespace EducationalPlatform.Application.DTOs.FawaterkDTO;

public class BasePaymentResponse
{
    [JsonProperty("status")] public string Status { get; set; }
}

public class BasePaymentDataResponse
{
    [JsonProperty("invoice_id")] public string InvoiceId { get; set; }

    [JsonProperty("invoice_key")] public string InvoiceKey { get; set; }
}