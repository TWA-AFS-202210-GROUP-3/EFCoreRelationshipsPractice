using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EFCoreRelationshipsPractice.Dtos;
using EFCoreRelationshipsPractice.Models;
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
            //1. get companies from db
            var companies = companyDbContext.Companies
                .Include(entity => entity.Profile)
                .Include(entity => entity.Employees)
                .ToList();
            //2. convert data to company Dto
            return companies.Select(companyEntity => new CompanyDto(companyEntity)).ToList();
        }

        public async Task<CompanyDto> GetById(long id)
        {
            //1. get matched company from db
            var selectedCompany = companyDbContext.Companies
                .Include(entity => entity.Profile)
                .Include(entity => entity.Employees)
                .ToList()
                .Find(item => item.Id == id);
            //2. convert data to company Dto
            return new CompanyDto(selectedCompany);
        }

        public async Task<int> AddCompany(CompanyDto companyDto)
        {
            //1. convert dto to entity
            CompanyEntity companyEntity = companyDto.ToEntity();

            //2. save entity to db
            await companyDbContext.Companies.AddAsync(companyEntity);
            await companyDbContext.SaveChangesAsync();

            //3. return companyId
            return companyEntity.Id;
        }

        public async Task DeleteCompany(int id)
        {
            //1. get matched company from db
            var selectedCompany = companyDbContext.Companies
                .Include(entity => entity.Profile)
                .Include(entity => entity.Employees)
                .ToList()
                .Find(item => item.Id == id);
            //2. convert data to company Dto
        }
    }
}