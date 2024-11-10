using System;
using System.Collections.Generic;
// using System.Drawing;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Runtime.InteropServices;

using P1XCS000090.Commands;
using P1XCS000090.Shapes;
using P1XCS000090.Math;

namespace P1XCS000090
{
	/// <summary>
	/// このカスタム コントロールを XAML ファイルで使用するには、手順 1a または 1b の後、手順 2 に従います。
	///
	/// 手順 1a) 現在のプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
	/// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
	/// 追加します:
	///
	///     xmlns:MyNamespace="clr-namespace:P1XCS000090"
	///
	///
	/// 手順 1b) 異なるプロジェクトに存在する XAML ファイルでこのカスタム コントロールを使用する場合
	/// この XmlNamespace 属性を使用場所であるマークアップ ファイルのルート要素に
	/// 追加します:
	///
	///     xmlns:MyNamespace="clr-namespace:P1XCS000090;assembly=P1XCS000090"
	///
	/// また、XAML ファイルのあるプロジェクトからこのプロジェクトへのプロジェクト参照を追加し、
	/// リビルドして、コンパイル エラーを防ぐ必要があります:
	///
	///     ソリューション エクスプローラーで対象のプロジェクトを右クリックし、
	///     [参照の追加] の [プロジェクト] を選択してから、このプロジェクトを参照し、選択します。
	///
	///
	/// 手順 2)
	/// コントロールを XAML ファイルで使用します。
	///
	///     <MyNamespace:Drafter/>
	///
	/// </summary>
	public class Drafter : Canvas
	{
		// *********************************************************************
		// Data 
		// *********************************************************************

		public enum DisplayTheme
		{
			Dark,
			Light,
		}



		// *********************************************************************
		// Fields 
		// *********************************************************************

		// ダーク
		private Brush _backGroundBrush = new SolidColorBrush(Color.FromRgb(24,25,28));
		// ホワイト
		private Brush _foreGroundBrush = new SolidColorBrush(Colors.White);
		// カーソル座標
		private Point _cursorPosition;
		



		// *********************************************************************
		// Properyies
		// *********************************************************************

		public static System.Drawing.Size Size { get; private set; }
		/// <summary>
		/// 画面倍率
		/// </summary>
		public static double Ratio { get; private set; } = 1.0;
		/// <summary>
		/// 線分オブジェクト
		/// </summary>
		public static List<DrLine> Lines { get; private set; } = new List<DrLine>();
		/// <summary>
		/// 円オブジェクト
		/// </summary>
		public static List<DrCircle> Circles { get; private set; } = new List<DrCircle>();



		// *********************************************************************
		// Dependency Properyies
		// *********************************************************************

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty CommandMessageProperty
			= DependencyProperty.Register(
				"CommandMessage",
				typeof(CommandMessage),
				typeof(Drafter),
				new FrameworkPropertyMetadata(
					new CommandMessage(),
					(d, e) => { }));
		public CommandMessage CommandMessage
		{
			get => (CommandMessage)GetValue(CommandMessageProperty);
			set => SetValue(CommandMessageProperty, value);
		}

		/// <summary>
		/// 
		/// </summary>
		public static readonly DependencyProperty ThemeProperty
			= DependencyProperty.Register(
				"Theme",
				typeof(DisplayTheme),
				typeof(Drafter),
				new FrameworkPropertyMetadata(
					DisplayTheme.Dark,
					(d, e) =>
					{

					}));
		public DisplayTheme Theme
		{
			get => (DisplayTheme)GetValue(ThemeProperty);
			set => SetValue(ThemeProperty, value);
		}

		/// <summary>
		/// OnRenderの意図的な呼び出しを行う為の依存関係プロパティ
		/// bool値の切替のみを行う
		/// </summary>
		public static readonly DependencyProperty ReloadProperty
			= DependencyProperty.Register(
				"Reload",
				typeof(bool),
				typeof(Drafter),
				new FrameworkPropertyMetadata(
					false,
					FrameworkPropertyMetadataOptions.AffectsRender,
					(d, e) => { }));
		public bool Reload
		{
			get => (bool)GetValue(ReloadProperty);
			set => SetValue(ReloadProperty, value);
		}



		// *********************************************************************
		// Constructor
		// *********************************************************************

		static Drafter()
		{
			// 
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Drafter), new FrameworkPropertyMetadata(typeof(Drafter)));
			
			/*
			int lineSz = Marshal.SizeOf<Line>();
			int circleSz = Marshal.SizeOf<Circle>();
			*/

			int ownerOrigin = Marshal.SizeOf(typeof(OwnerOrigin));


			DrLine drLine = new DrLine(new Point(100, 100), new Point(300, 300), new Pen(new SolidColorBrush(Colors.White), 1));
			Lines.Add(drLine);

