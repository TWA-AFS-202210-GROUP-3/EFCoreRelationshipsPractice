using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Model;
using EFCoreRelationshipsPractice.Repository;
using Microsoft.EntityFrameworkCore;

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
            //get company from db
            var companis = companyDbContext.Companies.Include(company => company.Profile)
                .Include(company => company.Employees).ToList();
            //convert entity to dto
            return companis.Select(CompanyEntiy => new CompanyDto(CompanyEntiy)).ToList();
        }

        public async Task<CompanyDto> GetById(long id)
        {

           CompanyEntiy foundCompany = await this.companyDbContext.Companies
               .Include(company => company.Employees)
               .Include(company => company.Profile)
               .FirstOrDefaultAsync(company => company.Id==id);

           return new CompanyDto(foundCompany);

        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            //1. convert dto to entity
            CompanyEntiy companyEntiy = companyDto.ToEntity();

            // save entity to db
            await companyDbContext.Companies.AddAsync(companyEntiy);
            await companyDbContext.SaveChangesAsync();

            return companyEntiy.Id;
        }

        public async Task DeleteCompany(int id)
        {
            var foundCompany = await this.companyDbContext.Companies.Include(company => company.Employees)
                .Include(company => company.Profile).FirstOrDefaultAsync(company => company.Id.Equals(id));
            companyDbContext.Companies.Remove(foundCompany);
            await companyDbContext.SaveChangesAsync();
        }
    }
}


