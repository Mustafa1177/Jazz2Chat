using Jazz2Chat.JJ2;
using Jazz2Chat.JJ2.DataClasses;

namespace Jazz2Chat;
public partial class ServerListPage : ContentPage
{
	public Jazz2Chat.ViewModel.ServerListViewModel VM { get; set; }
    public List<GameServer> GameServerList { get; set; }


	public ServerListPage(Jazz2Chat.ViewModel.ServerListViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        VM = vm;

        RefreshList();
	
    }

	void RefreshList()
	{
        GameServerList = ListClient.ParseASCIIList(ListClient.DownloadASCIIList());
		for(int i = 0; i < GameServerList.Count; i++)
		{
            VM.Text = GameServerList[i].Name.Replace("|", string.Empty);
            VM.Add();
        }
    }
}