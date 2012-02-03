using System;
using System.IO;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;

namespace PackMan
{
    public class PackageSigner
    {
        public static void Sign(string cn, string outputFileName)
        {
            var certificate = GetAlgorithmForCertificateByCN(cn, StoreLocation.CurrentUser);
            var cryptoProvider = (RSACryptoServiceProvider) certificate.PrivateKey;
            var signature = ComputeSignature(outputFileName, cryptoProvider);
            WriteSignature(outputFileName, signature);
        }

        public static bool VerifySignature(string cn, string packageFileName, StoreLocation storeLocation)
        {
            var certificate = GetAlgorithmForCertificateByCN(cn, storeLocation);
            var cryptoProvider = (RSACryptoServiceProvider) certificate.PublicKey.Key;
            var hashAlgotithm = CreateHashAlgorithm();
            byte[] hash;
            using( var packageFileStream = new FileStream(packageFileName, FileMode.Open))
            {
                hash = hashAlgotithm.ComputeHash(packageFileStream);
            }
            var signature = ReadSignature(packageFileName);
            return cryptoProvider.VerifyHash(hash, CryptoConfig.MapNameToOID("SHA1"), signature);
        }

        private static X509Certificate2 GetAlgorithmForCertificateByCN(string cn, StoreLocation storeLocation)
        {
            var myStore = OpenCertificateStore(storeLocation);
            var certificate = FindSingleMatchingCertificate(myStore, cn);
            return certificate;
        }

        private static byte[] ComputeSignature(string outputFileName, RSACryptoServiceProvider cryptoServiceProvider)
        {
            byte[] signature;
            using( var packageFileStream = new FileStream(outputFileName, FileMode.Open))
            {
                signature = cryptoServiceProvider.SignData(packageFileStream, CreateHashAlgorithm());
            }
            return signature;
        }

        private static SHA1CryptoServiceProvider CreateHashAlgorithm()
        {
            return new SHA1CryptoServiceProvider();
        }

        private static X509Store OpenCertificateStore(StoreLocation storeLocation)
        {
            var myStore = new X509Store(StoreName.My, storeLocation);
            myStore.Open(OpenFlags.ReadOnly);
            return myStore;
        }

        private static X509Certificate2 FindSingleMatchingCertificate(X509Store myStore, string cn)
        {
            var matching = myStore.Certificates.Find(X509FindType.FindBySubjectName, cn, false);
            if (matching.Count == 0)
            {
                throw new PackManException(String.Format("Certificate for CN={0} not found in current user's store",cn));
            }
            if (matching.Count > 1)
            {
                throw new PackManException(String.Format("More than one match found for certificate with CN={0} in current user's store.",cn));
            }
            return matching[0];
        }

        private static void WriteSignature(string outputFileName, byte[] signature)
        {
            var signatureFileName = Path.ChangeExtension(outputFileName, ".signature");
            using (var signatureFileStream = new FileStream(signatureFileName,FileMode.Create))
            {
                signatureFileStream.Write(signature, 0, signature.Length);
                signatureFileStream.Flush();
            }
        }

        private static byte[] ReadSignature(string packageFileName)
        {
            var signatureFileName = Path.ChangeExtension(packageFileName, ".signature");
            return File.ReadAllBytes(signatureFileName);
        }
    }
}