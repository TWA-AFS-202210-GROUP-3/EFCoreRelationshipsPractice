using EFCoreRelationshipsPractice.Model;
using System.Collections.Generic;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        private CompanyEntiy companyEntiy;
        private Task<CompanyEntiy?> foundCompany;

        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntiy companyEntiy)
        {
            Name = companyEntiy.Name;
            ProfileDto = companyEntiy.Profile != null ? new ProfileDto(companyEntiy.Profile) : null;
            EmployeesDto = companyEntiy.Employees?.Select(employeeEntity => new EmployeeDto(employeeEntity)).ToList();
        }

        //public CompanyDto(Task<CompanyEntiy?> foundCompany)
        //{
        //    this.foundCompany = foundCompany;
        //}

        public string Name { get; set; }

        public ProfileDto? ProfileDto { get; set; }

        public List<EmployeeDto>? EmployeesDto { get; set; }

        public CompanyEntiy ToEntity()
        {
            return new CompanyEntiy()
            {
                Name = this.Name,
                Profile = this.ProfileDto?.ToEntity(),
                Employees = this.EmployeesDto?.Select(item => item.ToEntity()).ToList(),
            };
        }
    }
}