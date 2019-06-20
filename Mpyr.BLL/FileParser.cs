using Mpyr.Definition.Interfaces;
using Mpyr.Definition.Models;

namespace Mpyr.BLL
{
	public class FileParser : IFileParser
	{
		private readonly static char[] _separators = new char[] { ' ' };

		public BinaryTreeNode Build(string[] lines)
		{
			lines = Helper.CleanupLines(lines);
			var matrix = Helper.BuildMatrix(lines);

			var root = new BinaryTreeNode();

			return Parse(root, matrix, 0, 0);
		}

		private BinaryTreeNode Parse(BinaryTreeNode root, int?[,] matrix, int x, int y)
		{
			if (x < matrix.GetLength(1) && y < matrix.GetLength(1))
			{
				root = new BinaryTreeNode { Data = matrix[y, x].Value };
				root.Left = Parse(root.Left, matrix, x, y + 1);
				root.Right = Parse(root.Right, matrix, x + 1, y + 1);
				return root;
			}
			return null;
		}
	}
}
