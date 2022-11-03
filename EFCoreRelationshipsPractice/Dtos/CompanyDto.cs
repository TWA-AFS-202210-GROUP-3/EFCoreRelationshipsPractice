using System.Collections.Generic;
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
        }

        public string Name { get; set; }

        public ProfileDto? Profile { get; set; }

        public List<EmployeeDto>? Employees { get; set; }

        public CompanyEntity ToEntity()
        {
            // var entity = new CompanyEntity();
            // entity.Name = Name;

            return new CompanyEntity()
            {
                Name = this.Name,
            };
        }

    }
}