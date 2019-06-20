namespace Mpyr.Definition.Models
{
	public class BinaryTreeNode
	{
		public int Data { get; set; }

		public BinaryTreeNode Left { get; set; }

		public BinaryTreeNode Right { get; set; }

		public BinaryTreeNode(int? data = null)
		{
			if (data.HasValue)
			{
				Data = data.Value;
			}
		}
	}
}
