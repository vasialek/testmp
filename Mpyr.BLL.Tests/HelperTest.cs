using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Linq;

namespace Mpyr.BLL.Tests
{
	[TestClass]
	public class HelperTest
	{
		private readonly string[] _lines = null;

		public HelperTest()
		{
			/*
			 *    0 1 2
			 *    -----> X
			 *  0|1 X X
			 *  1|2 3 X
			 *  2|4 5 6
			 *   v
			 */
			_lines = new string[]
			{
				"1",
				"2 3",
				"4 5 6",
			};
		}

		#region Build matrix

		[TestMethod]
		public void BuildMatrix_Check0_0()
		{
			var m = Helper.BuildMatrix(_lines);

			m[0, 0].Should().Be(1);
		}

		[TestMethod]
		public void BuildMatrix_Check0_1()
		{
			var m = Helper.BuildMatrix(_lines);

			m[0, 1].Should().BeNull();
		}

		[TestMethod]
		public void BuildMatrix_Check0_2()
		{
			var m = Helper.BuildMatrix(_lines);

			m[0, 2].Should().BeNull();
		}

		[TestMethod]
		public void BuildMatrix_Check1_0()
		{
			var m = Helper.BuildMatrix(_lines);

			m[1, 0].Should().Be(2);
		}

		[TestMethod]
		public void BuildMatrix_Check1_2()
		{
			var m = Helper.BuildMatrix(_lines);

			m[1, 2].Should().Be(null);
		}

		[TestMethod]
		public void BuildMatrix_Check2_0()
		{
			var m = Helper.BuildMatrix(_lines);

			m[2, 0].Should().Be(4);
		}

		[TestMethod]
		public void BuildMatrix_Check2_1()
		{
			var m = Helper.BuildMatrix(_lines);

			m[2, 1].Should().Be(5);
		}

		[TestMethod]
		public void BuildMatrix_Check2_2()
		{
			var m = Helper.BuildMatrix(_lines);

			m[2, 2].Should().Be(6);
		}

		#endregion

		#region Cleanup lines

		[TestMethod]
		public void CleanupLines_CheckLength()
		{
			string[] lines = Helper.CleanupLines(_lines);

			lines.Should().BeEquivalentTo(_lines);
		}

		[TestMethod]
		public void CleanupLines_RemoveEmpty()
		{
			string[] lines = Helper.CleanupLines(new string[] { "1", "", "2" });

			lines.Should().BeEquivalentTo(new string[] { "1", "2" });
		}

		[TestMethod]
		public void CleanupLines_RemoveNonDigits()
		{
			string[] lines = Helper.CleanupLines(new string[] { "1", "---" });

			// Expecting line "---" is skipped
			lines.Single().Should().Be("1");
		}

		#endregion
	}
}
