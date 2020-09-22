using SlothEnterprise.ProductApplication.Applications;

namespace SlothEnterprise.ProductApplication
{
    public class ProductApplicationService
    {
        public int SubmitApplicationFor(ISellerApplication application)
            => application.Product.SubmitApplicationFor(application.CompanyData);
    }
}
