using MeetSport.Options;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Threading.Tasks;

namespace MeetSport.Services.PasswordHasher
{
    public sealed class PasswordHasher : IPasswordHasher
    {
        private HashingOptions HashingOptions { get; }

        public PasswordHasher(IOptions<HashingOptions> hashingOptions)
        {
            HashingOptions = hashingOptions.Value;
        }

        public string Hash(string password)
        {
            /*
            using (Rfc2898DeriveBytes algorithm = new Rfc2898DeriveBytes(
              password,
              new byte[] {164,176,124,62,244,154,226,211,177,90,202,180,12,142,25,225},
              HashingOptions.Iterations,
              HashAlgorithmName.SHA512))
            {
                string key = Convert.ToBase64String(algorithm.GetBytes(KeySize));
                string salt = Convert.ToBase64String(algorithm.Salt);

                return $"{Options.Iterations}.{salt}.{key}";
            }*/
            var test = HashingOptions.Iterations;

            return "";
        }

        public (bool Verified, bool NeedsUpgrade) Check(string hash, string password)
        {
            var parts = hash.Split('.', 3);

            if (parts.Length != 3)
            {
                throw new FormatException("Unexpected hash format. " +
                  "Should be formatted as `{iterations}.{salt}.{hash}`");
            }

            var iterations = Convert.ToInt32(parts[0]);
            var salt = Convert.FromBase64String(parts[1]);
            var key = Convert.FromBase64String(parts[2]);

            var needsUpgrade = iterations != HashingOptions.Iterations;

            using (var algorithm = new Rfc2898DeriveBytes(
              password,
              salt,
              iterations,
              HashAlgorithmName.SHA512))
            {
                var keyToCheck = algorithm.GetBytes(10);

                var verified = keyToCheck.SequenceEqual(key);

                return (verified, needsUpgrade);
            }
        }
    }
}
