using Microsoft.Maui.Controls.Shapes;
using System.Text;
using JJ2ClientLib.JJ2;

namespace Jazz2Chat;

public partial class MainPage : ContentPage
{
    public Jazz2Chat.ViewModel.MainViewModel VM { get; set; }

    public MainPage(Jazz2Chat.ViewModel.MainViewModel vm)
	{
		InitializeComponent();
		BindingContext = vm;
		VM = vm;
        Init();

    }

	private void OnCounterClicked(object sender, EventArgs e)
	{
        
	}

    private void MenuFlyoutItem_ServerList_Clicked(object sender, EventArgs e)
    {
         Shell.Current.GoToAsync("ServerListPage");
    }

	void Init()
	{
        JJ2Main.Client.Disconnected_Event += Client_Disconnected_Event;
        JJ2Main.Client.Message_Received_Event += Client_Message_Received_Event;
        JJ2Main.Client.Player_Joined_Event += Client_Player_Joined_Event;
        JJ2Main.Client.Player_Left_Event += Client_Player_Left_Event;
        JJ2Main.Client.Players_List_Update_Event += Client_Players_List_Update_Event;
    }

    void UpdatePlayerList(JJ2ClientLib.JJ2.JJ2Client client, string separator = "\n")
    {
        if (client.Connected)
        {
            byte playerCount = 0;
            string[] playersNames = { };
            byte[] PlayersIds = { };
            byte[] playersClientIds = { };
            byte[] playersTeam = { };
            byte[] playersCharacter = { };
            client.GetPlayersList(ref playerCount, ref playersNames, ref PlayersIds, ref playersClientIds, ref playersTeam, ref playersCharacter, false, true);
            StringBuilder res = new StringBuilder(24 * playerCount);
            for (byte i = 0; i < playerCount; ++i)
            {
                res.Append(string.Format("{0}- {1}{2}", PlayersIds[i] + 1, JJ2GeneralFunctions.GetUnformattedName(playersNames[i]), separator));
            }
            Application.Current.MainPage.Dispatcher.Dispatch(() => LabelPlayerList.Text = res.ToString());
        }
        else
        {
            Action<int> printActionDel = delegate (int i)
            {       
            };
            Application.Current.MainPage.Dispatcher.Dispatch(() => { LabelPlayerList.Text = "* Offline"; });
        }

    }

    void SetLabelText(Label dest,string value)
    {
        Application.Current.MainPage.Dispatcher.Dispatch(() => dest.Text = value);
    }

    #region JJ2 Events
    private void Client_Message_Received_Event(string msg, string playerName, byte team, byte playerID, byte playerSocketIndex, object user)
    {
        string line = string.Format("{0}: {1}", playerName, msg);
        VM.AddMessage(line);        
    }

    private void Client_Disconnected_Event(JJ2_Disconnect_Message disconnectMessage, string serverIPAddrees, string serverAddress, ushort serverPort, object user)
    {
        SetLabelText(LabelPlayerList, "Offline");
    }

    private void Client_Player_Joined_Event(string playerName, byte playerID, byte socketIndex, byte character, byte team, object user)
    {
        UpdatePlayerList(JJ2Main.Client, "   ");
        string line = string.Format("{0} joined the game", playerName);
        VM.AddMessage(line);
    }

    private void Client_Player_Left_Event(string playerName, JJ2ClientLib.JJ2.JJ2_Disconnect_Message disconnectMessage, byte playerID, byte socketIndex, object user)
    {
        UpdatePlayerList(JJ2Main.Client, "   ");
        string line = string.Format("{0} left the game", playerName);
        VM.AddMessage(line);
    }

    private void Client_Players_List_Update_Event(byte[] updatedPlayersIDs, byte[] updatedClientsIndices, object user)
    {
        UpdatePlayerList(JJ2Main.Client, "   ");
    }

    #endregion

}

