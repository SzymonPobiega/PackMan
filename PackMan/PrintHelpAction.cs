using System;
using NDesk.Options;

namespace PackMan
{
    public class PrintHelpAction : IAction
    {
        public void Perform(OptionSet optionSet)
        {
            optionSet.WriteOptionDescriptions(Console.Out);
        }

        public bool VerifyParamaters()
        {
            return true;
        }

        public void BindCommadLineParameters(OptionSet optionSet)
        {
        }
    }
}