			int a = 0;
		}



		// *********************************************************************
		// Event
		// *********************************************************************

		public event RoutedEventHandler SendMessage;

		protected virtual void OnSendMessage(RoutedEventArgs e)
		{

		}



		// *********************************************************************
		// Method Override
		// *********************************************************************

		/// <summary>
		/// コントロール本体へのレンダリング処理を行う
		/// </summary>
		/// <param name="dc"></param>
		protected override void OnRender(DrawingContext dc)
		{
			// 基底メソッドの呼び出し
			base.OnRender(dc);

			foreach (DrLine line in Lines)
			{
				line.DraftLine(dc, _cursorPosition, Ratio);
			}

			/*
			double positionA = 100 * Ratio;
			double positionB = 300 * Ratio;
			Point firstPoint1 = new Point(positionA, positionA);
			Point secondPoint1 = new Point(positionB, positionB);

			
			Vector vector = new Vector(_cursorPosition.X * Ratio, _cursorPosition.Y * Ratio);
			Point firstPoint2 =  Point.Subtract(new Point(positionA, positionA), vector);
			Point secondPoint2 = Point.Subtract(new Point(positionB, positionB), vector);
			*/
			/*
			Matrix matrixFirst = new Matrix(100, 0, 0, 100, 0, 0);
			Matrix matrixSecond = new Matrix(300, 0, 0, 300, 0, 0);
			matrixFirst.Scale(Ratio, Ratio);
			matrixSecond.Scale(Ratio, Ratio);

			dc.DrawLine(new Pen(_foreGroundBrush, 2.0), new Point(matrixFirst.M11, matrixFirst.M22), new Point(matrixSecond.M11, matrixSecond.M22));
			*/

			

			// dc.DrawLine(new Pen(_foreGroundBrush, 2.0), CalculatePosition(firstPoint1, _cursorPosition, Ratio), CalculatePosition(secondPoint1, _cursorPosition, Ratio));
		}
		/// <summary>
		/// テンプレートの適用を行うメソッド
		/// 参考サイト：https://blog.okazuki.jp/entry/2014/09/08/221209
		/// </summary>
		public override void OnApplyTemplate()
		{
			// 基底メソッドのコール
			base.OnApplyTemplate();

			// 下記イベントハンドラの購読解除＆新規テンプレートからのコントロールの取得



		}
		protected override void OnRenderSizeChanged(SizeChangedInfo sizeInfo)
		{
			base.OnRenderSizeChanged(sizeInfo);



			var s = sizeInfo.NewSize;
			// var ss = sizeInfo.


		}
		protected override bool ShouldSerializeProperty(DependencyProperty dp)
		{
			return base.ShouldSerializeProperty(dp);

		}
		protected override System.Windows.Size ArrangeOverride(System.Windows.Size arrangeSize)
		{
			Background = _backGroundBrush;
			return base.ArrangeOverride(arrangeSize);
		}
		/// <summary>
		/// 
		/// </summary>
		/// <param name="e"></param>
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);

			if (e.Key == Key.Enter)
			{

			}
		}
		protected override void OnMouseDown(MouseButtonEventArgs e)
		{
			base.OnMouseDown(e);

			if (e.LeftButton == MouseButtonState.Pressed)
			{

			}
			if (e.MiddleButton == MouseButtonState.Pressed)
			{
				if (e.ClickCount is 2)
                {
					Ratio = 1;
					_cursorPosition = new Point(0, 0);
					Reload = !Reload;
                }
			}
		}

		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			base.OnMouseWheel(e);

			// 入力デバイスを取得
			IInputElement device = null;
			device = e.Device as IInputElement;
			// 現在のマウス位置を取得
			_cursorPosition = e.GetPosition(device);

			// ホイールのデルタ値を取得
			int delta = e.Delta;
			if (delta > 0 && Ratio < 100)
			{
				// 拡大
				Ratio = Ratio * 1.125;
				// 状態変更（OnRender呼び出しのため）
				Reload = !Reload;
			}
			else if (delta < 0 && Ratio >= 0.01)
			{
				// 縮小
				Ratio = Ratio * 0.875;
				// 状態変更（OnRender呼び出しのため）
				Reload = !Reload;
			}


		}

		protected override void OnMouseMove(MouseEventArgs e)
		{
			base.OnMouseMove(e);

			if (e.MiddleButton == MouseButtonState.Pressed)
			{
				
			}
		}



		// *********************************************************************
		// Private Methods
		// *********************************************************************

		private Point CalculatePosition(Point objectPosition, Point cursorPosition, double ratio)
        {
			double resultPositionX =
				objectPosition.X > cursorPosition.X ?
				objectPosition.X * ratio + cursorPosition.X :
				cursorPosition.X - objectPosition.X * ratio;
			double resultPositionY =
				objectPosition.Y > cursorPosition.Y ?
				objectPosition.Y * ratio + cursorPosition.Y :
				cursorPosition.Y - objectPosition.Y * ratio;

			return new Point(resultPositionX, resultPositionY);
        }

		private bool IsNegative(Point first, Point second, Point basePos)
        {
			// オブジェクト座標（first.X, second.X）と拡大中心座標basePos.X
			if ((first.X + second.X) - basePos.X < 0)
            {

            }

			return false;
        }
	}
}
