﻿using System;
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
		public DrPoint First { get; set; }
		public DrPoint Second { get; set; }
		public DrPoint MiddlePoint { get; private set; }
		public Pen Pen { get; set; }



		// *******************************************************************************
		// Constructors
		// *******************************************************************************

		public DrLine(Drafter drafter, Point first, Point second, Pen pen) : base()
		{
			_idCount++;


			Id = _idCount;
			First = new DrPoint(first);
			Second = new DrPoint(second);
			Pen = pen;

			SetMiddlePoint();
		}
		public DrLine(DrPoint first, DrPoint second, Pen pen) : base()
		{
			_idCount++;


			Id = _idCount;
			First = first;
			Second = second;
			Pen = pen;

			SetMiddlePoint();
		}



		// *******************************************************************************
		// Public Methods
		// *******************************************************************************

		/// <summary>
		/// 
		/// </summary>
		/// <param name="dc"></param>
		/// <param name="first"></param>
		/// <param name="second"></param>
		/// <param name="cursorPosition"></param>
		/// <param name="ratio"></param>
		/// <param name="pen"></param>
		public void DraftLine(DrawingContext dc, Point first, Point second, Point cursorPosition, double ratio)
		{
			Point f = new Point(first.X * ratio, first.Y * ratio);
			Point s = new Point(second.X * ratio, second.Y * ratio);
			dc.DrawLine(Pen, f, s);
		}
		/// <summary>
		/// 線を描画
		/// </summary>
		/// <param name="dc"><see cref="DrawingContext"/>描画をサポートするコンテキスト</param>
		/// <param name="cursorPosition">カーソル位置</param>
		/// <param name="rate">拡大倍率</param>
		public void DraftLine(DrawingContext dc, Point cursorPosition, double scale)
		{
			// 再スケーリング
			(DrPoint first, DrPoint second) = ReScaleLine(scale, cursorPosition, this);
			// 描画開始
			DoDraft(dc, first, second);
		}
		public void DraftLine(DrawingContext dc)
		{
			// 描画開始
			DoDraft(dc, First, Second);
		}



		// *******************************************************************************
		// Private Methods
		// *******************************************************************************

		/// <summary>
		/// System.Windows.PointからP1XCS00090.Shapes.DrPointへの変換をサポート
		/// </summary>
		/// <param name="point"></param>
		/// <returns></returns>
		private Point DrPointToPoint(DrPoint point)
			=> new Point(point.X, point.Y);

		/// <summary>
		/// カスタムコントロールへ提供されるコンテキストを用いて描画する。
		/// </summary>
		/// <param name="dc"></param>
		private void DoDraft(DrawingContext dc, DrPoint first, DrPoint second)
		{
			dc.DrawLine(Pen, DrPointToPoint(first), DrPointToPoint(second));
		}

		/// <summary>
		/// 始点・終点を提供された倍率およびマウスカーソルの座標に従い再計算
		/// </summary>
		/// <param name="rate"></param>
		/// <param name="cursorPoint"></param>
		/// <param name="targetLine"></param>
		/// <returns></returns>
		private (DrPoint first, DrPoint second) ReScaleLine(double rate, Point cursorPoint, DrLine targetLine)
		{
			return new(null, null);
			// return (drPointFirst, drPointSecond);
		}
		/// <summary>
		/// プロパティに設定された開始点-終了点の2等分点をMiddlePointプロパティにセット
		/// </summary>
		private void SetMiddlePoint()
		{
			double x = First.X <= Second.X ? First.X - Second.X : Second.X - First.X;
			double y = First.Y >= Second.Y ? First.Y - Second.Y : Second.Y - First.Y;
			MiddlePoint = new DrPoint(x, y);
		}



		// *******************************************************************************
		// Operator Override
		// *******************************************************************************


	}
}
