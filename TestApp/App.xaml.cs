﻿using MahApps.Metro.Controls;
using Prism.DryIoc;
using Prism.Ioc;
using System.Windows;
using TestApp.Views;

namespace TestApp
{
	/// <summary>
	/// Interaction logic for App.xaml
	/// </summary>
	public partial class App : PrismApplication
	{
		protected override Window CreateShell()
		{
			return Container.Resolve<MainWindow>();
		}

		protected override void RegisterTypes(IContainerRegistry containerRegistry)
		{

		}
	}
}
