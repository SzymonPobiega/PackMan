using System.IO;
using NUnit.Framework;

namespace PackMan.Tests
{
    [TestFixture]
    public class PackageSignerTests
    {
        [Test]
// ReSharper disable InconsistentNaming
        public void Digital_signature_can_be_verified()
// ReSharper restore InconsistentNaming
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
    }
}