using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;
using System.Collections.ObjectModel;

namespace Jazz2Chat.ViewModel
{
    public partial class ServerListViewModel : ObservableObject
    {
        [ObservableProperty]
        ObservableCollection<string> items;

        [ObservableProperty]
        string text;

        public ServerListViewModel()
        {
            items = new ObservableCollection<string>();
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
            //await Shell.Current.GoToAsync("ServerListPage");
        }
    }
}
