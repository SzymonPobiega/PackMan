using NDesk.Options;

namespace PackMan
{
    public interface IAction
    {
        void Perform(OptionSet optionSet);
        bool VerifyParamaters();
        void BindCommadLineParameters(OptionSet optionSet);
    }
}