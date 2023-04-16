namespace Jazz2Chat;

public partial class AppShell : Shell
{
	public AppShell()
	{
		InitializeComponent();

		Routing.RegisterRoute(nameof(ServerListPage), typeof(ServerListPage));
	}
}
