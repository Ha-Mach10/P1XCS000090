using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Media;

namespace P1XCS000090.Math
{
	internal static class Affine
	{
		public static double[,] matrix3D = new double[3, 3];


		public static void MatrixInitialize()
		{
			Matrix matrix = new Matrix();
			matrix.Scale(1, -1);
		}

		// public static 
	}
}
