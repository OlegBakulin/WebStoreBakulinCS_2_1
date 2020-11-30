using System;
using WebStoreCoreApplication.Domain.Entities.Base;

namespace WebStoreCoreApplication.Domain.Entities
{
    public class Employee : NameEntity
    {
        public string Surname { get; set; }

        public string Patronymic { get; set; }

        public int Age { get; set; }

        public DateTime EmployementDate { get; set; }
    }
}
