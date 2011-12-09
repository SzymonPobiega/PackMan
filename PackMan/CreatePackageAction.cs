using System;
using System.Collections.Generic;
using NDesk.Options;

namespace PackMan
{
    public class CreatePackageAction : IAction
    {
        private readonly List<string> _includes = new List<string>();
        private string _outputFileName;
        private string _cn;

        public void Perform(OptionSet optionSet)
        {
            PackageBuilder.BuildPackage(_includes, _outputFileName);
            PackageSigner.Sign(_cn, _outputFileName);

            Console.WriteLine("Created package {0}", _outputFileName);
        }

        public bool VerifyParamaters()
        {
            return _includes.Count > 0
                   && !string.IsNullOrEmpty(_outputFileName)
                   && !string.IsNullOrEmpty(_cn);
        }

        public void BindCommadLineParameters(OptionSet optionSet)
        {
            optionSet
                .Add("i|include=", "Include files to the package", _includes.Add)
                .Add("o|output=", "Name of the output file", x => _outputFileName = x)
                .Add("cn=", "CN of the certificate", x => _cn = x);
        }
    }
}