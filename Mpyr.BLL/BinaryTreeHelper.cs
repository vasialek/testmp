using Mpyr.Definition.Models;

namespace Mpyr.BLL
{
	public static class BinaryTreeHelper
	{
		public static int GetTreeHeight(BinaryTreeNode root)
		{
			int leftHeight = 0;
			int rightHeight = 0;

			if (root == null)
			{
				return 0;
			}

			leftHeight = GetTreeHeight(root.Left);
			rightHeight = GetTreeHeight(root.Right);

			// Return bigger height value + current
			return leftHeight > rightHeight ? leftHeight + 1 : rightHeight + 1;
		}
	}
}
