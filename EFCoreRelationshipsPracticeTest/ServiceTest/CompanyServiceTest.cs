using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Repository;
using EFCoreRelationshipsPractice.Services;
using Microsoft.Extensions.DependencyInjection;

namespace EFCoreRelationshipsPracticeTest.ServiceTest
{
    public class CompanyServiceTest: TestBase
    {
        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory)
            : base(factory)
        {
        }

        [Fact]
        public async Task Should_create_company_with_employee_success_via_company_service()
        {
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto()
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDtos = new ProfileDto()
                {
                    RegisteredCapital = 100010,
                    CertId = "100",
                },
            };

            CompanyService companyService = new CompanyService(context);

            await companyService.AddCompany(companyDto);

            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_delete_company_by_id_success_via_company_service()
        {
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto()
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDtos = new ProfileDto()
                {
                    RegisteredCapital = 100010,
                    CertId = "100",
                },
            };

            CompanyService companyService = new CompanyService(context);

            var id = await companyService.AddCompany(companyDto);

            await companyService.DeleteCompany(id);

            Assert.Equal(0, context.Companies.Count());
        }

        [Fact]
        public async Task Should_get_company_by_id_success_via_company_service()
        {
            var context = GetCompanyDbContext();
            CompanyDto companyDto = new CompanyDto()
            {
                Name = "IBM",
                Employees = new List<EmployeeDto>()
                {
                    new EmployeeDto()
                    {
                        Name = "Tom",
                        Age = 19,
                    },
                },
                ProfileDtos = new ProfileDto()
                {
                    RegisteredCapital = 100010,
                    CertId = "100",
                },
            };

            CompanyService companyService = new CompanyService(context);

            var id = await companyService.AddCompany(companyDto);

            CompanyDto company = await companyService.GetById(id);

            Assert.Equal("IBM", company.Name);
        }

        [Fact]
        public async Task Should_get_all_company_by_id_success_via_company_service()
        {
            var context = GetCompanyDbContext();
            List<CompanyDto> companyDtos = new List<CompanyDto>()
            {
                new CompanyDto()
                {
                    Name = "IBM",
                    Employees = new List<EmployeeDto>()
                    {
                        new EmployeeDto()
                        {
                            Name = "Tom",
                            Age = 19,
                        },
                    },
                    ProfileDtos = new ProfileDto()
                    {
                        RegisteredCapital = 100010,
                        CertId = "100",
                    },
                },
                new CompanyDto()
                {
                    Name = "SLB",
                    Employees = new List<EmployeeDto>()
                    {
                        new EmployeeDto()
                        {
                            Name = "Tom",
                            Age = 19,
                        },
                    },
                    ProfileDtos = new ProfileDto()
                    {
                        RegisteredCapital = 100010,
                        CertId = "100",
                    },
                },
            };

            CompanyService companyService = new CompanyService(context);

            foreach (CompanyDto companyDto in companyDtos)
            {
                await companyService.AddCompany(companyDto);
            }

            await companyService.GetAll();

            Assert.Equal(2, context.Companies.Count());
        }

        private CompanyDbContext GetCompanyDbContext()
        {
            var scope = this.Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            CompanyDbContext context = scopeService.GetRequiredService<CompanyDbContext>();
            return context;
        }
    }
}
