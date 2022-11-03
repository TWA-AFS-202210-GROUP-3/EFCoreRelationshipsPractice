using EFCoreRelationshipsPractice.Model;
using System.Collections.Generic;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class CompanyDto
    {
        private CompanyEntiy companyEntiy;

        public CompanyDto()
        {
        }

        public CompanyDto(CompanyEntiy companyEntiy)
        {
            Name = companyEntiy.Name;
        }

        public string Name { get; set; }

        public ProfileDto? Profile { get; set; }

        public List<EmployeeDto>? Employees { get; set; }

    }
}