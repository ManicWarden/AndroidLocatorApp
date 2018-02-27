using Akka.Actor;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Text.RegularExpressions;

namespace Mobile_Locator_App.Code
{
    class ValidationActor : UntypedActor
    {


        bool IsValidEmail(string email)
        {
            // I think the try sends a message to the email address passed, if it succeeds then the function returns a true, 
            // if it doesn't succeed the the try fails and the catch returns a false. Gotten from https://stackoverflow.com/questions/1365407/c-sharp-code-to-validate-email-address


            if (email.Contains("\"\"")) // if the string email contains ""
            {
                if (email.Substring(email.LastIndexOf('"') + 1).Contains("@")) // if an @ symbol comes after the "" then it can be valid
                {
                    return true;
                }

                else
                {
                    return false;
                }
            }

            try
            {
                var addr = new MailAddress(email); // Doesn't work for Universal Windows Phone, may be missing a reference
                return addr.Address == email;
            }
            catch
            {
                return false;
            }
        }

        bool IsValidPhoneNo(string PhoneNo)
        {
            Console.WriteLine("******************************************Phone Validation Phone Number length = " + PhoneNo.Length);
            //Jury rigged for different sized numbers until the text message verification is coded
            // as different areas have different mobile phone number lengths
            if (PhoneNo.Length == 9)
            {
                Console.WriteLine("******************************************Phone Length == 9");
                return Regex.Match(PhoneNo, @"^[0-9]{9}$").Success; // {} dictates the length of the number

            }
            else if (PhoneNo.Length == 10)
            {
                Console.WriteLine("******************************************Phone Length == 10");
                return Regex.Match(PhoneNo, @"^[0-9]{10}$").Success;


            }
            else if (PhoneNo.Length == 11)
            {
                Console.WriteLine("******************************************Phone Length == 11");
                return Regex.Match(PhoneNo, @"^[0-9]{11}$").Success;


            }
            else if (PhoneNo.Length == 12)
            {
                Console.WriteLine("******************************************Phone Length == 12");
                return Regex.Match(PhoneNo, @"^[0-9]{12}$").Success;


            }

            else if (PhoneNo.Length == 13)
            {
                Console.WriteLine("******************************************Phone Length == 13");
                return Regex.Match(PhoneNo, @"^[0-9]{13}$").Success;


            }
            else
            {
                Console.WriteLine("******************************************Phone Length = nothing");
                return false; // if a number is entered that is not between 9 and 13 digits or if the string has characters other than numbers in it


            }
        }

        protected override void OnReceive(object message)
        {

        }
    }
}
