using System.Linq;

namespace Mpyr.Definition.Models
{
	public class Route
	{
		public int[] Path { get; set; }

		public int Sum => Path?.Sum() ?? -1;

		public Route(int[] routes = null)
		{
			if (routes?.Length > 0)
			{
				Path = new int[routes.Length];
				for (int i = 0; i < routes.Length; i++)
				{
					Path[i] = routes[i];
				}
			}
		}
	}
}
