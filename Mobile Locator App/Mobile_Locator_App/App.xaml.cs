﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace Mobile_Locator_App
{
	public partial class App : Application
	{
		public App ()
		{
			InitializeComponent();

			MainPage = new Mobile_Locator_App.Xaml.LogInPage(); // the location where the app will start
		}

		protected override void OnStart ()
		{
			// Handle when your app starts
		}

		protected override void OnSleep ()
		{
			// Handle when your app sleeps
		}

		protected override void OnResume ()
		{
			// Handle when your app resumes
		}
	}
}