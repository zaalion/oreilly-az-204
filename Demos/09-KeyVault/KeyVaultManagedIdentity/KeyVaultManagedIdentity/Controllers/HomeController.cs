using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Azure.Identity;
using Azure.Security.KeyVault.Secrets;
using Azure.Core;
using System.Configuration;

namespace KeyVaultManagedIdentity.Controllers
{
    /// <summary>
    /// https://docs.microsoft.com/en-us/azure/key-vault/general/tutorial-net-create-vault-azure-web-app
    /// </summary>
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            SecretClientOptions options = new SecretClientOptions()
            {
                Retry =
                {
                    Delay= TimeSpan.FromSeconds(2),
                    MaxDelay = TimeSpan.FromSeconds(16),
                    MaxRetries = 5,
                    Mode = RetryMode.Exponential
                 }
            };
            var client = new SecretClient
                (new Uri("https://az204ordemokv.vault.azure.net/"),
                new DefaultAzureCredential(), options);

            KeyVaultSecret secret = client.GetSecret("password");

            string secretValue = secret.Value;

            ViewData["secretValue"] = secretValue;

            return View();
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}