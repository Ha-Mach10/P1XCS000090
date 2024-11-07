using P1XCS000090.Math;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Media;

namespace P1XCS000090.Shapes
{
	public class DrPoint
	{
		public double X { get; }
		public double Y { get; }


		public DrPoint()
		{
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




		public DrPoint ReScale(double rate, Point cursorPosition)
			=> ReScale(rate, cursorPosition, this);

		public DrPoint ReScale(double rate, Point cursorPosition, DrPoint targetPoint)
		{
			// カーソル点を仮想原点とし、仮想原点からのＸの位置を定義
			double ImaginalyOriginX = targetPoint.X - cursorPosition.X;
			// 仮想原点から位置Ｘへ倍率を乗算
			double reScaledPositionX = ImaginalyOriginX * rate;

			// 
			double reScaleX = 0;
			if (cursorPosition.X > targetPoint.X)
			{
				// 仮想原点としたカーソル点を加算し、実際の位置Ｘを算出
				reScaleX = cursorPosition.X + reScaledPositionX;
			}
			else
			{
				// 仮想原点としたカーソル点を加算し、実際の位置Ｘを算出
				reScaleX = cursorPosition.X - reScaledPositionX;
			}


			// カーソル点を仮想原点とし、仮想原点からのＹの位置を定義
			double imaginaryOriginY = targetPoint.Y - cursorPosition.Y;
			// 仮想原点から位置Ｙへ倍率を乗算
			double reScaledPositionY = imaginaryOriginY * rate;

			// 
			double reScaleY = 0;
			if (cursorPosition.Y > targetPoint.Y)
			{
				// 仮想原点としたカーソル点を加算し、実際の位置Ｙを算出
				reScaleY = cursorPosition.Y + reScaledPositionY;
			}
			else
			{
				// 仮想原点としたカーソル点を加算し、実際の位置Ｙを算出
				reScaleY = cursorPosition.Y - reScaledPositionY;
			}

			return new DrPoint(reScaleX, reScaleY);
		}
	}
}
