using System.Text;
using Microsoft.Extensions.DependencyInjection;
using PhoneBook.m1chael888.Infrastructure;
using PhoneBook.m1chael888.Views;
using PhoneBook.m1chael888.Controllers;
using PhoneBook.m1chael888.Services;
using PhoneBook.m1chael888.Repositories;
using PhoneBook.m1chael888.Models;

namespace PhoneBook.m1chael888
{
    internal class Program
    {
        static void Main(string[] args)
        {
            Console.OutputEncoding = Encoding.UTF8;
            
            var collection = new ServiceCollection();

            collection.AddScoped<IRouter, Router>();
            collection.AddScoped<IDbSeeder, DbSeeder>();
            collection.AddScoped<ContactController>();
            collection.AddScoped<IContactView, ContactView>();
            collection.AddScoped<IContactService, ContactService>();
            collection.AddScoped<IContactRepository, ContactRepository>();

            var provider = collection.BuildServiceProvider();

            var seeder = provider.GetRequiredService<IDbSeeder>();
            if (seeder.ShouldSeed<Contact>()) seeder.Seed();

            var router = provider.GetRequiredService<IRouter>();
            var contactController = provider.GetRequiredService<ContactController>();
            var contactView = provider.GetRequiredService<IContactView>();

            router.Route(contactController, contactView);
        }
    }
}