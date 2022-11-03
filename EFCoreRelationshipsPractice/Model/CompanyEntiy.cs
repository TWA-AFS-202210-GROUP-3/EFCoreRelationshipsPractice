using EFCoreRelationshipsPractice.Dtos;

namespace EFCoreRelationshipsPractice.Model
{
    public class CompanyEntiy
    {
        // same with the table in database
        public CompanyEntiy()
        {

        }
        public int Id { get; set; }
        public string Name { get; set; }

        public ProfileEntity? Profile { get; set; }

    }
}