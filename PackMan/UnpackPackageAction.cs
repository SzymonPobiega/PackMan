using System;
using System.Security.Cryptography.X509Certificates;
using NDesk.Options;

namespace PackMan
{
    public class UnpackPackageAction : IAction
    {
        private string _packageFileName;
        private string _cn;
        private string _destinationDir;
        private StoreLocation _storeLocation = StoreLocation.LocalMachine;

        public int Perform(OptionSet optionSet)
        {
            if (!PackageSigner.VerifySignature(_cn, _packageFileName, _storeLocation))
            {
                Console.WriteLine("Failed to verify authenticity of package {0}.", _packageFileName);
                return 1;
            }
            PackageBuilder.Unpack(_packageFileName, _destinationDir);
            Console.WriteLine("Unpacked package {0} to {1}", _packageFileName, _destinationDir);
            return 0;
        }

        public bool VerifyParamaters()
        {
            return !string.IsNullOrEmpty(_packageFileName)
                   && !string.IsNullOrEmpty(_cn)
                   && !string.IsNullOrEmpty(_destinationDir);
        }

        public void BindCommadLineParameters(OptionSet optionSet)
        {
            optionSet
                .Add("p|package=", "Name of the package file", x => _packageFileName = x)
                .Add("vcn=", "CN of the certificate to verify", x => _cn = x)
                .Add("d|destination=","Name of destination directory", x => _destinationDir = x)
                .Add("s|store=","Store location", x=> _storeLocation = x == "user" ? StoreLocation.CurrentUser : StoreLocation.LocalMachine);
        }
    }
}