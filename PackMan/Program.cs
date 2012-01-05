using System;
using System.Collections.Generic;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using Ionic.Zip;
using NDesk.Options;

namespace PackMan
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var createPackage = new CreatePackageAction();
                var unpackPackage = new UnpackPackageAction();
                var printHelp = new PrintHelpAction();

                IAction selectedAction = printHelp;

                var optionSet = new OptionSet()
                    .Add("h|?|help", "Print this help information", x => selectedAction = printHelp)
                    .Add("a|action=", "Action to be performed (pack/unpack)",
                         delegate(string x)
                             {
                                 switch (x.ToLowerInvariant())
                                 {
                                     case "pack":
                                         selectedAction = createPackage;
                                         break;
                                     case "unpack":
                                         selectedAction = unpackPackage;
                                         break;
                                     default:
                                         selectedAction = printHelp;
                                         break;
                                 }
                             })
                    .AddFromAction(createPackage)
                    .AddFromAction(unpackPackage);

                optionSet.Parse(args);
                var result = selectedAction.Perform(optionSet);
                Environment.Exit(result);
            }
            catch (PackManException ex)
            {
                Console.WriteLine(ex.Message);
                Environment.Exit(1);
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Environment.Exit(1);
            }
            
        }

        private static bool NotEnoughData(IEnumerable<string> includes, string outputFileName)
        {
            return !includes.Any() || string.IsNullOrEmpty(outputFileName);
        }
    }
}
