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
		// Static Fields 
		// *********************************************************************

		// ダークテーマ
		private static Brush _backGroundBrush = new SolidColorBrush(Color.FromRgb(24,25,28));
		// ホワイトテーマ
		private static Brush _foreGroundBrush = new SolidColorBrush(Colors.White);

		// カーソル座標
		private static Point _cursorPosition;
		// マトリックス
		private static Matrix _matrixAffine;

		// 初期化されたかを示す状態
		private static bool s_InitializedFlag = false;
		



		// *********************************************************************
		// Properyies
		// *********************************************************************

		public static System.Drawing.Size Size { get; private set; }
		/// <summary>
		/// 画面倍率
		/// </summary>
		public static double Scale { get; private set; } = 1.0;
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
			// プロパティのメタデータをオーバーライド
			DefaultStyleKeyProperty.OverrideMetadata(typeof(Drafter), new FrameworkPropertyMetadata(typeof(Drafter)));

			// 
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

			if (s_InitializedFlag is false)
            {
				Initialize();
            }

			foreach (DrLine line in Lines)
			{
				line.DraftLine(dc, _cursorPosition, Scale);
			}

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
					Scale = 1;
					_cursorPosition = new Point(0, 0);
					Reload = !Reload;
                }
			}
		}

		/// <summary>
		/// マウスホイールイベント
		/// 拡大・縮小時の倍率を設定
		/// </summary>
		/// <param name="e"></param>
		protected override void OnMouseWheel(MouseWheelEventArgs e)
		{
			// 基底メソッド呼び出し
			base.OnMouseWheel(e);

			// 入力デバイスを取得
			IInputElement device = e.Device as IInputElement;

			// 現在のマウス位置を取得
			_cursorPosition = e.GetPosition(device);

			// ホイールのデルタ値を取得
			int delta = e.Delta;

			if (delta > 0 && Scale < 100)
			{
				// 拡大
				Scale = Scale * 1.125;
				// 状態変更（OnRender呼び出しのため）
				Reload = !Reload;
			}
			else if (delta < 0 && Scale >= 0.01)
			{
				// 縮小
				Scale = Scale * 0.875;
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

		private void Initialize()
        {
			// 
			s_InitializedFlag = true;

			// Initialize Properties

			// 描画変形の原点を定義
			RenderTransformOrigin = new Point(0.5, 0.5);
        }
	}
}
