using Mpyr.Definition.Models;

namespace Mpyr.Definition.Interfaces
{
	public interface IFileParser
    {
		BinaryTreeNode Build(string[] lines);
    }
}
