using Newtonsoft.Json;

namespace EducationalPlatform.Application.DTOs.FawaterkDTO;

public class CardPaymentResponse : BasePaymentResponse
{
    [JsonProperty("data")] public CardPaymentResponseDataModel Data { get; set; }

    public class CardPaymentResponseDataModel : BasePaymentDataResponse
    {
        [JsonProperty("payment_data")] public CardPaymentData PaymentData { get; set; }

        public class CardPaymentData
        {
            [JsonProperty("redirectTo")] public string RedirectTo { get; set; }
        }
    }
}