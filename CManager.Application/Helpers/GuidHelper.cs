using System;

namespace CManager.Application.Helpers
{
    public static class GuidHelper
    {
        public static Guid GenerateGuid()
        {
            return Guid.NewGuid();
        }

        public static bool IsValidGuid(string guidString)
        {
            return Guid.TryParse(guidString, out _);
        }

        public static Guid ParseGuid(string guidString)
        {
            if (IsValidGuid(guidString))
            {
                return Guid.Parse(guidString);
            }
            throw new FormatException("Invalid GUID format.");
        }
    }
}
