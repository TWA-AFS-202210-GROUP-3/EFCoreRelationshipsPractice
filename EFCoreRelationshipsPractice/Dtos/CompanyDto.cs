using System.Collections.Generic;
using System.Runtime.InteropServices.ComTypes;
using EFCoreRelationshipsPractice.Model;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntity companyEntity)
        {
            Name = companyEntity.Name;
            ProfileDtos = companyEntity.Profile != null ? new ProfileDto(companyEntity.Profile) : null;
            Employees = companyEntity.Employees?.Select(_ => new EmployeeDto(_)).ToList();
        }

        public string Name { get; set; }

        public ProfileDto? ProfileDtos { get; set; }

        public List<EmployeeDto>? Employees { get; set; }

        public CompanyEntity ToEntity()
        {
            return new CompanyEntity()
            {
                Name = this.Name,
                Profile = ProfileDtos?.ToEntity(),
                Employees = Employees?.Select(_ => _.ToEntity()).ToList(),
            };
        }
    }
}