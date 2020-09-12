using CwoPqsCrosswalk.Models.Services;
using NUnit.Framework;
using System.IO;
using System.Text;

namespace CwoPqsCrosswalkTestFixture.Models.Services.Tests
{
    class FileHelperTests
	{
		public const string TestDirectory = @"C:\TempTests";
		public const string TestFileName = "MyTest";
		public static string TestFileFullName;
		private static bool deleteDirectory = false;

		private static void CreateTestDirectory()
        {
			Directory.CreateDirectory(TestDirectory);
			deleteDirectory = true;
		}

		private static void CreateTestFile(string extension)
		{
			if (!Directory.Exists(TestDirectory))
			{
				CreateTestDirectory();
			}

			TestFileFullName = Path.Combine(TestDirectory, TestFileName + extension);

			if (File.Exists(TestFileFullName))
			{
				File.Delete(TestFileFullName);
			}

			using FileStream fs = File.Create(TestFileFullName);
			byte[] info = new UTF8Encoding(true).GetBytes("");
			fs.Write(info, 0, info.Length);
		}

		private static void CleanUpTestFile()
		{
			if (File.Exists(TestFileFullName))
			{
				File.Delete(TestFileFullName);
			}
		}

		private static void CleanUpTestDirectory()
        {
			if (!Directory.Exists(TestDirectory))
			{
				return;
			}

			// https://www.csharp-examples.net/delete-all-files/
			string[] filePaths = Directory.GetFiles(TestDirectory);
			foreach (string filePath in filePaths)
            {
				File.Delete(filePath);
            }

			Directory.Delete(TestDirectory);
		}

		private static string ReadTestFileContents()
        {
			string contents = "";
			using (StreamReader sr = File.OpenText(TestFileFullName))
			{
				string s;
				while ((s = sr.ReadLine()) != null)
				{
					contents += s;
				}
			}
			return contents;
		}

		[SetUp]
		public void Setup()
		{
		}

		[TearDown]
		public void TearDown()
        {
			CleanUpTestFile();
            if (deleteDirectory)
            {
				CleanUpTestDirectory();
            }
        }

		[Test]
		public void CreatesFileInExistingDirectory()
		{
			CreateTestDirectory();
			TestFileFullName = Path.Combine(TestDirectory, TestFileName + ".txt");

			FileHelper.WriteTo(TestFileFullName, "Hello World");
			bool actual = File.Exists(TestFileFullName);

			Assert.IsTrue(actual);
		}

		[Test]
		public void CreatesFileInNonExistingDirectory()
        {
			// Setup
			CleanUpTestDirectory();
			TestFileFullName = Path.Combine(TestDirectory, TestFileName + ".txt");
			deleteDirectory = true;

			FileHelper.WriteTo(TestFileFullName, "Hello World");
			bool actual = File.Exists(TestFileFullName);

			Assert.IsTrue(actual);
		}

		[Test]
		public void WritesOverFile()
        {
			// Setup
			CreateTestFile(".txt");
			string contents = "Hello World.";
			string newContents = "And Universe.";
			FileHelper.WriteTo(TestFileFullName, contents);
			var actual = ReadTestFileContents();

			Assert.AreEqual(contents, actual);

			FileHelper.WriteOver(TestFileFullName, newContents);

			actual = ReadTestFileContents();
			Assert.AreEqual(newContents, actual);
		}

		[Test]
		public void AppendsToFile()
		{
			// Setup
			CreateTestFile(".txt");
			string contents = "Hello World.";
			string newContents = "And Universe.";
			FileHelper.WriteTo(TestFileFullName, contents);
			var actual = ReadTestFileContents();

			Assert.AreEqual(contents, actual);

			FileHelper.WriteTo(TestFileFullName, newContents);

			actual = ReadTestFileContents();
			Assert.AreEqual(contents + newContents, actual);
		}
	}
}
