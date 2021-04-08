using CI.API.Rest.Controllers.V2;
using CI.API.Rest.Domain.Business;
using CI.API.Rest.Domain.Data;
using Microsoft.AspNetCore.Mvc;
using Moq;
using System;
using System.Collections.Generic;
using Xunit;

namespace CI.ApiRest.Tests
{
    public class V2_CompanyControllerTests
    {
        private readonly Company _company;
        public V2_CompanyControllerTests()
        {
            _company = new Company("42727592000198", CompanyType.Headquarter, "Banco do Victor", "VictorBank", "09000000000000", Situation.Active, DateTime.Now, "00000600", new List<Partner> { new Partner("Victor", PartnerType.Individual, "000***356458**") });
        }

        [Theory(DisplayName = "Success getting Company with a valid Cnpj ")]
        [Trait("Company V2", "Get Company Data By Cnpj")]
        [InlineData("42727592000198")]
        [InlineData("42.727.592/0001-98")]
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
        [Trait("Company V2", "Get Company Data By Cnpj")]
        [InlineData("42725642000198")]
        [InlineData("DFdsdsdW")]
        [InlineData("")]
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

        [Theory(DisplayName = "Not Found getting Company with a nonexistent Cnpj ")]
        [Trait("Company V2", "Get Company Data By Cnpj")]
        [InlineData("71299425000191")]
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
