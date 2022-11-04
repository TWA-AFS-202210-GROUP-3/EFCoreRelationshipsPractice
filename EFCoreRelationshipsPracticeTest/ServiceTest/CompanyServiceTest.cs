﻿using EFCoreRelationshipsPractice.Dtos;
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
        public CompanyServiceTest(CustomWebApplicationFactory<Program> factory) : base(factory)
        {
        }

        [Fact]
        public async Task Should_Create_company_successfully()
        {
            //given
            var context = GetCompanyDbContext();

            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                EmployeeDtos = new List<EmployeeDto>()
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
            var companyService = new CompanyService(context);

            //when
            await companyService.AddCompany(companyDto);

            //then
            Assert.Equal(1, context.Companies.Count());
        }

        [Fact]
        public async Task Should_delete_company_successfully()
        {
            //given
            var context = GetCompanyDbContext();

            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                EmployeeDtos = new List<EmployeeDto>()
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
            var companyService = new CompanyService(context);

            int id = await companyService.AddCompany(companyDto);
            Assert.Equal(1, context.Companies.Count());

            //when
            await companyService.DeleteCompany(id);

            //then
            Assert.Equal(0, context.Companies.Count());
        }

        [Fact]
        public async Task Should_return_company_when_given_id_successfully()
        {
            //given
            var context = GetCompanyDbContext();

            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                EmployeeDtos = new List<EmployeeDto>()
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
            var companyService = new CompanyService(context);

            int id = await companyService.AddCompany(companyDto);
            Assert.Equal(1, context.Companies.Count());

            //when
            var companyFound = await companyService.GetById(id);

            //then
            Assert.Equal("IBM", companyFound.Name);
            Assert.Equal(1, companyFound.EmployeeDtos.Count);
            Assert.Equal(100010, companyFound.ProfileDto.RegisteredCapital);
        }

        [Fact]
        public async Task Should_return_all_companies_successfully()
        {
            //given
            var context = GetCompanyDbContext();

            CompanyDto companyDto = new CompanyDto
            {
                Name = "IBM",
                EmployeeDtos = new List<EmployeeDto>()
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

            CompanyDto companyDto1 = new CompanyDto
            {
                Name = "IBM",
                EmployeeDtos = new List<EmployeeDto>()
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
            var companyService = new CompanyService(context);
            await companyService.AddCompany(companyDto);
            await companyService.AddCompany(companyDto1);

            //when
            List<CompanyDto> allCompanies = await companyService.GetAll();

            //then
            Assert.Equal(2, allCompanies.Count);
        }

        public CompanyDbContext GetCompanyDbContext()
        {
            var scope = Factory.Services.CreateScope();
            var scopedService = scope.ServiceProvider;
            CompanyDbContext context = scopedService.GetService<CompanyDbContext>();
            return context;
        }
    }
}
