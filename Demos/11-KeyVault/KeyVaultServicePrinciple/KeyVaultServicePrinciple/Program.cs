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
        static string appId = "59f91d97-2f83-4322-b07c-922409370dd5";
        static string appSecret = "WhF59Pgv31_q~q8s-Iif8TnWzF5-NlN58O";
        static string tenantId = "0ec02b79-d89f-48c4-9870-da4a7498d887";

        static void Main(string[] args)
        {
            var kv = new KeyVaultClient(GetAccessToken);
            var secret = kv.GetSecretAsync("https://vault-demo01.vault.azure.net", "password")
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
