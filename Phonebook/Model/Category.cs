using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace Phonebook.Model
{
    [Index(nameof(CategoryName), IsUnique = true)]
    internal class Category
    {
        public int CategoryId { get; set; }
        public required string CategoryName { get; set; }
    }
}
