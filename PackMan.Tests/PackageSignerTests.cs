using System.IO;
using NUnit.Framework;

// ReSharper disable InconsistentNaming
namespace PackMan.Tests
{
    [TestFixture]
    public class PackageSignerTests
    {
        [Test]
        public void Digital_signature_can_be_verified()
        {
            File.Delete("a.txt");
            using (var file = File.CreateText("a.txt"))
            {
                file.WriteLine("Test");
                file.Flush();
            }
            PackageSigner.Sign("PackManTest", "a.txt");
            var result = PackageSigner.VerifySignature("PackManTest", "a.txt");
            Assert.IsTrue(result);
        }

        [Test]
        public void Digital_signature_is_created_in_same_forlder_as_package()
        {
            if (!Directory.Exists("Subfolder"))
            {
                Directory.CreateDirectory("Subfolder");
            }
            File.Delete(@"Subfolder\a.txt");
            File.Delete(@"Subfolder\a.signature");
            using (var file = File.CreateText(@"Subfolder\a.txt"))
            {
                file.WriteLine("Test");
                file.Flush();
            }
            PackageSigner.Sign("PackManTest", @"Subfolder\a.txt");
            Assert.IsTrue(File.Exists(@"Subfolder\a.signature"));
        }
    }
}
// ReSharper restore InconsistentNaming
