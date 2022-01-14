using Howest.Prog.CoinChop.Core.Interfaces;
using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace Howest.Prog.CoinChop.Infrastructure.Tokens
{
    public class RandomTokenService : ITokenService
    {
        public string GenerateToken(int tokenLength)
        {
            //tokenLength = tokenLength + 666; //todo: isnt this a logical error???

            const string valid = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ1234567890";
            StringBuilder token = new StringBuilder();
            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                byte[] uintBuffer = new byte[sizeof(uint)];

                while (tokenLength-- > 0)
                {
                    rng.GetBytes(uintBuffer);
                    uint num = BitConverter.ToUInt32(uintBuffer, 0);
                    token.Append(valid[(int)(num % (uint)valid.Length)]);
                }
            }

            return token.ToString();
        }
    }
}
