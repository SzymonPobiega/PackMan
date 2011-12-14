using System.Collections.Generic;
using System.IO;
using System.Linq;
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
                    if (include.Length < 3)
                    {
                        throw new PackManException("Include option must have following format: f|d|p:<value>.");
                    }
                    char type = char.ToLower(include[0]);
                    string value = include.Substring(2);
                    switch (type)
                    {
                        case 'f':
                            zipFile.AddFile(value);
                            break;
                        case 'd':
                            zipFile.AddDirectory(value);
                            break;
                        case 'p':
                            zipFile.AddSelectedFiles(value,true);
                            break;
                    }
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