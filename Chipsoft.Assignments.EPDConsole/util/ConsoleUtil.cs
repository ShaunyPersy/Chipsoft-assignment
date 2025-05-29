using System.Globalization;
using System.Text.RegularExpressions;

namespace Chipsoft.Assignments.EPDConsole.Util
{
    public static class ConsoleUtil
    {
        public static T ReadInput<T>(string prompt, Func<string, (bool IsValid, T ParsedValue)> validator)
        {
            while (true)
            {
                Console.Write(prompt);
                string input = Console.ReadLine()?.Trim();

                var (isValid, parsedValue) = validator(input);

                if (isValid)
                    return parsedValue;

                Console.WriteLine("Ongeldige invoer, probeer opnieuw.");
            }
        }

        public static string ReadNonEmptyString(string prompt)
        {
            return ReadInput(prompt, input =>
                (!string.IsNullOrEmpty(input), input));
        }

        public static DateTime ReadDate(string prompt)
        {
            return ReadInput(prompt, input =>
                (DateTime.TryParseExact(input, "dd/MM/yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date), date));
        }

        public static DateTime ReadDateAndTime(string prompt)
        {
            return ReadInput(prompt, input =>
                (DateTime.TryParseExact(input, "dd/MM/yyyy HH:mm", CultureInfo.InvariantCulture, DateTimeStyles.None, out var date), date));
        }

        public static int ReadInt(string prompt)
        {
            return ReadInput(prompt, input =>
                (int.TryParse(input, out var value), value));
        }
        public static int ReadChoice(string prompt, int min, int max)
        {
            return ReadInput(prompt, input =>
            {
                if (int.TryParse(input, out var value) && value >= min && value <= max)
                {
                    return (true, value);
                }
                return (false, default);
            });
        }

        public static bool Confirm(string prompt = "Weet u zeker dat u wilt doorgaan? (ja/nee): ")
        {
            return ReadInput(prompt, input =>
            {
                input = input?.Trim().ToLower();
                return (input == "ja" || input == "j", true);
            });
        }

        public static string ReadValidEmail(string prompt)
        {
            return ReadInput(prompt, input =>
                (IsValidEmail(input), input));
        }

        private static bool IsValidEmail(string email)
        {
            if (string.IsNullOrWhiteSpace(email))
                return false;

            string pattern = @"^[^@\s]+@[^@\s]+\.[^@\s]+$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
        }
    }
}