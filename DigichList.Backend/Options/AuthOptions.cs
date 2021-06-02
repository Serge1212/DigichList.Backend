using Microsoft.IdentityModel.Tokens;
using System.Text;

namespace DigichList.Backend.Options
{
    public class AuthOptions
    {
        public string Issuer { get; set; }
        public string Audience { get; set; }
        public string Secret { get; set; }
        public int TokenLifetime { get; set; }
        public SymmetricSecurityKey GetSymmetricSecurutyKey()
            => new(Encoding.ASCII.GetBytes(Secret));
    }
}
