using AutoFixture;
using NSubstitute;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class ProductApplicationTests
    {

        private readonly Fixture _fixture;
        public ProductApplicationTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void SubmitApplicationFor_SelectInvoiceDiscount_CallSelectInvoiceService()
        {
            var sid = Substitute.For<ISelectInvoiceService>();
            var cid = Substitute.For<IConfidentialInvoiceService>();
            var loans = Substitute.For<IBusinessLoansService>();

            var sidProduct = _fixture.Build<SelectiveInvoiceDiscount>().Create();
            var companyData = _fixture.Build<SellerCompanyData>().Create();
            var application = _fixture.Build<SellerApplication>()
                .With(a => a.Product, sidProduct)
                .With(a => a.CompanyData, companyData)
                .Create();

            sid.SubmitApplicationFor(application.CompanyData.Number.ToString(), sidProduct.InvoiceAmount, sidProduct.AdvancePercentage).Returns(100);

            var service = new ProductApplicationService(sid, cid, loans);
            var result = service.SubmitApplicationFor(application);

            sid.Received(1).SubmitApplicationFor(application.CompanyData.Number.ToString(), sidProduct.InvoiceAmount, sidProduct.AdvancePercentage);
            Assert.Equal(100, result);
        }

        [Fact]
        public void SubmitApplicationFor_ConfidentialInvoiceDiscount_CallConfidentialInvoiceService_Successfully_WithApplicationId()
        {
            var sid = Substitute.For<ISelectInvoiceService>();
            var cid = Substitute.For<IConfidentialInvoiceService>();
            var loans = Substitute.For<IBusinessLoansService>();

            var cidProduct = _fixture.Build<ConfidentialInvoiceDiscount>().Create();
            var companyData = _fixture.Build<SellerCompanyData>().Create();
            var application = _fixture.Build<SellerApplication>()
                .With(a => a.Product, cidProduct)
                .With(a => a.CompanyData, companyData)
                .Create();

            var cidRequestResult = Substitute.For<IApplicationResult>();
            cidRequestResult.ApplicationId = 1;
            cidRequestResult.Errors = null;
            cidRequestResult.Success = true;

            cid.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate).Returns(cidRequestResult);

            var service = new ProductApplicationService(sid, cid, loans);
            var result = service.SubmitApplicationFor(application);

            cid.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate);
            Assert.Equal(1, result);
        }

        [Fact]
        public void SubmitApplicationFor_ConfidentialInvoiceDiscount_CallConfidentialInvoiceService_Successfully_WithoutApplicationId()
        {
            var sid = Substitute.For<ISelectInvoiceService>();
            var cid = Substitute.For<IConfidentialInvoiceService>();
            var loans = Substitute.For<IBusinessLoansService>();

            var cidProduct = _fixture.Build<ConfidentialInvoiceDiscount>().Create();
            var companyData = _fixture.Build<SellerCompanyData>().Create();
            var application = _fixture.Build<SellerApplication>()
                .With(a => a.Product, cidProduct)
                .With(a => a.CompanyData, companyData)
                .Create();

            var cidRequestResult = Substitute.For<IApplicationResult>();
            cidRequestResult.ApplicationId = null;
            cidRequestResult.Errors = null;
            cidRequestResult.Success = true;

            cid.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate).Returns(cidRequestResult);

            var service = new ProductApplicationService(sid, cid, loans);
            var result = service.SubmitApplicationFor(application);

            cid.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate);
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SubmitApplicationFor_ConfidentialInvoiceDiscount_CallConfidentialInvoiceService_UnSuccessfully()
        {
            var sid = Substitute.For<ISelectInvoiceService>();
            var cid = Substitute.For<IConfidentialInvoiceService>();
            var loans = Substitute.For<IBusinessLoansService>();

            var cidProduct = _fixture.Build<ConfidentialInvoiceDiscount>().Create();
            var companyData = _fixture.Build<SellerCompanyData>().Create();
            var application = _fixture.Build<SellerApplication>()
                .With(a => a.Product, cidProduct)
                .With(a => a.CompanyData, companyData)
                .Create();

            var cidRequestResult = Substitute.For<IApplicationResult>();
            cidRequestResult.ApplicationId = null;
            cidRequestResult.Errors = null;
            cidRequestResult.Success = false;

            cid.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate).Returns(cidRequestResult);

            var service = new ProductApplicationService(sid, cid, loans);
            var result = service.SubmitApplicationFor(application);

            cid.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), cidProduct.TotalLedgerNetworth, cidProduct.AdvancePercentage, cidProduct.VatRate);
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SubmitApplicationFor_BusinessLoans_CallBusinessLoansService_Successfully_WithApplicationId()
        {
            var sid = Substitute.For<ISelectInvoiceService>();
            var cid = Substitute.For<IConfidentialInvoiceService>();
            var loans = Substitute.For<IBusinessLoansService>();

            var loanProduct = _fixture.Build<BusinessLoans>().Create();
            var companyData = _fixture.Build<SellerCompanyData>().Create();
            var application = _fixture.Build<SellerApplication>()
                .With(a => a.Product, loanProduct)
                .With(a => a.CompanyData, companyData)
                .Create();

            var loanRequestResult = Substitute.For<IApplicationResult>();
            loanRequestResult.ApplicationId = 1;
            loanRequestResult.Errors = null;
            loanRequestResult.Success = true;

            loans.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>()).Returns(loanRequestResult);

            var service = new ProductApplicationService(sid, cid, loans);
            var result = service.SubmitApplicationFor(application);

            loans.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>());
            Assert.Equal(1, result);
        }

        [Fact]
        public void SubmitApplicationFor_BusinessLoans_CallBusinessLoansService_Successfully_WithoutApplicationId()
        {
            var sid = Substitute.For<ISelectInvoiceService>();
            var cid = Substitute.For<IConfidentialInvoiceService>();
            var loans = Substitute.For<IBusinessLoansService>();

            var loanProduct = _fixture.Build<BusinessLoans>().Create();
            var companyData = _fixture.Build<SellerCompanyData>().Create();
            var application = _fixture.Build<SellerApplication>()
                .With(a => a.Product, loanProduct)
                .With(a => a.CompanyData, companyData)
                .Create();

            var loanRequestResult = Substitute.For<IApplicationResult>();
            loanRequestResult.ApplicationId = null;
            loanRequestResult.Errors = null;
            loanRequestResult.Success = true;

            loans.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>()).Returns(loanRequestResult);

            var service = new ProductApplicationService(sid, cid, loans);
            var result = service.SubmitApplicationFor(application);

            loans.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>());
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SubmitApplicationFor_BusinessLoans_CallBusinessLoansService_UnSuccessfully()
        {
            var sid = Substitute.For<ISelectInvoiceService>();
            var cid = Substitute.For<IConfidentialInvoiceService>();
            var loans = Substitute.For<IBusinessLoansService>();

            var loanProduct = _fixture.Build<BusinessLoans>().Create();
            var companyData = _fixture.Build<SellerCompanyData>().Create();
            var application = _fixture.Build<SellerApplication>()
                .With(a => a.Product, loanProduct)
                .With(a => a.CompanyData, companyData)
                .Create();

            var loanRequestResult = Substitute.For<IApplicationResult>();
            loanRequestResult.ApplicationId = null;
            loanRequestResult.Errors = null;
            loanRequestResult.Success = false;

            loans.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>()).Returns(loanRequestResult);

            var service = new ProductApplicationService(sid, cid, loans);
            var result = service.SubmitApplicationFor(application);

            loans.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>());
            Assert.Equal(-1, result);
        }
    }
}
