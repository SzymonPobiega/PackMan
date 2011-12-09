using System.IO;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace PackMan.Tests
{
    [TestFixture]
    public class PackageBuilderTests
    {
        [Test]
        public void It_can_pack_and_unpack_a_package_with_directories_and_files()
        {
            Directory.CreateDirectory("Dir");

            File.Create(@"Dir\a.txt").Close();
            File.Create(@"Dir\b.txt").Close();
            PackageBuilder.BuildPackage(new[] { "*.txt" }, "package.zip");
            PackageBuilder.Unpack("package.zip");

            Assert.IsTrue(Directory.Exists("package"));
            Assert.IsTrue(File.Exists(@"package\Dir\a.txt"));
            Assert.IsTrue(File.Exists(@"package\Dir\b.txt"));
        }

        [SetUp]
        public void SetUp()
        {
            if (Directory.Exists("package"))
            {
                Directory.Delete("package", true);
            }
            if (Directory.Exists("Dir"))
            {
                Directory.Delete("Dir", true);
            }
            File.Delete("a.txt");
            File.Delete("b.txt");
        }
    }
}
// ReSharper restore InconsistentNaming
