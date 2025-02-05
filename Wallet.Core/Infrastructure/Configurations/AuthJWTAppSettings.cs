namespace Wallet.Core.Infrastructure.Configurations
{
    public class AuthJWTAppSettings
    {
        public required string Key { get; set; }
        public required string Issuer { get; set; }
        public required string Audience { get; set; }
    }
}
