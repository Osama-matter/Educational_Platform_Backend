using Newtonsoft.Json;
namespace EducationalPlatform.Application.DTOs.FawaterkDTO;

public class FawryPaymentResponse : BasePaymentResponse
{
    [JsonProperty("data")]
    public FawryPaymentResponseDataModel Data { get; set; }

    public class FawryPaymentResponseDataModel : BasePaymentDataResponse
    {
        [JsonProperty("payment_data")]
        public FawryPaymentData PaymentData { get; set; }

        public class FawryPaymentData
        {
            [JsonProperty("fawryCode")]
            public string FawryCode { get; set; }
            [JsonProperty("expireDate")]
            public DateTime ExpireDate { get; set; }
        }
    }
}