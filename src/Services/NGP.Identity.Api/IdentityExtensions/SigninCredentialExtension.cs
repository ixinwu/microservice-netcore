using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System.IO;
using System.Security.Cryptography.X509Certificates;

namespace NGP.Identity.Api.IdentityExtensions
{
    /// <summary>
    /// Impl of adding a signin key for identity server 4,
    /// with an appsetting.json configuration look similar to:
    /// "SigninKeyCredentials": {
    ///     "KeyType": "KeyFile",
    ///     "KeyFilePath": "C:\\certificates\\idsv4.pfx",
    ///     "KeyStorePath": ""
    /// }
    /// </summary>
    public static class SigninCredentialExtension
    {
        public static IIdentityServerBuilder AddSigninCredentialFromConfig(
            this IIdentityServerBuilder builder, IConfigurationSection options, 
            IHostingEnvironment hostingEnvironment)
        {
            string keyType = options.GetValue<string>("KeyType");

            switch (keyType)
            {
                case "KeyFile":
                    AddCertificateFromFile(builder, options, hostingEnvironment);
                    break;

                case "KeyStore":
                    AddCertificateFromStore(builder, options);
                    break;
            }

            return builder;
        }

        private static void AddCertificateFromStore(IIdentityServerBuilder builder,
            IConfigurationSection options)
        {
            var keyIssuer = options.GetValue<string>("KeyStoreIssuer");

            X509Store store = new X509Store(StoreName.My, StoreLocation.LocalMachine);
            store.Open(OpenFlags.ReadOnly);

            var certificates = store.Certificates.Find(X509FindType.FindByIssuerName, keyIssuer, true);

            if (certificates.Count > 0)
                builder.AddSigningCredential(certificates[0]);
        }

        private static void AddCertificateFromFile(IIdentityServerBuilder builder,
            IConfigurationSection options,
            IHostingEnvironment hostingEnvironment)
        {
            var basePath = File.Exists(hostingEnvironment.WebRootPath)
                ? Path.GetDirectoryName(hostingEnvironment.WebRootPath)
                : hostingEnvironment.ContentRootPath;

            var keyFilePath = Path.Combine(basePath, "NGPAuth.pfx");
            var keyFilePassword = options.GetValue<string>("KeyFilePassword");

            if (File.Exists(keyFilePath))
            {
                var cer = new X509Certificate2(keyFilePath, keyFilePassword);
                builder.AddSigningCredential(cer);
            }
        }
    }
}
