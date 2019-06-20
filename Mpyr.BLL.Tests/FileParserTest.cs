using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mpyr.Definition.Interfaces;

namespace Mpyr.BLL.Tests
{
	[TestClass]
	public class FileParserTest
	{
		private readonly string[] _lines = null;

		private readonly IFileParser _parser = new FileParser();

		public FileParserTest()
		{
			_lines = new string[]
			{
				"1",
				"8 9",
				"1 5 9",
				"4 5 2 3",
			};
		}

		#region Build tree

		[TestMethod]
		public void Build_Root()
		{
			var r = _parser.Build(new string[] { "1" });

			r.Data.Should().Be(1);
		}

		[TestMethod]
		public void Build_CheckRootData()
		{
			var r = _parser.Build(_lines);

			r.Data.Should().Be(1);
		}

		[TestMethod]
		public void Build_CheckLeftData()
		{
			var r = _parser.Build(_lines);

			r.Left.Data.Should().Be(8);
		}

		[TestMethod]
		public void Build_CheckRightData()
		{
			var r = _parser.Build(_lines);

			r.Right.Data.Should().Be(9);
		}

		[TestMethod]
		public void Build_SkipEmptyLines()
		{
			var r = _parser.Build(new string[]
			{
				"1",
				"",
				"2 3",
			});

			r.Left.Data.Should().Be(2);
		}

		#endregion

	}
}
