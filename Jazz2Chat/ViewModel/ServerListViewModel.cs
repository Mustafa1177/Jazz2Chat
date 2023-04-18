using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;
using Jazz2Chat.JJ2.DataClasses;

namespace Jazz2Chat.ViewModel
{
    public partial class ServerListViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [ObservableProperty]
        List<GameServer> gameServerList;
        //ObservableCollection<GameServer> gameServerList;



        public ServerListViewModel()
        {
            items = new ObservableCollection<string>();
            //GameServerList = new ObservableCollection<GameServer>();
            GameServerList = new List<GameServer>();
        }

        [RelayCommand]
      public  void Add()
        {
            if (string.IsNullOrEmpty(Text))
                return;
            Items.Add(Text);
            Text = string.Empty;
        }

        public void Add(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            Items.Add(value);
        }

        [RelayCommand]
        void Delete(string value)
        {
            if (this.Items.Contains(value))
            {
                Items.Remove(value);
            }
        }

        [RelayCommand]
        async Task Tap(string value)
        {
            int serverIndex;
            int.TryParse(value.Substring(0,value.IndexOf('-')), out serverIndex);
            serverIndex--;
            if (serverIndex >= 0 && serverIndex < GameServerList.Count)
            {
                JJ2Main.Client.JoinServer(GameServerList[serverIndex].IP, JJ2Main.Client, "Name", (ushort)GameServerList[serverIndex].Port);
            }
            else
            {

            }
            
    
             //Shell.Current.GoToAsync("MainPage");
        }
    }
}
