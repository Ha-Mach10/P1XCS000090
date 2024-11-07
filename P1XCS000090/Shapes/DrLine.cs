using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace P1XCS000090.Shapes
{
	public class DrLine : DrPoint
	{
		// *******************************************************************************
		// Static Readonly Fields
		// *******************************************************************************

		public static readonly DrLine Empty;



		// *******************************************************************************
		// Static Fields
		// *******************************************************************************

		private static int _idCount = 0;



		// *******************************************************************************
		// Properties
		// *******************************************************************************

		public int Id { get; }
		public DrPoint First { get; }
		public DrPoint Second { get; }
		public DrPoint CenterPoint { get; }
		public Pen Pen { get; }



		// *******************************************************************************
		// Constructors
		// *******************************************************************************

		public DrLine(Point first, Point second, Pen pen) : base()
		{
			_idCount++;

			Id = _idCount;
			First = new DrPoint(first);
			Second = new DrPoint(second);
			Pen = pen;

			double x = First.X <= Second.X ? First.X - Second.X : Second.X - First.X;
			double y = First.Y >= Second.Y ? First.Y - Second.Y : Second.Y - First.Y;
			CenterPoint = new DrPoint(x, y);
		}
		public DrLine(int id, Point first, Point second, Pen pen)
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
			dc.DrawLine(Pen, DrPointToPoint(First), DrPointToPoint(Second));
        }
		public void DraftLine(DrawingContext dc, Point first, Point second, double ratio, Point cursorPosition)
        {
			Point f = new Point(first.X * ratio, first.Y * ratio);
			Point s = new Point(second.X * ratio, second.Y * ratio);
			dc.DrawLine(Pen, f, s);
        }
		public void ReScaleLine(double rate, Point cursorPoint, DrLine targetLine)
		{
			DrPoint drPointFirst = base.ReScale(rate, cursorPoint, targetLine.First);
			DrPoint drPointSecond = base.ReScale(rate, cursorPoint, targetLine.Second);

			
		}



		// *******************************************************************************
		// Public Methods
		// *******************************************************************************

		private Point DrPointToPoint(DrPoint point)
			=> new Point(point.X, point.Y);



		// *******************************************************************************
		// Operator Override
		// *******************************************************************************


	}
}
