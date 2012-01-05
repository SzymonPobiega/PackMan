using System;
using NDesk.Options;

namespace PackMan
{
    public static class OptionSetExtensions
    {
        public static OptionSet AddFromAction(this OptionSet optionSet, IAction action)
        {
            action.BindCommadLineParameters(optionSet);
            return optionSet;
        }
    }
}