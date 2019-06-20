using Mpyr.BLL;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Mpyr.Ui
{
	public partial class Form1 : Form
	{
		private string _filename = null;
		private int?[,] _matrix = null;

		public Form1()
		{
			InitializeComponent();
		}

		private void numericDelay_ValueChanged(object sender, EventArgs e)
		{

		}

		private void button1_Click(object sender, EventArgs e)
		{
			if (String.IsNullOrEmpty(_filename))
			{
				DisplayError("Please choose file to process");
				return;
			}

			ProcessFile(_filename);
		}

		private void txtFilename_Click(object sender, EventArgs e)
		{
			OpenFileDialog dlg = new OpenFileDialog();
			dlg.Filter = "Text files|*.txt";
			if (dlg.ShowDialog() == DialogResult.OK)
			{
				SetFilename(dlg.FileName);
			}
		}

		private void ProcessFile(string filename)
		{
			try
			{
				string[] lines = File.ReadAllLines(filename);
				_matrix = Helper.BuildMatrix(lines);
				Log("Got matrix of {0} lines", _matrix.GetLength(0));

				DrawMatrix(_matrix);
			}
			catch (Exception ex)
			{
				DisplayError(String.Concat("Error processing file ", filename, ". ", ex.Message), ex);
			}
		}

		private void SetFilename(string filename)
		{
			SetText("txtFilename", filename);
			AddText("txtOutput", String.Concat("File to process ", filename));
			_filename = filename;
		}

		#region Drawings

		private void DrawMatrix(int?[,] matrix)
		{
			int sizeOfMatrix = matrix.GetLength(0);
			using (var g = pictureBox1.CreateGraphics())
			{
				InitGraphics();
				g.Clear(Color.White);

				if (sizeOfMatrix > 15)
				{
					DrawTrialLimit(g);
					return;
				}

				DrawGrid(g, sizeOfMatrix);
				for (int y = 0; y < sizeOfMatrix; y++)
				{
					for (int x = 0; x < matrix.GetLength(1); x++)
					{
						if (matrix[y, x].HasValue)
						{
							DrawCell(g, matrix[y, x].ToString(), x, y);
						}
					}
				}

				HighlightPath(g, new Point[] { new Point(0, 0), new Point(0, 1) }, Color.LightSteelBlue);
			}
		}

		private void InitGraphics()
		{
			_redBrush = new SolidBrush(Color.Red);
			_blackBrush = new SolidBrush(Color.Black);
			_cellFont = new Font("Courier", 8, FontStyle.Regular);
			_grayPen = new Pen(Color.Gray);
		}

		private SolidBrush _redBrush = null;
		private SolidBrush _blackBrush = null;
		private Pen _grayPen = null;
		private int _cellWidth = 5;
		private Font _cellFont = null;

		private void HighlightPath(Graphics g, Point[] points, Color c)
		{
			int w = 3;
			g.DrawRectangles(new Pen(c, w), points.Select(x => new Rectangle
			{
				// Calculate inner rectangle
				X = x.X * _cellWidth + w,
				Y = x.Y * _cellWidth + w,
				Width = _cellWidth - 2 * w,
				Height = _cellWidth - 2 * w,
			}).ToArray());
		}

		private void DrawCell(Graphics g, string text, int x, int y)
		{
			var point = new Point(x * _cellWidth + _cellWidth / 6, y * _cellWidth + _cellWidth / 3);
			g.DrawString(text, _cellFont, _blackBrush, point);
		}

		private void DrawGrid(Graphics g, int total)
		{
			_cellWidth = pictureBox1.Width / total;
			int width = _cellWidth * total;

			for (int x = 0; x <= width; x += _cellWidth)
			{
				g.DrawLine(_grayPen, x, 0, x, width);
			}
			for (int y = 0; y <= width; y += _cellWidth)
			{
				g.DrawLine(_grayPen, 0, y, width, y);
			}
		}

		private void DrawTrialLimit(Graphics g)
		{
			var f = new Font("Sans Serif", 10, FontStyle.Regular);
			var p = new Point(40, pictureBox1.Height / 2);
			g.DrawString("This matrix is too large for Trial version. Upgrade to Pro version for just $99.99", f, _redBrush, p);
		}

		#endregion

		private void DisplayError(string msg)
		{
			DisplayError(msg, null);
		}

		private void DisplayError(string msg, Exception ex)
		{
			MessageBox.Show(msg, "", MessageBoxButtons.OK, MessageBoxIcon.Error);
		}

		private void Log(string fmt, params object[] args)
		{
			AddText("txtOutput", String.Concat(Environment.NewLine, String.Format(fmt, args)));
		}

		#region " Get/set text methods (invoked) "

		delegate void SetTextDelegate(string name, string value);
		delegate string GetTextDelegate(string name);

		private string GetText(string name)
		{
			string s = "";
			if (InvokeRequired)
			{
				Invoke(new GetTextDelegate(GetText), name);
			}
			else
			{
				Control[] ar = Controls.Find(name, false);
				s = ar.FirstOrDefault()?.Text;
			}
			return s;
		}

		private void AddText(string name, string value)
		{
			if (InvokeRequired)
			{
				Invoke(new SetTextDelegate(AddText), name, value);
			}
			else
			{
				Control[] ar = Controls.Find(name, false);
				if (ar?.Length > 0)
				{
					ar[0].Text += value;
					if (ar[0].GetType() == typeof(TextBox))
					{
						((TextBox)ar[0]).SelectionStart = Int32.MaxValue;
					}
				}
			}
		}

		private void SetText(string name, string value)
		{
			if (InvokeRequired)
			{
				Invoke(new SetTextDelegate(SetText), name, value);
			}
			else
			{
				Control[] ar = Controls.Find(name, false);
				if (ar?.Length > 0)
				{
					ar[0].Text = value;
				}
			}
		}

		#endregion
	}
}
