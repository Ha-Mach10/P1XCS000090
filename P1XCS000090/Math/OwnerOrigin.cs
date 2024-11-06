using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P1XCS000090.Math
{
    public struct OwnerOrigin
    {
        public static readonly double OriginX = 0;
        public static readonly double OriginY = 0;


        /// <summary>
        /// ユーザー座標系のX軸補正量
        /// </summary>
        public double OffsetX { get; }
        /// <summary>
        /// ユーザー座標系のY軸補正量
        /// </summary>
        public double OffsetY { get; }



        public OwnerOrigin(double offsetX, double offsetY)
        {
            OffsetX = offsetX;
            OffsetY = offsetY;
        }
    }
}
