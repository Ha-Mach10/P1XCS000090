using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace P1XCS000090.Shapes
{
	public struct Line
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



		// *******************************************************************************
		// Constructor
		// *******************************************************************************

		public Line(Point first, Point second)
		{
			_idCount++;

			Id = _idCount;
			First = first;
			Second = second;

			double x = First.X <= Second.X ? First.X - Second.X : Second.X - First.X;
			double y = First.Y >= Second.Y ? First.Y - Second.Y : Second.Y - First.Y;
			CenterPoint = new Point(x, y);
		}
		public Line(int id, Point first, Point second)
			: this(first, second)
		{
			Id = id;
		}



		// *******************************************************************************
		// Operator Override
		// *******************************************************************************


	}
}
