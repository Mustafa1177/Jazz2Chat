namespace Jazz2Chat;

public partial class MainPage : ContentPage
{
	int count = 0;

	public MainPage(Jazz2Chat.ViewModel.MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
	}

	private void OnCounterClicked(object sender, EventArgs e)
	{

	}

    private void MenuFlyoutItem_ServerList_Clicked(object sender, EventArgs e)
    {
         Shell.Current.GoToAsync("ServerListPage");
    }
}

