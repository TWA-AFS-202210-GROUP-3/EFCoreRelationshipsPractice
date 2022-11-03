using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreRelationshipsPracticeTest.ControllerTest
{
    public class CompanyServiceUnitTest : TestBase
    {
        public CompanyServiceUnitTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_company_success_via_company_service()
        {
            //given
            var context = getCompanyDbContext();
            var companyDto = PrepareCompanyDto();
            CompanyService companyService = new CompanyService(context);
            //when
            await companyService.AddCompany(companyDto);
            //then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_all_companies_success_via_company_service()
        {
            //given
            var context = getCompanyDbContext();
            var companyDto = PrepareCompanyDto();
            CompanyService companyService = new CompanyService(context);
            //when
            await companyService.AddCompany(companyDto);
            await companyService.GetAll();
            //then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_company_by_id_via_company_service()
        {
            //given
            var context = getCompanyDbContext();
            var companyDto = PrepareCompanyDto();
            CompanyService companyService = new CompanyService(context);
            //when
            var companyId = await companyService.AddCompany(companyDto);
            var selectedCompanyDto = await companyService.GetById(companyId);
            //then
            Assert.Equal("IBM", selectedCompanyDto.Name);
        }

        private CompanyDto PrepareCompanyDto()
        {
            CompanyDto companyDto = new CompanyDto()
            {
                Name = "IBM",
                EmployeesDto = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDto = new ProfileDto()
                {
                    RegisteredCapital = 100010,
                    CertId = "100",
                },
            };
            return companyDto;
        }

        private CompanyDbContext getCompanyDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            CompanyDbContext context = scopeService.GetRequiredService<CompanyDbContext>();
            return context;
        }
    }
}
