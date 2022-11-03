using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Services;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class CompanyServiceTest : TestBase
    {
        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        private CompanyDbContext GetCompanyDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            CompanyDbContext context = scopedService.GetRequiredService<CompanyDbContext>();
            return context;
        }

        [Fact]
        public async Task Should_create_company_through_service()
        {
            //given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeesDto = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                }
            };
            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 10000,
                CertId = "180"
            };
            CompanyService companyService = new CompanyService(context);

            //when
            await companyService.AddCompany(companyDto);

            //then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_company_by_Id_through_service()
        {
            //given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeesDto = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                }
            };
            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 10000,
                CertId = "180"
            };
            CompanyService companyService = new CompanyService(context);
            var id = await companyService.AddCompany(companyDto);
            Assert.Equal(1, context.Companies.Count());

            //when
            
             var company = await companyService.GetById(id);
             

            //then
            Assert.Equal("IBM", company.Name);
            Assert.Equal("Tom", company.EmployeesDto[0].Name);
            Assert.Equal("180", company.ProfileDto.CertId);
        }

        [Fact]
        public async Task Should_delete_company_by_Id_through_service()
        {
            //given
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto();
            companyDto.Name = "IBM";
            companyDto.EmployeesDto = new List<EmployeeDto>()
            {
                new EmployeeDto()
                {
                    Name = "Tom",
                    Age = 19,
                }
            };
            companyDto.ProfileDto = new ProfileDto()
            {
                RegisteredCapital = 10000,
                CertId = "180"
            };
            CompanyService companyService = new CompanyService(context);
            var id = await companyService.AddCompany(companyDto);
            Assert.Equal(1, context.Companies.Count());
            
            //when
            await companyService.DeleteCompany(id);
            //  await companyService.GetById(company.Id);


            //then
            Assert.Equal(0, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_company_information_through_service()
        {
        }

    }
}
