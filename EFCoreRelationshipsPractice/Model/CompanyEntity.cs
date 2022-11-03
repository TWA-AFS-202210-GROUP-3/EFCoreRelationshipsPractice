namespace EFCoreRelationshipsPractice.Model
{
    public class CompanyEntity
    {
        public CompanyEntity()
        {
        }

        public ProfileEntity? Profile { get; set; }

        public int Id { get; set; }

        public string Name { get; set; }
    }
}
