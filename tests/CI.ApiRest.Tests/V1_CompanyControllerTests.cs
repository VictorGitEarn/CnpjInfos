using CI.API.Rest.Controllers.V1;
using CI.API.Rest.Domain.Business;
using CI.API.Rest.Domain.Data;
using CI.API.Rest.Models;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Moq.AutoMock;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CI.ApiRest.Tests
{
    public class V1_CompanyControllerTests
    {
        private readonly Company _company;
        public V1_CompanyControllerTests()
        {
            _company = new Company("42727592000198", CompanyType.Headquarter, "Banco do Victor", "VictorBank", "09000000000000", Situation.Active, DateTime.Now, "00000600", new List<Partner> { new Partner("Victor", PartnerType.Individual, "***356458**") });
        }

        [Theory(DisplayName = "Success getting Company with a valid Cnpj ")]
        [Trait("Company V1", "Get Company Data By Cnpj")]
        [InlineData("42727592000198")]
        [Obsolete("This version is obsolete!")]
        public void GetCompany_ShouldReturnOk(string cnpj)
        {
            //Arranje
            var repository = new Mock<ICompanyRepository>();

            repository.Setup(repo => repo.GetBySocialSecurity(It.IsAny<string>())).Returns(() => _company);

            //Act
            var response = new CompanyController(repository.Object).GetCompany(cnpj);

            //Assert
            Assert.IsType<OkObjectResult>(response);
        }

        [Theory(DisplayName = "Error getting Company with a invalid or empty Cnpj ")]
        [Trait("Company V1", "Get Company Data By Cnpj")]
        [InlineData("42725642000198")]
        [InlineData("DFdsdsdW")]
        [InlineData("")]
        [Obsolete("This version is obsolete!")]
        public void GetCompany_ShouldReturnBadRequest(string cnpj)
        {
            //Arranje
            var repository = new Mock<ICompanyRepository>();

            repository.Setup(repo => repo.GetBySocialSecurity(It.IsAny<string>())).Returns(() => _company);

            //Act
            var response = new CompanyController(repository.Object).GetCompany(cnpj);

            //Assert
            Assert.IsType<BadRequestObjectResult>(response);
        }

        [Theory(DisplayName = "Not Found getting Company with a nonexistent or with mask Cnpj ")]
        [Trait("Company V1", "Get Company Data By Cnpj")]
        [InlineData("71299425000191")]
        [InlineData("42.727.592/0001-98")]
        [Obsolete("This version is obsolete!")]
        public void GetCompany_ShouldReturnNotFound(string cnpj)
        {
            //Arranje
            var repository = new Mock<ICompanyRepository>();

            repository.Setup(repo => repo.GetBySocialSecurity(It.IsAny<string>())).Returns(() => null);

            //Act
            var response = new CompanyController(repository.Object).GetCompany(cnpj);

            //Assert
            Assert.IsType<NotFoundResult>(response);
        }
    }
}
