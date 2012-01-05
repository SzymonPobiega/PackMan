using NDesk.Options;

namespace PackMan
{
    public interface IAction
    {
        int Perform(OptionSet optionSet);
        bool VerifyParamaters();
        void BindCommadLineParameters(OptionSet optionSet);
    }
}