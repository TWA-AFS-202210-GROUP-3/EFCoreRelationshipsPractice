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
            return GetAllCompanyEntities().Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        private List<CompanyEntity> GetAllCompanyEntities()
        {
            return companyDbContext.Companies
                .Include(company => company.Profile)
                .Include(company => company.Employees).ToList();
        }

        public async Task<CompanyDto> GetById(long id)
        {
            return new CompanyDto(GetAllCompanyEntities().Find(_ => _.Id == id));
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

            var companyEntityFound = this.companyDbContext.Companies
                .Include(company => company.Profile)
                .Include(company => company.Employees)
                .FirstOrDefault(_ => _.Id == id);

            companyDbContext.Companies.RemoveRange(companyEntityFound);
            await this.companyDbContext.SaveChangesAsync();

        }
    }
}