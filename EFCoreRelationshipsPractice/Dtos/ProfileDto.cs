using EFCoreRelationshipsPractice.Model;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        private ProfileEntity? profile;

        public ProfileDto()
        {
        }

        public ProfileDto(ProfileEntity? profile)
        {
            RegisteredCapital = profile.RegisteredCapital;
            CertId = profile.CerId;
        }

        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }

        public ProfileEntity ToEntity()
        {
            return new ProfileEntity()
            {
                RegisteredCapital = RegisteredCapital,
                CerId = CertId,
            };
        }
    }
}