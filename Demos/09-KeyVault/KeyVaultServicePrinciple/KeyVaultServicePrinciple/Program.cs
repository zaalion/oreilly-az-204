using Microsoft.Azure.KeyVault;
using Microsoft.IdentityModel.Clients.ActiveDirectory;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthenticationContext = Microsoft.IdentityModel.Clients.ActiveDirectory.AuthenticationContext;

namespace KeyVaultServicePrinciple
{
    class Program
    {
        static string appId = "d9c2db92-e1d9-4021-b4c9-771541d05389";
        static string appSecret = "9ps8Q~aKg7kaPtN7EvcYzsM0lbzd6Dgv2CG9jb6i";
        static string tenantId = "0ec02b79-d89f-48c4-9870-da4a7498d887";

        static void Main(string[] args)
        {
            var kv = new KeyVaultClient(GetAccessToken);
            //https://keyvault-or204demo.vault.azure.net/
            var secret = kv.GetSecretAsync("https://vaultordemo01.vault.azure.net", "password")
                .GetAwaiter().GetResult();

            Console.WriteLine("The secret value is : " + secret.Value);

            Console.ReadLine();
        }

        public static async Task<string> GetAccessToken(string azureTenantId, string clientId, string redirectUri)
        {
            var context = new AuthenticationContext("https://login.windows.net/" + tenantId);
            var credential = new ClientCredential(appId, appSecret);
            var tokenResult = await context.AcquireTokenAsync("https://vault.azure.net", credential);
            return tokenResult.AccessToken;
        }
    }
}
