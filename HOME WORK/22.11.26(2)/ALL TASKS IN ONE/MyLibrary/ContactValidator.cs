using System.Text.RegularExpressions;

namespace MyLibrary
{
    public static class ContactValidator
    {
        public static bool IsValidName(string name)
        {
            return Regex.IsMatch(name, @"^[a-zA-Zа-яА-ЯёЁ\s]+$");
        }

        public static bool IsValidAge(string age)
        {
            return Regex.IsMatch(age, @"^\d+$");
        }

        public static bool IsValidPhone(string phone)
        {
            return Regex.IsMatch(phone, @"^\+?\d{1,3}[-\s]?\(?\d{3}\)?[-\s]?\d{3}[-\s]?\d{2}[-\s]?\d{2}$");
        }

        public static bool IsValidEmail(string email)
        {
            return Regex.IsMatch(email, @"^[^@\s]+@[^@\s]+\.[^@\s]+$");
        }
    }
}