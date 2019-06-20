using FluentAssertions;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Mpyr.Definition.Models;

namespace Mpyr.BLL.Tests
{
	[TestClass]
	public class BinaryTreeHelperTest
	{
		private readonly BinaryTreeNode _root = null;

		public BinaryTreeHelperTest()
		{
			_root = new BinaryTreeNode(1);
			_root.Left = new BinaryTreeNode(2);
			_root.Right = new BinaryTreeNode(3);
			_root.Left.Left = new BinaryTreeNode(4);
			_root.Left.Right = new BinaryTreeNode(5);
		}

		#region Tree height

		[TestMethod]
		public void GetTreeHeight_Zero_WhenNull()
		{
			int h = BinaryTreeHelper.GetTreeHeight(null);

			// Expecting no exception
			h.Should().Be(0);
		}

		[TestMethod]
		public void GetTreeHeight_CheckHeight()
		{
			int height = BinaryTreeHelper.GetTreeHeight(_root);

			height.Should().Be(3);
		}

		#endregion
	}
}
