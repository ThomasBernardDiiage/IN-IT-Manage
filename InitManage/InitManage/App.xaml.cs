﻿using InitManage.Views.Pages;

namespace InitManage;

public partial class App : Application
{
	public App()
	{
		InitializeComponent();

		MainPage = new LoginPage();
	}
}

