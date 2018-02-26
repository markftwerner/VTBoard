﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Xamarin.Forms;

namespace VTBoard
{
    public partial class App : Application
    {
        public App()
        {
            InitializeComponent();

            //MainPage = new VTBoard.UI.LandingPage();
            MainPage = new NavigationPage(new UI.LandingPage);
        }

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
