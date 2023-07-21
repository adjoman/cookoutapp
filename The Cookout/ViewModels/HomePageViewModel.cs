using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using The_Cookout.Models;

namespace The_Cookout.ViewModels
{
	public class HomePageViewModel: INotifyPropertyChanged
    {
        public ObservableCollection<Item> Items { get; set; }

        public HomePageViewModel()
        { 
            Items = new ObservableCollection<Item>
            {
                new Item { Title = "Item 1", Description = "This is item 1" },
                new Item { Title = "Item 2", Description = "This is item 2" },
                // Add more items as required
            };
		}

        public event PropertyChangedEventHandler PropertyChanged;

        protected virtual void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

