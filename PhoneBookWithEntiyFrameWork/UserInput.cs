using HelperLibrary;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using ConsoleTableExt;
using System.Threading.Tasks;
using System.Reflection.PortableExecutable;

namespace PhoneBookWithEntiyFrameWork
{
    internal class UserInput
    {

        public void CreateMenu()
        {
            Console.WriteLine("Welcome to Phone Book");
            Console.WriteLine("1. Add Contact");
            Console.WriteLine("2. Update Contact");
            Console.WriteLine("3. Delete Contact");
            Console.WriteLine("4. Read Contact");
            Console.WriteLine("5. Exit");

            Console.WriteLine();

            Console.Write("Enter your choice: ");

            while (true)
            {
                string choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddContact();
                        break;
                    case "2":
                        UpdateContact();
                        break;
                    case "3":
                        DeleteContact();
                        break;
                    case "4":
                        ReadContacts();
                        break;
                    case "5":
                        Environment.Exit(0);
                        break;
                    default:
                        Console.WriteLine("Invalid Choice");
                        break;
                }

                Console.WriteLine();
                CreateMenu();
            }
        }

        private void AddContact()
        {
            using (var context = new ContactDbContext())
            {
                // Get user input for a new contact
                Console.Write("Enter name: ");
                string name = Console.ReadLine();
                int b = 0;
                while (name == "" || int.TryParse(name,out b))
                {
                    Console.WriteLine("Invalid Name");
                    Console.Write("Enter name: ");
                    name=Console.ReadLine();
                }

                Console.Write("Enter email: ");
                string email = Console.ReadLine();
                while (email == "" || !Helper.IsValidEmail(email))
                {
                    Console.WriteLine("Invalid Email");
                    Console.Write("Enter email: ");
                    email=Console.ReadLine();
                }
                

                Console.Write("Enter phone number: ");
                string phoneNumber = Console.ReadLine();
                int i = 0;
                while (phoneNumber == "" || !Helper.IsValidPhoneNumber(phoneNumber))
                {
                    Console.WriteLine("Invalid Phone Number");
                    Console.Write("Enter Number: ");
                     phoneNumber=Console.ReadLine();
                }


                // Create a new contact object
                var newContact = new Contact
                {
                    Name = name,
                    Email = email,
                    PhoneNumber = phoneNumber
                };

                // Add the new contact to the database
                context.AddContact(newContact);

                Console.WriteLine("Contact added successfully!");

                CreateMenu();
            }
        }

        private void UpdateContact()
        {
            using (var context = new ContactDbContext())
            {
                // Get user input for the contact ID to update
                Console.Write("Enter contact ID to update: ");
                int contactId;
                if (int.TryParse(Console.ReadLine(), out contactId))
                {
                    // Display valid property options
                    Console.WriteLine("Valid property options: All, Name, Email, PhoneNumber");

                    string propertyToUpdate;

                    do
                    {
                        // Get user input for the property to update
                        Console.Write("Enter property to update: ");
                        propertyToUpdate = Console.ReadLine();

                        // Validate the property choice
                        if (!IsValidProperty(propertyToUpdate))
                        {
                            Console.WriteLine("Invalid property. Please enter a valid property.");
                        }

                    } while (!IsValidProperty(propertyToUpdate));

                    // Get user input for the new value
                    Console.Write($"Enter new value for {propertyToUpdate}: ");
                    string newValue = Console.ReadLine();

                    // Update the contact
                    context.UpdateContact(contactId, propertyToUpdate, newValue);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid contact ID.");
                }
            }

        }

        //this function deletes the contact from the database
        private void DeleteContact()
        {
            using (var context = new ContactDbContext())
            {
                // Get user input for the contact ID to delete
                Console.Write("Enter contact ID to delete: ");
                int contactId;
                if (int.TryParse(Console.ReadLine(), out contactId))
                {
                    // Delete the contact
                    context.DeleteContact(contactId);
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid contact ID.");
                }
                CreateMenu();
            }
        }

        //this function reads the contact from the database
        private  void ReadContacts()
        {
            List<Contact> tableData = new List<Contact>();
            using (var context = new ContactDbContext())
            {
                // Retrieve contacts from the database
                List<Contact> contacts = context.GetContacts();

                foreach (var contact in contacts)
                {
                    
                         tableData.Add(new Contact
                         {
                             Id = contact.Id,
                             Name = contact.Name,
                             Email = contact.Email,
                             PhoneNumber = Helper.FormatPhoneNumber(contact.PhoneNumber)
                         });
                   
                }
                TableVisualisation.ShowTable(tableData);
            }
            CreateMenu();
        }


        //this function checks to see if the property is valid
        static bool IsValidProperty(string property)
        {
            // List of valid property options
            var validProperties = new List<string> { "All", "Name", "Email", "PhoneNumber" };

            // Check if the entered property is in the list of valid properties
            return validProperties.Contains(property, StringComparer.OrdinalIgnoreCase);
        }


    }
}
