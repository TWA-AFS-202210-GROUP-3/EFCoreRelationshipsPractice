using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Model;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;
using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

namespace EFCoreRelationshipsPractice.Services
{
    public class CompanyService
    {
        private readonly CompanyDbContext companyDbContext;

        public CompanyService(CompanyDbContext companyDbContext)
        {
            this.companyDbContext = companyDbContext;
        }

        public async Task<List<CompanyDto>> GetAll()
        {
            //1. get conpany from db
            var companies = companyDbContext.Companies.Include(company=>company.Profile).Include(company=>company.Employees).ToList();
            //2. convert entity ro dto
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(long id)
        {
            CompanyEntity companyEntity = await companyDbContext.Companies.
                Include(company => company.Profile).
                Include(company => company.Employees).
                FirstOrDefaultAsync(company => company.Id == id);

            return new CompanyDto(companyEntity);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            //1.convert DTO to entity
            CompanyEntity companyEntity = companyDto.ToEntity();

            //2.save entity
            await companyDbContext.Companies.AddAsync(companyEntity);
            await companyDbContext.SaveChangesAsync();

            return companyEntity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            CompanyEntity companyEntity = await companyDbContext.Companies.
                Include(company=>company.Profile).
                Include(company=>company.Employees).
                FirstOrDefaultAsync(company=>company.Id==id);
            companyDbContext.Companies.Remove(companyEntity);
            await companyDbContext.SaveChangesAsync();
        }
    }
}