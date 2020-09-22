using AutoFixture;
using NSubstitute;
using SlothEnterprise.External;
using SlothEnterprise.External.V1;
using SlothEnterprise.ProductApplication.Applications;
using SlothEnterprise.ProductApplication.Products;
using Xunit;

namespace SlothEnterprise.ProductApplication.Tests
{
    public class BusinessLoansTests
    {
        private readonly Fixture _fixture;
        public BusinessLoansTests()
        {
            _fixture = new Fixture();
        }

        [Fact]
        public void SubmitApplicationFor_BusinessLoans_CallBusinessLoansService_Successfully_WithApplicationId()
        {
            var loans = Substitute.For<IBusinessLoansService>();

            var loanProduct = new BusinessLoans(loans);
            var companyData = _fixture.Build<SellerCompanyData>().Create();

            var loanRequestResult = Substitute.For<IApplicationResult>();
            loanRequestResult.ApplicationId = 1;
            loanRequestResult.Errors = null;
            loanRequestResult.Success = true;

            loans.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>()).Returns(loanRequestResult);

            var result = loanProduct.SubmitApplicationFor(companyData);

            loans.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>());
            Assert.Equal(loanRequestResult.ApplicationId, result);
        }

        [Fact]
        public void SubmitApplicationFor_BusinessLoans_CallBusinessLoansService_Successfully_WithoutApplicationId()
        {
            var loans = Substitute.For<IBusinessLoansService>();

            var loanProduct = new BusinessLoans(loans);
            var companyData = _fixture.Build<SellerCompanyData>().Create();

            var loanRequestResult = Substitute.For<IApplicationResult>();
            loanRequestResult.ApplicationId = null;
            loanRequestResult.Errors = null;
            loanRequestResult.Success = true;

            loans.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>()).Returns(loanRequestResult);

            var result = loanProduct.SubmitApplicationFor(companyData);

            loans.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>());
            Assert.Equal(-1, result);
        }

        [Fact]
        public void SubmitApplicationFor_BusinessLoans_CallBusinessLoansService_UnSuccessfully()
        {
            var loans = Substitute.For<IBusinessLoansService>();

            var loanProduct = new BusinessLoans(loans);
            var companyData = _fixture.Build<SellerCompanyData>().Create();

            var loanRequestResult = Substitute.For<IApplicationResult>();
            loanRequestResult.ApplicationId = null;
            loanRequestResult.Errors = null;
            loanRequestResult.Success = false;

            loans.SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>()).Returns(loanRequestResult);

            var result = loanProduct.SubmitApplicationFor(companyData);

            loans.Received(1).SubmitApplicationFor(Arg.Any<CompanyDataRequest>(), Arg.Any<LoansRequest>());
            Assert.Equal(-1, result);
        }
    }
}
