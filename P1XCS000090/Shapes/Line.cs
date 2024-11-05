using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace P1XCS000090.Shapes
{
	public class Line
	{
		// *******************************************************************************
		// Static Readonly Fields
		// *******************************************************************************

		public static readonly Line Empty;



		// *******************************************************************************
		// Static Fields
		// *******************************************************************************

		private static int _idCount = 0;



		// *******************************************************************************
		// Properties
		// *******************************************************************************

		public int Id { get; }
		public Point First { get; }
		public Point Second { get; }
		public Point CenterPoint { get; }
		public Pen Pen { get; }



		// *******************************************************************************
		// Constructors
		// *******************************************************************************

		public Line(Point first, Point second, Pen pen)
		{
			_idCount++;

			Id = _idCount;
			First = first;
			Second = second;
			Pen = pen;

			double x = First.X <= Second.X ? First.X - Second.X : Second.X - First.X;
			double y = First.Y >= Second.Y ? First.Y - Second.Y : Second.Y - First.Y;
			CenterPoint = new Point(x, y);
		}
		public Line(int id, Point first, Point second, Pen pen)
			: this(first, second, pen)
		{
			Id = id;
		}



		// *******************************************************************************
		// Public Methods
		// *******************************************************************************

		/// <summary>
		/// カスタムコントロールへ提供されるコンテキストを用いて描画する。
		/// </summary>
		/// <param name="dc"></param>
		public void DraftLine(DrawingContext dc)
        {
			dc.DrawLine(Pen, First, Second);
        }
		public void DraftLine(DrawingContext dc, Point first, Point second, double ratio, Point cursorPosition)
        {
			Point f = new Point(first.X * ratio, first.Y * ratio);
			Point s = new Point(second.X * ratio, second.Y * ratio);
			dc.DrawLine(Pen, f, s);
        }



		// *******************************************************************************
		// Operator Override
		// *******************************************************************************


	}
}
