using CI.API.Rest.Extentions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace CI.ApiRest.Tests
{
    public class ValidatorsTests
    {
        [Theory(DisplayName = "Return true when send valid Cnpj ")]
        [Trait("Validators","Cpnj validators")]
        [InlineData("95326426000146")]
        [InlineData("57833149000179")]
        [InlineData("70.218.538/0001-52")]
        public void Validators_ShouldReturnTrue(string cnpj)
        {
            //Act
            var result = Validators.CnpjIsValid(cnpj);

            //Assert
            Assert.True(result);
        }

        [Theory(DisplayName = "Return false when send invalid or null Cnpj ")]
        [Trait("Validators", "Cpnj validators")]
        [InlineData("57825149000179")]
        [InlineData(null)]
        public void Validators_ShouldReturnFalse(string cnpj)
        {
            //Act
            var result = Validators.CnpjIsValid(cnpj);

            //Assert
            Assert.False(result);
        }
    }
}
