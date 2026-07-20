using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using Phonebook.Data;
using Phonebook.Models;

namespace Phonebook
{
    public class ContactService
    {
        private readonly PhonebookContext _context;

        public ContactService(PhonebookContext phonebookContext)
        {
            _context = phonebookContext;
        }

        public List<Contact> GetAll()
        {
            try
            {
                var list = _context.Contacts.ToList();
                return list;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nDatabase connection/query error: {ex.Message}");
                return new List<Contact>();
            }
        }

        public List<Contact> GetContactByCategory(string category)
        {
            try
            {
                if (string.IsNullOrEmpty(category))
                {
                    return GetAll();
                }
                var contacts = GetAll();
                var result = contacts
                            .Where(c => string.Equals(c.Category, category, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nDatabase connection/query error: {ex.Message}");
                return new List<Contact>();
            }
        }

        public Contact? GetContactByNumber(string phoneNumber)
        {
            try
            {
                var contacts = GetAll(); 
                var contact = contacts.FirstOrDefault(x => x.PhoneNumber == phoneNumber);
                return contact;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nDatabase connection/query error: {ex.Message}");
                return null;
            }
        }

        public List<Contact> GetContactByName(string name)
        {
            try
            {
                var contacts = GetAll();
                var result = contacts
                            .Where(c => c.Name != null && c.Name.Contains(name, StringComparison.OrdinalIgnoreCase))
                            .ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nDatabase connection/query error: {ex.Message}");
                return new List<Contact>();
            }
        }

        public bool AddContact(string name, string phoneNumber, string? email = null, string? category = null) 
        {
            try
            {
                bool exists = _context.Contacts.Any(c => c.PhoneNumber == phoneNumber);

                if (exists)
                {
                    Console.WriteLine("Phone number already exists!");
                    return false;
                }

                var contact = new Contact
                {
                    Name = name,
                    PhoneNumber = phoneNumber,
                    Email = email,
                    Category = category
                };

                _context.Add(contact);
                _context.SaveChanges();
                return true;
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"\nConcurrency error: {ex.Message}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"\nDatabase update error: {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected database error: {ex.Message}");
                return false;
            }
        }

        public bool DeleteContact(int contactId)
        {
            try
            {
                var contact = _context.Contacts.FirstOrDefault(c => c.Id == contactId);

                if (contact != null)
                {
                    _context.Remove(contact);
                    _context.SaveChanges();
                    return true;
                }
                else
                {
                    Console.WriteLine("\nContact not found!");
                    return false;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"\nConcurrency error: {ex.Message}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"\nDatabase update error: {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected database error: {ex.Message}");
                return false;
            }
        }

        public bool UpdateContactInfo(int id, string name, string phoneNumber, string? email = null, string? category = null)
        {
            try
            {
                var contact = _context.Contacts.FirstOrDefault(c => c.Id == id);

                if (contact != null)
                {
                    contact.Name = name;
                    contact.Email = email;
                    contact.PhoneNumber = phoneNumber;
                    contact.Category = category;
                    _context.SaveChanges();

                    return true;
                }
                else
                {
                    Console.WriteLine("\nContact not found!");
                    return false;
                }
            }
            catch (DbUpdateConcurrencyException ex)
            {
                Console.WriteLine($"\nConcurrency error: {ex.Message}");
                return false;
            }
            catch (DbUpdateException ex)
            {
                Console.WriteLine($"\nDatabase update error: {ex.InnerException?.Message ?? ex.Message}");
                return false;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nUnexpected database error: {ex.Message}");
                return false;
            }
        }

        public List<string> GetCategories()
        {
            try
            {
                var contacts = GetAll();

                var result = contacts
                            .Where(n => !string.IsNullOrEmpty(n.Category))
                            .OrderBy(n => n.Category)
                            .Select(n => n.Category!)
                            .Distinct()
                            .ToList();
                return result;
            }
            catch (Exception ex)
            {
                Console.WriteLine($"\nDatabase connection/query error: {ex.Message}");
                return new List<string>();
            }
        }
    }
}
