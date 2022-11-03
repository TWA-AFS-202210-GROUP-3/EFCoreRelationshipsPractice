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
            var companies = companyDbContext.Companies
                .Include(company => company.Profile)
                .Include(company => company.Employees).ToList();

            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(long id)
        {
            throw new NotImplementedException();
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            CompanyEntity entity = companyDto.ToEntity();

            await this.companyDbContext.Companies.AddAsync(entity);
            await this.companyDbContext.SaveChangesAsync();
            return entity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            var companyEntityFound = this.companyDbContext.Companies.Find(id);
            await this.companyDbContext.Companies.Remove(companyEntityFound);
        }
    }
}