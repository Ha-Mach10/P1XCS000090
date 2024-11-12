using P1XCS000090.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing.Drawing2D;

namespace P1XCS000090.Shapes
{
	public class DrPoint
	{
		// *********************************************************************
		// Static Fields 
		// *********************************************************************

		/// <summary>
		/// アフィン変換に利用するための内部変数
		/// </summary>
		private Matrix _matrix;



		// *********************************************************************
		// Properties 
		// *********************************************************************

		public double X { get; set; }
		public double Y { get; set; }



		// *********************************************************************
		// Constructors 
		// *********************************************************************

		public DrPoint()
		{
			// 
			_matrix = new Matrix();

			X = 0;
			Y = 0;
		}
		public DrPoint(double x, double y) : this()
		{
			X = x;
			Y = y;
		}
		public DrPoint(Point point) : this()
		{
			X = point.X;
			Y = point.Y;
		}



		// *********************************************************************
		// Public Methods 
		// *********************************************************************

		/// <summary>
		/// 点の位置をスケーリングする
		/// </summary>
		/// <param name="scale">スケール倍率</param>
		/// <param name="cursorPosition">カーソル位置</param>
		/// <param name="mat">マトリックス</param>
		/// <returns></returns>
		public DrPoint ReScale(double scale, Point cursorPosition)
		{
			// スケーリング（アフィン変換）
			_matrix.ScaleAt(scale, cursorPosition);

			System.Drawing.Point[] points = new System.Drawing.Point[0];
			_matrix.TransformPoints(points);

			// return new DrPoint(points)
			return new DrPoint(cursorPosition.X, cursorPosition.Y);
		}
	}
}
