using AvUtils;
using Mpyr.BLL;
using Mpyr.Definition.Interfaces;
using System;
using System.IO;

namespace MpyrConsole
{
	class Program
	{
		private static string _examplesDirectory = null;
		private static readonly IFileParser _parser = new FileParser();
		private static readonly IMatrixSolver _solver = new MatrixSolver();

		static void Main(string[] args)
		{
			bool isRunning = true;
			var menu = new Menu();
			_examplesDirectory = GetExamplesDirectory();

			menu.Add("Exit", () => { isRunning = false; }, ConsoleColor.DarkYellow);
			menu.Add("Find MAX path", () => { FindMax(); });
			menu.Add("Find all routes", () => { FindAllRoutes(); });

			do
			{
				try
				{
					menu.Display();
				}
				catch (Exception ex)
				{
					Output.WriteLine(ConsoleColor.Red, ex.Message);
					Console.WriteLine(ex.ToString());
					Console.WriteLine("Press ENTER to continue...");
					Console.ReadKey(true);
					Console.Clear();
				}
			} while (isRunning);
		}

		private static void FindMax()
		{
			string[] lines = SelectFileAndReadLines();
			if (lines?.Length > 0)
			{
				var matrix = _parser.Build(lines);
				var route = _solver.FindMax(matrix);
				if (route != null)
				{
					Output.WriteLine(ConsoleColor.Green, "Got MAX route");
					Output.WriteLine("{0} ({1})", String.Join(" -> ", route.Path), route.Sum);
				}
				else
				{
					Output.WriteLine(ConsoleColor.Red, "No route found.");
				}
			}
		}

		private static void FindAllRoutes()
		{
			string[] lines = SelectFileAndReadLines();
			if (lines?.Length > 0)
			{
				var matrix = _parser.Build(lines);
				var routes = _solver.FindAllRoutes(matrix);
				if (routes?.Length > 0)
				{
					Output.WriteLine(ConsoleColor.Green, "Got {0} available routes", routes.Length);
					foreach (var r in routes)
					{
						Console.WriteLine("{0} ({1})", String.Join(" -> ", r.Path), r.Sum);
					}
				}
				else
				{
					Output.WriteLine(ConsoleColor.Red, "No routes found.");
				}
			}
		}

		private static string[] SelectFileAndReadLines()
		{
			string[] lines = null;
			// Select file from directory or NULL if not selected
			string filename = new FileMenu(_examplesDirectory)
				.GetFullFilename("*.txt");

			if (String.IsNullOrEmpty(filename) == false)
			{
				Console.WriteLine("Going to read all lines from file `{0}`", filename);

				// Read from file...
				lines = File.ReadAllLines(filename);
				// remove empty...
				lines = Helper.CleanupLines(lines);
				// display
				DumpMatix(Helper.BuildMatrix(lines));
			}

			return lines;
		}

		private static void DumpMatix(int?[,] matrix)
		{
			for (int y = 0; y < matrix.GetLength(0); y++)
			{
				for (int x = 0; x < matrix.GetLength(1); x++)
				{
					string s = (matrix[y, x]?.ToString() ?? "").PadRight(5, ' ');
					Console.Write(s);
				}
				Console.WriteLine();
			}
		}

		private static string GetExamplesDirectory()
		{
			if (String.IsNullOrEmpty(_examplesDirectory))
			{
				// Remove trash if it is run from Visual Studio
				_examplesDirectory = Directory.GetCurrentDirectory()
						.Replace("Debug", "")
						.Replace("bin", "")
						.Replace("Release", "")
						.Replace("\\\\", "");
				_examplesDirectory = Path.Combine(_examplesDirectory, "examples"); 
			}

			if (Directory.Exists(_examplesDirectory) == false)
			{
				throw new Exception($"Examples directory `{_examplesDirectory}` does not exist. Please create it and copy some TXT files with data.");
			}

			return _examplesDirectory;
		}
	}
}
