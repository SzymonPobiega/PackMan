using System;
using NDesk.Options;

namespace PackMan
{
    public class PrintHelpAction : IAction
    {
        public int Perform(OptionSet optionSet)
        {
            optionSet.WriteOptionDescriptions(Console.Out);
            return 0;
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