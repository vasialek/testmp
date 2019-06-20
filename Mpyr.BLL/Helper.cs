using System;
using System.Linq;

namespace Mpyr.BLL
{
	public static class Helper
	{
		private readonly static char[] _separators = new char[] { ' ' };

		public static int?[,] BuildMatrix(string[] lines)
		{
			int height = lines.Length;
			int width = height;
			int?[,] matrix = new int?[width, height];

			for (int y = 0; y < height; y++)
			{
				string[] ar = lines[y].Split(_separators);
				for (int x = 0; x < ar.Length; x++)
				{
					matrix[y, x] = int.Parse(ar[x]);
				}
			}

			return matrix;
		}

		public static string[] CleanupLines(string[] lines)
		{
			// Leave only non-empty lines which starts with digit
			return lines
				.Where(x => x.Length > 0 && Char.IsDigit(x[0]))
				.ToArray();
		}
	}
}
