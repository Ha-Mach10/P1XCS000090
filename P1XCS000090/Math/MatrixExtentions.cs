using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Drawing.Drawing2D;

using P1XCS000090.Shapes;

namespace P1XCS000090.Math
{
    public static class MatrixExtentions
    {
        // *******************************************************************************
        // Extention System.Drawing.Drawing2D.Matrix
        // *******************************************************************************

        /// <summary>
        /// マウス座標を中心としてスケーリングを行う
        /// </summary>
        /// <param name="mat"></param>
        /// <param name="scale"></param>
        /// <param name="cursorPosition"></param>
        public static void ScaleAt(this Matrix mat, double scale, Point cursorPosition)
        {
            // カーソル座標をアフィン変換の中心座標として利用
            System.Drawing.PointF center = new((float)cursorPosition.X, (float)cursorPosition.Y);
            // 
            float scaleF = (float)scale;

            // カーソル座標減算し原点（0,0）にセット
            mat.Translate(-center.X, -center.Y, MatrixOrder.Append);
            // スケーリング
            mat.Scale(scaleF, scaleF, MatrixOrder.Append);
            // カーソル座標を加算する
            mat.Translate(center.X, center.Y, MatrixOrder.Append);
        }
    }
}
