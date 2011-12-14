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
            PackageBuilder.BuildPackage(new[] { "p:*.txt" }, "package.zip");
            PackageBuilder.Unpack("package.zip");

            Assert.IsTrue(Directory.Exists("package"));
            Assert.IsTrue(File.Exists(@"package\Dir\a.txt"));
            Assert.IsTrue(File.Exists(@"package\Dir\b.txt"));
        }

        [Test]
        public void It_can_pack_and_unpack_a_package_with_nested_directories()
        {
            Directory.CreateDirectory(@"Dir");
            Directory.CreateDirectory(@"Dir\\Subdir");

            File.Create(@"Dir\a.txt").Close();
            File.Create(@"Dir\Subdir\b.txt").Close();

            PackageBuilder.BuildPackage(new[] { "d:Dir" }, "package.zip");
            PackageBuilder.Unpack("package.zip");

            Assert.IsTrue(Directory.Exists("package"));
            Assert.IsTrue(File.Exists(@"package\a.txt"));
            Assert.IsTrue(File.Exists(@"package\Subdir\b.txt"));
        }

        [Test]
        public void It_can_pack_and_unpack_a_package_with_directories_with_spaces_in_name()
        {
            Directory.CreateDirectory(@"Dir ectory");

            File.Create(@"Dir ectory\a.txt").Close();

            PackageBuilder.BuildPackage(new[] { @"d:Dir ectory" }, "package.zip");
            PackageBuilder.Unpack("package.zip");

            Assert.IsTrue(Directory.Exists("package"));
            Assert.IsTrue(File.Exists(@"package\a.txt"));
        }

        [Test]
        public void It_can_pack_and_unpack_a_package_with_directories()
        {
            Directory.CreateDirectory("Dir");

            File.Create(@"Dir\a.txt").Close();
            File.Create(@"Dir\b.txt").Close();
            PackageBuilder.BuildPackage(new[] { "p:Dir"}, "package.zip");
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
