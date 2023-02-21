using AutoMapper;
using InsuranceClaimApi.Controllers;
using InsuranceClaimApi.Entities;
using InsuranceClaimApi.Models;
using InsuranceClaimApi.Profiles;
using InsuranceClaimApi.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Moq;

namespace InsuranceClaimAPI.Tests.Controllers
{
    public class CompanyControllerTests
    {

        private readonly CompanyController _companyController;
        private readonly Company _company;
        public CompanyControllerTests()
        {
            _company = new Company()
            {
                Id = 1000,
                Name = "AbcLimitd",
                Address1 = "255, Hertsmere Road",
                Address2 = "London",
                PostCode = "E14 4ED",
                Country = "UnitedKingdom",
                Active = true
            };

            var insuranceRepositoryMock = new Mock<IInsuranceRepository>();
            insuranceRepositoryMock
               .Setup(c => c.GetCompaniesAsync())
               .ReturnsAsync(new List<Company>(){
                            _company,
                            new Company()
                            {
                                Id = 1001,
                                Name ="XyzLimitd",
                                Address1 = "10, Hertsmere Road",
                                Address2 = "London",
                                PostCode = "E14 4ED",
                                Country = "UnitedKingdom",
                                Active = true
                            },
                            new Company()
                            {
                                Id = 1002,
                                Name ="ZyxLimitd",
                                Address1 = "300, Hertsmere Road",
                                Address2 = "London",
                                PostCode = "E14 4ED",
                                Country = "UnitedKingdom",
                                Active = true
                            }
                });

            var mapperConfiguration = new MapperConfiguration(
                cfg => cfg.AddProfile<CompanyProfile>());

            var mapper = new Mapper(mapperConfiguration);

            var logger = Mock.Of<ILogger<CompanyController>>();

            _companyController = new CompanyController(logger, insuranceRepositoryMock.Object, mapper);
        }


        [Fact]
        public async Task GetCompnanies_GetAction_MustReturnOkObjectResult()
        {
            ///Arrange


            ///Act  
            var results = await _companyController.GetCompanies();

            // Assert
            var actionResult = Assert
             .IsType<ActionResult<IEnumerable<CompanyWithOutClaimsDto>>>(results);
            Assert.IsType<OkObjectResult>(actionResult.Result);
        }

        [Fact]
        public async Task GetCompany_GetAction_MustReturnNumberOfAssignedCompanies()
        {
            ///Arrange


            ///Act  
            var results = await _companyController.GetCompanies();

            // Assert
            var actionResult = Assert
                .IsType<ActionResult<IEnumerable<CompanyWithOutClaimsDto>>>(results);

            Assert.Equal(3,
                   ((IEnumerable<CompanyWithOutClaimsDto>)
                   ((OkObjectResult)actionResult.Result).Value).Count());
        }
    }
}
