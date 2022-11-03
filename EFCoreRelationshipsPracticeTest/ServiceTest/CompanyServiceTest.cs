using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class CompanyServiceTest : TestBase
    {
        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async void Should_create_company_success_via_company_service()
        {
            //given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                },
            };

            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };
            CompanyService companyService = new CompanyService(context);

            //when
            await companyService.AddCompany(companyDto);

            //then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async void Should_get_all_companies_success_via_company_service()
        {
            //given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                },
            };

            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };
            CompanyService companyService = new CompanyService(context);
            await companyService.AddCompany(companyDto);

            //when
            var companiesGot = await companyService.GetAll();


            //then
            Assert.Single(companiesGot);
        }

        [Fact]
        public async void Should_get_company_success_via_company_service()
        {
            //given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                },
            };

            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };
            CompanyService companyService = new CompanyService(context);
            await companyService.AddCompany(companyDto);

            //when
            var companiesGot = await companyService.GetAll();


            //then
            Assert.Single(companiesGot);
        }

        private CompanyDbContext GetCompanyDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            CompanyDbContext context = scopedService.GetRequiredService<CompanyDbContext>();
            return context;
        }
    }
}
