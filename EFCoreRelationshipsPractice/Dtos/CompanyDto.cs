using System.Collections.Generic;
using EFCoreRelationshipsPractice.Models;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntity companyEntity)
        {
            this.Name = companyEntity.Name;
            this.ProfileDto = companyEntity.Profile != null ? new ProfileDto(companyEntity.Profile) : null;
            this.EmployeesDto = companyEntity.Employees?.Select(item => new EmployeeDto(item)).ToList();
        }

        public string Name { get; set; }

        public ProfileDto? ProfileDto { get; set; }

        public List<EmployeeDto>? EmployeesDto { get; set; }

        public CompanyEntity ToEntity()
        {
            return new CompanyEntity()
            {
                Name = this.Name,
                Profile = this.ProfileDto?.ToEntity(),
                Employees = this.EmployeesDto?.Select(item => item.ToEntity()).ToList(),
            };
        }
    }
}