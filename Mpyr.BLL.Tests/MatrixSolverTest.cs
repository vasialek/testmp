using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mpyr.Definition.Interfaces;

namespace Mpyr.BLL.Tests
{
	[TestClass]
	public class MatrixSolverTest
	{
		private readonly IMatrixSolver _solver = new MatrixSolver();
		private readonly IFileParser _parser = new FileParser();

		#region Find max route

		[TestMethod]
		public void FindMax_CheckSum()
		{
			var tree = _parser.Build(new string[] 
			{
				"1",
				"8 9",
				"1 5 9",
				"4 5 2 3",
			});

			var r = _solver.FindMax(tree);

			// Expecting: 1 -> 8 -> 5 -> 2 = 16
			r.Sum.Should().Be(16);
		}

		[TestMethod]
		public void FindMax_NoException_WhenNoRoute()
		{
			var tree = _parser.Build(new string[]
			{
				"1",
				"3 5",
			});

			var r = _solver.FindMax(tree);

			// No exception
			r.Should().BeNull();
		}

		#endregion

		#region Pathfinder

		[TestMethod]
		public void FindAllRoutes_GetTwoPaths()
		{
			var tree = _parser.Build(new string[] { "1", "2 4" });

			var routes = _solver.FindAllRoutes(tree);

			// Expecting 2 valid routes: 1->2 and 1->4
			routes.Should().HaveCount(2);
		}

		[TestMethod]
		public void FindAllRoutes_GetTwoPaths_WhenDoubleRun()
		{
			var tree = _parser.Build(new string[] { "1", "2 4" });

			_solver.FindAllRoutes(tree);
			var routes = _solver.FindAllRoutes(tree);

			// Expecting 2 valid routes, previous are cleared
			routes.Should().HaveCount(2);
		}

		[TestMethod]
		public void FindAllRoutes_CheckRouteLength()
		{
			var tree = _parser.Build(new string[] { "1", "2 4" });

			var routes = _solver.FindAllRoutes(tree);

			// Expecting 1 2
			routes[0].Path.Should().HaveCount(2);
		}

		[TestMethod]
		public void FindAllRoutes_GetOne_WhenOneIsInvalid()
		{
			var tree = _parser.Build(new string[] { "1", "2 3" });

			var routes = _solver.FindAllRoutes(tree);

			// Expecting 1 valid: 1->2. 1->3 is invalid
			routes.Should().HaveCount(1);
		}

		#endregion
	}
}
