using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication.Products
{
    public class SelectiveInvoiceDiscount : IProduct
    {
        private readonly ISelectInvoiceService _selectInvoiceService;
        public SelectiveInvoiceDiscount(ISelectInvoiceService selectInvoiceService)
        {
            _selectInvoiceService = selectInvoiceService;
        }
        public int Id { get; set; }
        /// <summary>
        /// Proposed networth of the Invoice
        /// </summary>
        public decimal InvoiceAmount { get; set; }
        /// <summary>
        /// Percentage of the networth agreed and advanced to seller
        /// </summary>
        public decimal AdvancePercentage { get; set; } = 0.80M;

        public int SubmitApplicationFor(ISellerCompanyData applicantData)
            => _selectInvoiceService.SubmitApplicationFor(applicantData.Number.ToString(), InvoiceAmount, AdvancePercentage);
    }
}