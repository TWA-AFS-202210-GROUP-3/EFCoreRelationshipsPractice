using EFCoreRelationshipsPractice.Model;

namespace EFCoreRelationshipsPractice.Dtos
{
    public class EmployeeDto
    {
        public EmployeeDto()
        {
        }

        public EmployeeDto(EmployeesEntity employeesEntity)
        {
            Name = employeesEntity.Name;
            Age = employeesEntity.Age;
        }

        public string Name { get; set; }

        public int Age { get; set; }

        public EmployeesEntity ToEntity()
        {
            return new EmployeesEntity()
            {
                Name = this.Name,
                Age = this.Age,
            };
        }
    }
}