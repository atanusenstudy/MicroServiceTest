using System.Security.Cryptography;
using System.Text;

namespace Compute.Microservice.Helpers
{
    public static class CodeHelper
    {
        /// <summary>
        /// This method is responsible to generate unique codes
        /// </summary>
        /// <param name="size">size is code length(eg. 5)</param>
        /// <param name="chars">chars is alphanumeric combination in shuffled form</param>
        /// <param name="validId">validId is HashSet<string> obj to make sure unique code in fast and efficient way</param>
        /// <returns>string : alphanumneric unique code</returns>
        public static string GetUniqueKey(int size, char[] chars,HashSet<string> validId)
        {
            byte[] data = new byte[4 * size];
            using (var crypto = RandomNumberGenerator.Create())
            {
                crypto.GetBytes(data);
            }
            StringBuilder result = new StringBuilder(size);
            for (int i = 0; i < size; i++)
            {
                var rnd = BitConverter.ToUInt32(data, i * 4);
                var idx = rnd % chars.Length;

                result.Append(chars[idx]);
            }
            if (!validId.Add(result.ToString()))
            {
                chars = Shuffle(ConstHelper.charString);
                string outcome = GetUniqueKey(size, chars, validId);
                return outcome;
            }
            else
                return result.ToString();
        }
        /// <summary>
        /// This method is used to shuffle alphanumeric string to help unique code generation process
        /// </summary>
        /// <param name="str">str is combination of alphanumeric string</param>
        /// <returns>char[] : in shuffled form</returns>
        public static char[] Shuffle(string str)
        {
            char[] array = str.ToCharArray();
            Random rng = new Random();
            int n = array.Length;
            while (n > 1)
            {
                n--;
                int k = rng.Next(n + 1);
                var value = array[k];
                array[k] = array[n];
                array[n] = value;
            }
            return array;
        }
    }
}
