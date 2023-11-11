using System.Configuration;
using HelperLibrary;
using Microsoft.EntityFrameworkCore;

namespace PhoneBookWithEntiyFrameWork
{
    internal class ContactDbContext : DbContext
    {
        //this is the constructor of the class
        public DbSet<Contact> Contacts { get; set; }

        //this is the connection string to connect to the database
        private static string ConnectionString = ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;

        //this function is used to configure the connection string
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }

        //this function returns the list of contacts from the database
        public List<Contact> GetContacts()
        {
            return Contacts.ToList();
        }

        //this function adds the contact to the database
        public void AddContact(Contact contact)
        {
            Contacts.Add(contact);
            SaveChanges();
        }

        //this function deletes the contact from the database
        public void DeleteContact(int contactId)
        {
            var contactToRemove = Contacts.Find(contactId);

            if (contactToRemove != null)
            {
                Contacts.Remove(contactToRemove);
                SaveChanges();
                Console.WriteLine("Contact deleted successfully!");
            }
            else
            {
                Console.WriteLine("Invalid contact ID. Contact not found.");
            }
        }


        //this function updates the contact in the database
         public void UpdateContact(int contactId, string propertyToUpdate, string newValue)
        {
            var contactToUpdate = Contacts.Find(contactId);

            if (contactToUpdate != null)
            {
                // Update the selected property or all properties
                if (propertyToUpdate.ToLower() == "all")
                {
                    contactToUpdate.Name = newValue;
                    contactToUpdate.Email = newValue;
                    contactToUpdate.PhoneNumber = newValue;
                }
                else
                {
                    //switch statement to update the property
                    switch (propertyToUpdate.ToLower())
                    {

                        case "name":
                            contactToUpdate.Name = newValue;
                            break;
                        case "email":
                            if (Helper.IsValidEmail(newValue))
                            {
                                contactToUpdate.Email = newValue;
                            }
                            else
                            {
                                Console.WriteLine("Invalid email format. Please enter a valid email.");
                                return;
                            }
                            break;
                        case "phonenumber":
                            if (Helper.IsValidPhoneNumber(newValue))
                            {
                                contactToUpdate.PhoneNumber = newValue;
                            }
                            else
                            {
                                Console.WriteLine("Invalid phone number format. Please enter a valid phone number.");
                                return;
                            }
                            break;
                        default:
                            Console.WriteLine("Invalid property to update. Valid options are: All, Name, Email, PhoneNumber.");
                            return;
                    }
                }

                SaveChanges();
                Console.WriteLine("Contact updated successfully!");
            }
            else
            {
                Console.WriteLine("Invalid contact ID. Contact not found.");
            }
        }
    
    }
}
