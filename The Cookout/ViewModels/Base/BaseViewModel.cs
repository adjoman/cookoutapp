using System;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace The_Cookout.ViewModels
{
	public class BaseViewModel : INotifyPropertyChanged
    {
        public event PropertyChangedEventHandler PropertyChanged;

        public BaseViewModel()
		{
		}

        public void OnPropertyChanged([CallerMemberName] string name = "") =>
        PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }
}

