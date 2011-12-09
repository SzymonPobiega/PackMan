using System;
using System.Collections.Generic;
using System.IO.Compression;
using System.Linq;
using System.Text;
using NDesk.Options;

namespace PackMan
{
    
    class Program
    {
        static void Main(string[] args)
        {
            var printHelp = false;
            var optionSet = new OptionSet()
                .Add("h|?|help", "Print this help information", x =>
                                                               {
                                                                   printHelp = x != null;
                                                               });

            var result = optionSet.Parse(args);

            if (printHelp)
            {
                Console.WriteLine("Usage: packman.exe [-h]");
                Console.WriteLine("Available options:");
                optionSet.WriteOptionDescriptions(Console.Out);
            }

            Console.ReadLine();

            //var packageBytes = BuildPackage(args);

            //var hash = ComputeHash(packageBytes);

            //var signature = Sign(hash, args);

            //WritePackage(args, packageBytes);
            //WriteSignature(args, signature);
        }
    }
}
