using Newtonsoft.Json;

namespace Compute.Microservice.Helpers
{
    public static class FileHelper
    {
        /// <summary>
        /// This method is to save serialized object in form of json file
        /// </summary>
        /// <param name="validId">HastSet<string> : to be serialize in physical form of json file</param>
        public static async void WriteToFile(HashSet<string> validId)
        {
            using (StreamWriter writer = System.IO.File.CreateText("unique.json"))
            {
                await writer.WriteLineAsync(JsonConvert.SerializeObject(validId));
            }
        }
        /// <summary>
        /// This method is to deserialized object in from json file
        /// </summary>
        /// <returns>HashSet<string></returns>
        public static HashSet<string> ReadFile()
        {
            try
            {
                using (StreamReader r = new StreamReader("unique.json"))
                {
                    string json = r.ReadToEnd();
                    if (string.IsNullOrEmpty(json))
                    {
                        return new HashSet<string>();
                    }
                    else
                        return JsonConvert.DeserializeObject<HashSet<string>>(json);
                }
            }
            catch (Exception ex)
            {
                return new HashSet<string>();
            }
        }
    }
}
