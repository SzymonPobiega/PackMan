using System;
using NDesk.Options;

namespace PackMan
{
    public class UnpackPackageAction : IAction
    {
        private string _packageFileName;
        private string _cn;

        public void Perform(OptionSet optionSet)
        {
            


        }

        public bool VerifyParamaters()
        {
            return !string.IsNullOrEmpty(_packageFileName)
                   && !string.IsNullOrEmpty(_cn);
        }

        public void BindCommadLineParameters(OptionSet optionSet)
        {
            optionSet
                .Add("i|input=", "Name of the package file", x => _packageFileName = x)
                .Add("cn=", "CN of the certificate", x => _cn = x);
        }
    }
}