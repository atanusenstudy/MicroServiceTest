using System;
using System.Collections.Generic;
using System.Linq;


namespace TaskService.BLL.Facade
{
    public class UniqueGenerator
    {
        private static readonly Random random = new Random();

        public List<string> GenerateUniqueStrings(int numberOfStrings)
        {
            if (numberOfStrings <= 0)
            {
                throw new ArgumentException("Number of strings must be greater than 0.");
            }

            var uniqueStrings = new HashSet<string>();
            var generatedStrings = new List<string>();

            while (uniqueStrings.Count < numberOfStrings)
            {
                string uniqueString = GenerateRandomString();

                // Check if the generated string is not in the set of unique strings
                if (uniqueStrings.Add(uniqueString))
                {
                    generatedStrings.Add(uniqueString);
                }
            }

            return generatedStrings;
        }

        private string GenerateRandomString()
        {
            const string allowedChars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            int stringLength = random.Next(1, 6); // Random length from 1 to 5 characters

            char[] chars = new char[stringLength];
            for (int i = 0; i < stringLength; i++)
            {
                chars[i] = allowedChars[random.Next(0, allowedChars.Length)];
            }

            return new string(chars);
        }
    }
}

