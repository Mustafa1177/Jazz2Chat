using Microsoft.Maui.Controls;
using Jazz2Chat.JJ2;
using Jazz2Chat.JJ2.DataClasses;

namespace Jazz2Chat;
public partial class ServerListPage : ContentPage
{
	public Jazz2Chat.ViewModel.ServerListViewModel VM { get; set; }
    public List<GameServer> GameServerList { get; set; }

    DateTime lastRefresh = DateTime.MinValue;

	public ServerListPage(Jazz2Chat.ViewModel.ServerListViewModel vm)
	{
		InitializeComponent();
        BindingContext = vm;
        VM = vm;

        if((DateTime.Now - lastRefresh).TotalSeconds >= 60)
        {    
            RefreshList();
        }      	
    }

	void RefreshList()
	{
        lastRefresh = DateTime.Now;
        GameServerList = ListClient.ParseASCIIList(ListClient.DownloadASCIIList());
		for(int i = 0; i < GameServerList.Count; i++)
		{
            //VM.Text = GameServerList[i].Name.Replace("|", string.Empty);
            VM.GameServerList.Add(GameServerList[i]);
            VM.Add((i+1) + "- " + GameServerList[i].Name.Replace("|", string.Empty));
        }
    }

    private void TapGestureRecognizer_Tapped(object sender, EventArgs e)
    {
      
     
    }
}