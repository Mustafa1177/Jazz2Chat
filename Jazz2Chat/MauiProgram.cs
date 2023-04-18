namespace Jazz2Chat;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		//register with dependency service
		builder.Services.AddSingleton<MainPage>();
        builder.Services.AddSingleton<Jazz2Chat.ViewModel.MainViewModel>();
        builder.Services.AddSingleton<ServerListPage>(); //  builder.Services.AddTransient is used to create new page everytime we navigate? (temp page)
        builder.Services.AddSingleton<Jazz2Chat.ViewModel.ServerListViewModel>();

        return builder.Build();
	}
}
