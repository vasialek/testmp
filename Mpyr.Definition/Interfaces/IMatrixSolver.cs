using Mpyr.Definition.Models;

namespace Mpyr.Definition.Interfaces
{
	public interface IMatrixSolver
	{
		Route FindMax(BinaryTreeNode tree);

		Route[] FindAllRoutes(BinaryTreeNode tree);
	}
}
