using System.Text.RegularExpressions;

namespace App.API.GloabalClasses
{
    public class Validation
    {
        public static bool ValidateEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email)) return false;

            
            var pattern = @"^[a-zA-Z0-9._%+-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,}$";
            var regex = new Regex(pattern);
            return regex.IsMatch(email);
        }

        public static bool ValidateInteger(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;

            var pattern = @"^[0-9]+$"; 
            var regex = new Regex(pattern);
            return regex.IsMatch(number);
        }

        public static bool ValidateFloat(string number)
        {
            if (string.IsNullOrWhiteSpace(number)) return false;

            var pattern = @"^[0-9]+(?:\.[0-9]+)?$"; 
            var regex = new Regex(pattern);
            return regex.IsMatch(number);
        }

        public static bool IsNumber(string number)
        {
            return ValidateInteger(number) || ValidateFloat(number);
        }

        public static bool ValidatePassword(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            var pattern = @"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]{8,}$";
            var regex = new Regex(pattern);
            return regex.IsMatch(password);
        }

        public static bool ValidateSubDomain(string subdomain)
        {
            if (string.IsNullOrWhiteSpace(subdomain)) return false;

            var pattern = @"^[a-z0-9](?:[a-z0-9\-]{1,61}[a-z0-9])?$";
            var regex = new Regex(pattern);
            return regex.IsMatch(subdomain);
        }
    }
}