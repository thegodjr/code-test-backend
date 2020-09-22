using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Products
{
    public class ConfidentialInvoiceDiscount : IProduct
    {
        private readonly IConfidentialInvoiceService _confidentialInvoiceWebService;

        public int Id { get; set; }
        public decimal TotalLedgerNetworth { get; set; }
        public decimal AdvancePercentage { get; set; }
        public decimal VatRate { get; set; } = VatRates.UkVatRate;

        public ConfidentialInvoiceDiscount(IConfidentialInvoiceService confidentialInvoiceWebService)
        {
            _confidentialInvoiceWebService = confidentialInvoiceWebService;
        }

        public int SubmitApplicationFor(ISellerCompanyData applicantData)
        {
            var result = _confidentialInvoiceWebService.SubmitApplicationFor(
                    new CompanyDataRequest
                    {
                        CompanyFounded = applicantData.Founded,
                        CompanyNumber = applicantData.Number,
                        CompanyName = applicantData.Name,
                        DirectorName = applicantData.DirectorName
                    }, TotalLedgerNetworth, AdvancePercentage, VatRate);

            return (result.Success) ? result.ApplicationId ?? -1 : -1;
        }
    }
}