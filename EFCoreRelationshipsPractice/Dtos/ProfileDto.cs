using EFCoreRelationshipsPractice.Models;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        public ProfileDto() {}

        public ProfileDto(ProfileEntity profileEntity)
        {
            this.CertId = profileEntity.CertId;
            this.RegisteredCapital = profileEntity.RegisteredCapital;
        }

        public int RegisteredCapital { get; set; }

        public string CertId { get; set; }

        public ProfileEntity ToEntity()
        {
            return new ProfileEntity()
            {
                RegisteredCapital = this.RegisteredCapital,
                CertId = this.CertId
            };
        }
    }
}