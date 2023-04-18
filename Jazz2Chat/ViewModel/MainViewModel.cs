using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Jazz2Chat.ViewModel
{
    public partial class MainViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        [ObservableProperty]
        string playerListLabel = string.Empty;

        public MainViewModel()
        {
            items = new ObservableCollection<string>();
            //JJ2Main.Init();
        }

        [RelayCommand]
        void Add()
        {
            if (string.IsNullOrEmpty(Text))
                return;
            Items.Add(Text);
            Text = string.Empty;
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
            await Shell.Current.GoToAsync("ServerListPage");
        }

        [RelayCommand]
        void Send(string value)
        {
            if (string.IsNullOrEmpty(Text))
                return;
            if(JJ2Main.Client.Connected)
            {
                JJ2Main.Client.SendMessage(Text);
                AddMessage(string.Format("{0}: {1}", JJ2Main.Client.CurrentName, Text));
            }
            else
            {
                AddMessage("You are not connected");
            }
            playerListLabel = "dsdsfds";
            Text = string.Empty;
        }

        public void AddMessage(string value)
        {
            if (string.IsNullOrEmpty(value))
                return;
            Items.Add(value);
        }
    }

 




    /*
    public class MainViewModel : System.ComponentModel.INotifyPropertyChanged
    {
        string _text;
        public string Text { get => _text;
            set
            {
                _text = value;
                OnPropertyChanged(nameof(Text));
            }
        }
        public event PropertyChangedEventHandler PropertyChanged;
        void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
    */
}
