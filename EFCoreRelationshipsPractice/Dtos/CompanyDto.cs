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
            // ProfileDto.RegisteredCapital = companyEntiy.Profile.RegisteredCapital;
            //profiledto.certid = companyEntiy.profile.cerid;
            ProfileDto = companyEntiy.Profile !=null? new ProfileDto(companyEntiy.Profile): null;

        }

        public string Name { get; set; }

        public ProfileDto? ProfileDto { get; set; }

        public List<EmployeeDto>? Employees { get; set; }

        public CompanyEntiy ToEntity()
        {
            return new CompanyEntiy()
            {
                Name = this.Name,
                Profile = this.ProfileDto?.ToEntity(),
            };
        }
    }
}