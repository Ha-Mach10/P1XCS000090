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



        public DrPoint(double x, double y)
        {
            X = x;
            Y = y;
        }




        public DrPoint ReScale(double rate, Point cursorPosition)
        {
            double reScaleX = double.NaN;
            if (cursorPosition.X > X)
            {
                // カーソル点を仮想原点とし、仮想原点からのＸの位置を定義
                double ImaginalyOriginX = X - cursorPosition.X;
                // 仮想原点から位置Ｘへ倍率を乗算
                double reScaledPositionX = ImaginalyOriginX * rate;
                // 仮想原点としたカーソル点を加算し、実際の位置Ｘを算出
                reScaleX = cursorPosition.X + reScaledPositionX;
            }
            else
            {
				// カーソル点を仮想原点とし、仮想原点からのＸの位置を定義
				double ImaginalyOriginX = cursorPosition.X - X;
				// 仮想原点から位置Ｘへ倍率を乗算
				double reScaledPositionX = ImaginalyOriginX * rate;
				// 仮想原点としたカーソル点を加算し、実際の位置Ｘを算出
				reScaleX = cursorPosition.X + reScaledPositionX;
			}

            double reScaleY = double.NaN;
            if (cursorPosition.Y > Y)
            {
                double imaginaryOriginY = Y - cursorPosition.Y;
                double reScaledPositionY = imaginaryOriginY * rate;
                reScaleY = cursorPosition.Y + reScaledPositionY;
            }
            else
            {

            }
        }
    }
}
