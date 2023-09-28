namespace Compute.Microservice.Helpers
{
    /// <summary>
    /// Common helper class for const/readonly variables
    /// </summary>
    public static class ConstHelper
    {
        public static readonly string charString = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
        public static readonly string InvalidInput = "Input must be between {0} to {1}";
        public static readonly string ResponseMessage = "Execution Time: {0} Seconds";
    }
}
