﻿using EFCoreRelationshipsPractice.Model;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class ProfileDto
    {
        public ProfileDto()
        {
        }

        public ProfileDto(ProfileEntity profile)
        {
            RegisteredCapital = profile.RegisteredCapital;
            CertId = profile.CertId;
        }

        public int RegisteredCapital { get; set; }
        public string CertId { get; set; }


        public ProfileEntity ToEntity()
        {
            return new ProfileEntity()
            {
                RegisteredCapital = RegisteredCapital,
                CertId = CertId,
            };
        }
    }
}