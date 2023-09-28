using System;
using System.Collections.Concurrent;
using System.Linq;
using System.Threading.Tasks;

namespace TaskService.BLL.Facade
{

    public class UniqueStringGenerator
    {
        private  readonly char[] Characters = "abcdefghijklmnopqrstuvwxyz".ToCharArray();

        public  List<string> GenerateUniqueStrings(int count, int maxLength)
        {
            if (count <= 0 || maxLength <= 0)
            {
                throw new ArgumentException("Invalid input parameters.");
            }

            var uniqueStrings = new List<string>();
            var usedStrings = new HashSet<string>();
            var random = new Random();

            while (uniqueStrings.Count < count)
            {
                var newString = GenerateRandomString(random, maxLength);

                if (!usedStrings.Contains(newString))
                {
                    uniqueStrings.Add(newString);
                    usedStrings.Add(newString);
                }
            }

            return uniqueStrings;
        }

        private  string GenerateRandomString(Random random, int maxLength)
        {
            var length = random.Next(1, maxLength + 1);
            var result = new char[length];

            for (var i = 0; i < length; i++)
            {
                result[i] = Characters[random.Next(Characters.Length)];
            }

            return new string(result);
        }

    }
}
