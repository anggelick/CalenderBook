using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

public class Program
{
    //  Klass för kontaktinformation
    public class BaseContact
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string PhoneNumber { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }
    }

    // Klass för hantering av kontakter
    public class AddressBook
    {
        public List<BaseContact> contacts;

        public AddressBook()
        {
            // Skapar en ny lista för kontakter
            contacts = new List<BaseContact>();

            // Laddar in kontakter vid uppstart
            LoadContacts();
        }

        /// <summary>
        /// Ny kontakt i adressboken
        /// </summary>
        public void AddContact(BaseContact contact)
        {
            contacts.Add(contact);
            SaveContacts(); 
            // Spara kontakter till filen efter att ha lagt till en ny kontakt
        }

        /// <summary>
        /// Listar upp alla kontakter
        /// </summary>
        public void ListAllContacts()
        {
            Console.WriteLine("Alla kontakter:");
            foreach (var contact in contacts)
            {
                Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}, Telefon: {contact.PhoneNumber}, E-post: {contact.Email}, Adress: {contact.Address}");
            }
        }

        /// <summary>
        /// Visar information om en specifik kontakt (baserat på e-postadress)
        /// </summary>
        public void ShowContactDetails(string email)
        {
            var contact = contacts.FirstOrDefault(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));

            if (contact != null)
            {
                Console.WriteLine($"Detaljer för kontakt med e-postadress {email}:");
                Console.WriteLine($"Namn: {contact.FirstName} {contact.LastName}");
                Console.WriteLine($"Telefon: {contact.PhoneNumber}");
                Console.WriteLine($"E-post: {contact.Email}");
                Console.WriteLine($"Adress: {contact.Address}");
            }
            else
            {
                Console.WriteLine($"Kontakt med e-postadress {email} hittades inte.");
            }
        }

        /// <summary>
        /// Tar bort en kontakt (baserat på e-postadress)
        /// </summary>
        public void RemoveContact(string email)
        {
            contacts.RemoveAll(c => c.Email.Equals(email, StringComparison.OrdinalIgnoreCase));
            SaveContacts(); 
            // Spara kontakter efter att ha tagit bort en kontakt
        }

        /// <summary>
        /// Spara kontakter till contacts
        /// </summary>
        public void SaveContacts()
        {
            string json = JsonConvert.SerializeObject(contacts, Formatting.Indented);
            File.WriteAllText("contacts.json", json);
        }

        /// <summary>
        /// Läs in kontakter vid uppstart
        /// </summary>
        public void LoadContacts()
        {
            if (File.Exists("contacts.json"))
            {
                string json = File.ReadAllText("contacts.json");
                contacts = JsonConvert.DeserializeObject<List<BaseContact>>(json);
            }
        }

        public class Contact : BaseContact
        {
            public string FirstName { get; set; }
            public string LastName { get; set; }
            public string PhoneNumber { get; set; }
            public string Email { get; set; }
            public string Address { get; set; }
        }
    }

    public static void Main()
    {
        var addressBook = new AddressBook();

        while (true)
        {
            Console.WriteLine("\nVälj en åtgärd:");
            Console.WriteLine("1. Lägg till ny kontakt");
            Console.WriteLine("2. Lista alla kontakter");
            Console.WriteLine("3. Visa detaljer för en kontakt");
            Console.WriteLine("4. Ta bort en kontakt");
            Console.WriteLine("5. Avsluta");

            string choice = Console.ReadLine();

            switch (choice)
            {
                case "1":
                    Console.WriteLine("Ange förnamn:");
                    string firstName = Console.ReadLine();

                    Console.WriteLine("Ange efternamn:");
                    string lastName = Console.ReadLine();

                    Console.WriteLine("Ange telefonnummer:");
                    string phoneNumber = Console.ReadLine();

                    Console.WriteLine("Ange e-postadress:");
                    string email = Console.ReadLine();

                    Console.WriteLine("Ange adress:");
                    string address = Console.ReadLine();

                    var newContact = new BaseContact
                    {
                        FirstName = firstName,
                        LastName = lastName,
                        PhoneNumber = phoneNumber,
                        Email = email,
                        Address = address
                    };

                    addressBook.AddContact(newContact);
                    Console.WriteLine("Kontakt tillagd!");
                    break;

                case "2":
                    addressBook.ListAllContacts();
                    break;

                case "3":
                    Console.WriteLine("Ange e-postadress för kontakten du vill visa:");
                    string emailToShow = Console.ReadLine();
                    addressBook.ShowContactDetails(emailToShow);
                    break;

                case "4":
                    Console.WriteLine("Ange e-postadress för kontakten du vill ta bort:");
                    string emailToRemove = Console.ReadLine();
                    addressBook.RemoveContact(emailToRemove);
                    Console.WriteLine("Kontakt borttagen!");
                    break;

                case "5":
                    Environment.Exit(0);
                    break;

                default:
                    Console.WriteLine("Ogiltigt val. Försök igen.");
                    break;
            }
        }
    }
}
