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
        public async Task Should_create_company_success_via_company_service()
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

        private CompanyDbContext GetCompanyDbContext()
        {
            var scope = this.Factory.Services.CreateScope();
            var scopeService = scope.ServiceProvider;
            CompanyDbContext context = scopeService.GetRequiredService<CompanyDbContext>();
            return context;
        }
    }
}
