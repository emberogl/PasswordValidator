using System.Text.RegularExpressions;

namespace PasswordValidator
{
    internal class Program
    {
        static void Main()
        {
            Controller();
        }

        #region Controller
        static void Controller()
        {
            // Fetch the password input from user
            string Password = FetchPassword("Hej \n \n Angiv et adgangskode imellem 12 & 64 characters \n Adgangskoden skal have både uppercase og lowercase characters \n Adgangskoden skal have numre og bogstaver \n Adgangskoden skal have mindst én speciel character \n \n");
            // Get the strength of the password fetched
            int Strength = CheckPassword(Password);
            // Output how well the user's password is
            OutputStrength(Strength);
        }
        #endregion

        #region Model
        static int CheckPassword(string Password)
        {
            int StrengthValue = 2;
            /// If password is not empty
            if (Password != null)
            {
                /// If password is between 12 - 64
                if (Password.Length < 64 && Password.Length > 12)
                {
                    /// If password contains at least one uppercase and one lowercase
                    if (Password.Any(char.IsUpper) && Password.Any(char.IsLower))
                    {
                        /// If password contains at least one digit and one letter
                        if (Password.Any(char.IsDigit) && Password.Any(char.IsLetter)) 
                        {
                            /// If password contains at least one special character
                            //if (Password.Any(ch => !char.IsLetterOrDigit(ch)))
                            int Special = 0;
                            for (int i = 0; i < Password.Length; i++)
                            {
                                if (!char.IsLetterOrDigit(Password[i]))
                                {
                                    Special++;
                                }
                            }
                            if (Special > 0)
                            {
                                // Using regular expressions, checking if a sequence of 4 letters/strings are in the password, or if 4 ascending/descending numbers are in the password
                                if (Regex.IsMatch(Password, "(.)\\1{" + 3 + "}") || Regex.IsMatch(Password, "(0123|1234|2345|3456|4567|5678|6789|3210|4321|5432|6543|7654|8765|9876)"))
                                {
                                    // Password is OK but weak
                                    return StrengthValue -= 1;
                                }
                                else
                                {
                                    // Password is OK
                                    return StrengthValue;
                                }
                            }
                            else
                            {
                                // Password must contain at least one special character
                                return StrengthValue -= 2;
                            }
                        }
                        else
                        {
                            // Password must contain at least one letter and one digit
                            return StrengthValue -= 2;
                        }
                    }
                    else
                    {
                        // Password must contain at least one upper and one lower
                        return StrengthValue -= 2;
                    }
                }
                else
                {
                    // Password must be between 12 and 64
                    return StrengthValue -= 2;
                }
            }
            else
            {
                // Something went wrong (string null)?
                return StrengthValue -= 3;
            }
        }
        #endregion

        #region View
        static string FetchPassword(string text)
        {
            Console.Write(text);
            string Input = Console.ReadLine()!;
            return Input;
        }
        static void OutputStrength(int strength)
        {
            // strength -1: no password
            // strength 0: bad password
            // strength 1: ok but weak password
            // strength 2: good password
            switch(strength)
            {
                case -1:
                    Console.Clear();
                    Main();
                    break;
                case 0:
                    Console.ForegroundColor = ConsoleColor.DarkRed;
                    Console.WriteLine("Din adgangskode er ikke gyldig");
                    Console.ReadKey();
                    Console.ResetColor();
                    Console.Clear();
                    Main();
                    break;
                case 1:
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.WriteLine("Din adgangskode er gyldig men svag");
                    Console.ReadKey();
                    Console.ResetColor();
                    Console.Clear();
                    Main();
                    break;
                case 2:
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.WriteLine("Din adgangskode er gyldig");
                    Console.ReadKey();
                    Console.ResetColor();
                    Console.Clear();
                    Main();
                    break;
            }
        }
        #endregion
    }
}