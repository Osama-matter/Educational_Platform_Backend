
using EducationalPlatform.Application.DTOs.FawaterkDTO;

using static EducationalPlatform.Application.DTOs.FawaterkDTO.EInvoiceResponseModel;
using static EducationalPlatform.Application.DTOs.FawaterkDTO.PaymentMethodsResponse;

namespace EducationalPlatform.Infrastructure.Services.FawaterkServices;

public interface IFawaterakPaymentService
{
    // Create EInvoice Link
    Task<EInvoiceResponseDataModel?> CreateEInvoiceAsync(EInvoiceRequestModel eInvoice);

    // Payment Integration
    Task<IList<PaymentMethod>?> GetPaymentMethods();
    Task<BasePaymentDataResponse?> GeneralPay(EInvoiceRequestModel invoice);

    // WebHook Verification
    bool VerifyWebhook(WebHookModel webHook);
    bool VerifyCancelTransaction(CancelTransactionModel cancelTransaction);
    bool VerifyApiKeyTransaction(string apiKey);

    // HashKey
    string GenerateHashKeyForIFrame(string domain);
}
