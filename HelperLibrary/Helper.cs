namespace HelperLibrary
{
    public class Helper
    {

        // Implement email validation logic
        public static bool IsValidEmail(string email)
        {
           
            // For simplicity, this example checks for a basic email format
            return email.Contains("@");
        }

        //Implments Valid phone number logic
        public static bool IsValidPhoneNumber(string phoneNumber)
        {
            // Implement phone number validation logic
            return long.TryParse(phoneNumber, out _);
        }

        //this method will check to see if the user input is an integer
        public static int GetIntInput(string prompt)
        {
            while (true)
            {
                Console.Write(prompt);

                if (int.TryParse(Console.ReadLine(), out int result))
                {
                    return result;
                }
                else
                {
                    Console.WriteLine("Invalid input. Please enter a valid integer.");
                }
            }
        }

        public static string FormatPhoneNumber(string phoneNumber)
        {
            // Insert dashes into the phone number
            if (phoneNumber.Length == 10)
            {
                 return $"{phoneNumber.Substring(0, 3)}-{phoneNumber.Substring(3, 3)}-{phoneNumber.Substring(6)}";
    
            }
            else
            {
                // Handle other phone number formats as needed
                return phoneNumber;
            }
        }

    }
}