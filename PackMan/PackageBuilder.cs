using System.Collections.Generic;
using System.IO;
using Ionic.Zip;

namespace PackMan
{
    public class PackageBuilder
    {
        public static void BuildPackage(IEnumerable<string> includes, string outputFileName)
        {
            File.Delete(outputFileName);
            using (var zipFile = new ZipFile(outputFileName))
            {
                foreach (var include in includes)
                {
                    zipFile.AddSelectedFiles(include, true);
                }
                zipFile.Save();
            }
        }

        public static void Unpack(string packageFileName)
        {
            using (var zipFile = ZipFile.Read(packageFileName))
            {
                var packageDir = Path.GetFileNameWithoutExtension(packageFileName);
                File.Delete(packageDir);
                zipFile.ExtractAll(packageDir);
            }
        }
    }
}