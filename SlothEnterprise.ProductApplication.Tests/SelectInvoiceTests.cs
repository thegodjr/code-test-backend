using AutoFixture;
using NSubstitute;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class SelectInvoiceTests
    {
        private readonly Fixture _fixture;
        public SelectInvoiceTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void SubmitApplicationFor_SelectInvoiceDiscount_CallSelectInvoiceService()
        {
            var sid = Substitute.For<ISelectInvoiceService>();

            var sidProduct = new SelectiveInvoiceDiscount(sid);
            var companyData = _fixture.Build<SellerCompanyData>().Create();

            sid.SubmitApplicationFor(companyData.Number.ToString(), sidProduct.InvoiceAmount, sidProduct.AdvancePercentage).Returns(100);

            var result = sidProduct.SubmitApplicationFor(companyData);

            sid.Received(1).SubmitApplicationFor(companyData.Number.ToString(), sidProduct.InvoiceAmount, sidProduct.AdvancePercentage);
            Assert.Equal(100, result);
        }
    }
}
