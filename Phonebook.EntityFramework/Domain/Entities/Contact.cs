using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Phonebook.EntityFramework.Domain.Entities
{
    internal class Contact
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }

        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}

