using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    [Collection("Test")]
    public class CompanyServiceTest: TestBase
    {
        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }



        [Fact]
        public async Task Should_create_company_success_via_company_service()
        {
            //given
            var context = GetCompanyDbContext();
            var companyDto = CompanyDto();

            CompanyService companyService = new CompanyService(context);

            //when
            await companyService.AddCompany(companyDto);

            //then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_delete_company_success_via_company_service()
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
            int companyId = await companyService.AddCompany(companyDto);

            //when
            await companyService.DeleteCompany(companyId);

            //then
            Assert.Empty(context.Companies);
        }

        [Fact]
        public async Task Should_get_all_company_via_company_service()
        {
            //given
            var context = GetCompanyDbContext();
            var companyDto1 = CompanyDto();
            CompanyDto companyDto2 = new CompanyDto();
            companyDto2.Name = "SLB";
            companyDto2.Employees = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                },
            };
            companyDto2.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 100010,
                CertId = "100",
            };

            CompanyService companyService = new CompanyService(context);
            await companyService.AddCompany(companyDto1);
            await companyService.AddCompany(companyDto2);

            //when
            var companyObtained = await companyService.GetAll();

            //then
            Assert.Equal(2, companyObtained.Count);
            Assert.Equal("BGC", companyObtained[0].Name);
            Assert.Equal("SLB", companyObtained[1].Name);
        }

        [Fact]
        public async Task Should_get_company_by_Id_via_company_service()
        {
            //given
            var context = GetCompanyDbContext();
            var companyDto = CompanyDto();

            CompanyService companyService = new CompanyService(context);
            int companyId = await companyService.AddCompany(companyDto);

            //when
            var companyObtained = await companyService.GetById(companyId);

            //then
            Assert.Equal("abc", companyObtained.Name);
        }

        private static CompanyDto CompanyDto()
        {
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
            return companyDto;
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
