using Mpyr.Definition.Interfaces;
using Mpyr.Definition.Models;
using System.Collections.Generic;
using System.Linq;

namespace Mpyr.BLL
{
	public class MatrixSolver : IMatrixSolver
	{
		public Route FindMax(BinaryTreeNode tree)
		{
			var routes = FindAllRoutes(tree);

			return routes?.OrderByDescending(x => x.Sum)?.FirstOrDefault();
		}

		public Route[] FindAllRoutes(BinaryTreeNode tree)
		{
			var route = new List<int>();

			// Max route will be equal to height of tree
			int[] routeData = new int[BinaryTreeHelper.GetTreeHeight(tree)];
			var routes = new List<Route>();
			FindAllRoutes(tree, routeData, 0, routes);

			return routes.ToArray();
		}

		private void FindAllRoutes(BinaryTreeNode node, int[] routeData, int pos, IList<Route> routes)
		{
			if (node != null)
			{
				// Put current data in route on its position
				routeData[pos] = node.Data;
				pos++;
				if (IsLeaf(node))
				{
					// Add found route
					routes.Add(new Route(routeData));
				}
				else
				{
					if (IsNextOk(node, node.Left))
					{
						FindAllRoutes(node.Left, routeData, pos, routes);
					}
					if (IsNextOk(node, node.Right))
					{
						FindAllRoutes(node.Right, routeData, pos, routes);
					}
				}
			}
		}

		/// <summary>
		/// We could go to next - if it is leaf (last) or even/odd
		/// </summary>
		private bool IsNextOk(BinaryTreeNode node, BinaryTreeNode next)
		{
			return next == null || (node.Data & 0x01) != (next.Data & 0x01);
		}

		/// <summary>
		/// Returns true if leaf (last element)
		/// </summary>
		private bool IsLeaf(BinaryTreeNode node)
		{
			return node.Left == null && node.Right == null;
		}
	}
}
