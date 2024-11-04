using System;
using System.Collections.Generic;
using System.Windows;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1XCS000090.Shapes
{
	public struct Circle
	{
		// *******************************************************************************
		// Static Readonly Fields
		// *******************************************************************************

		public static readonly Circle Empry;



		// *******************************************************************************
		// Static Fields
		// *******************************************************************************

		private static int _idCount = 0;



		// *******************************************************************************
		// Properties
		// *******************************************************************************

		public int Id { get; }
		public Point Center { get; }
		public double Radius { get; }

		/// <summary>
		/// 四半円点を格納する配列
		/// </summary>
		public Point[] QuadrantPoints { get; }



		// *******************************************************************************
		// Constructor
		// *******************************************************************************

		public Circle(Point center, double radius, bool isDirmeter = false)
		{
			_idCount++;

			Id = _idCount;

			Center = center;

			if (isDirmeter is false)
			{
				Radius = radius;
			}
			else
			{
				Radius = radius / 2;
			}

			QuadrantPoints = new Point[4];

			for (int i = 0; i < 4; i++)
			{
				Point qp = new Point();
				switch (i)
				{
					case 0:
						qp = Point.Subtract(Center, new Vector(Radius, 0));
						break;
					case 1:
						qp = Point.Subtract(Center, new Vector(0, Radius));
						break;
					case 2:
						qp = Point.Subtract(Center, new Vector(-Radius, 0.0));
						break;
					case 3:
						qp = Point.Subtract(Center, new Vector(0, -Radius));
						break;
				}
				QuadrantPoints[i] = qp;
			}
		}
		public Circle(int id, Point center, float radius, bool isDirmeter = false)
			: this(center, radius, isDirmeter)
		{
			Id = id;
		}



		// *******************************************************************************
		// Operator Overload
		// *******************************************************************************


	}
}
