using AutoFixture;
using NSubstitute;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ConfidentialInvoiceDiscountTests
    {
        private readonly Fixture _fixture;
        public ConfidentialInvoiceDiscountTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void SubmitApplicationFor_ConfidentialInvoiceDiscount_CallConfidentialInvoiceService_Successfully_WithApplicationId()
        {
            var cid = Substitute.For<IConfidentialInvoiceService>();

            var cidProduct = new ConfidentialInvoiceDiscount(cid);
            var companyData = _fixture.Build<SellerCompanyData>().Create();

            var cidRequestResult = Substitute.For<IApplicationResult>();
            cidRequestResult.ApplicationId = 1;
            cidRequestResult.Errors = null;
            cidRequestResult.Success = true;

            cid.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate).Returns(cidRequestResult);

            var result = cidProduct.SubmitApplicationFor(companyData);

            cid.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate);
            Assert.Equal(1, result);
        }

        [Fact]
        public void SubmitApplicationFor_ConfidentialInvoiceDiscount_CallConfidentialInvoiceService_Successfully_WithoutApplicationId()
        {
            var cid = Substitute.For<IConfidentialInvoiceService>();

            var cidProduct = new ConfidentialInvoiceDiscount(cid);
            var companyData = _fixture.Build<SellerCompanyData>().Create();

            var cidRequestResult = Substitute.For<IApplicationResult>();
            cidRequestResult.ApplicationId = null;
            cidRequestResult.Errors = null;
            cidRequestResult.Success = true;

            cid.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate).Returns(cidRequestResult);

            var result = cidProduct.SubmitApplicationFor(companyData);

            cid.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate);
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SubmitApplicationFor_ConfidentialInvoiceDiscount_CallConfidentialInvoiceService_UnSuccessfully()
        {
            var cid = Substitute.For<IConfidentialInvoiceService>();

            var cidProduct = new ConfidentialInvoiceDiscount(cid);
            var companyData = _fixture.Build<SellerCompanyData>().Create();

            var cidRequestResult = Substitute.For<IApplicationResult>();
            cidRequestResult.ApplicationId = null;
            cidRequestResult.Errors = null;
            cidRequestResult.Success = false;

            cid.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate).Returns(cidRequestResult);

            var result = cidProduct.SubmitApplicationFor(companyData);

            cid.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate);
            Assert.Equal(-1, result);
        }
    }
}
