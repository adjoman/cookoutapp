using System;
using Microsoft.AppCenter.Crashes;
using System.Windows.Input;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon;
using Amazon.Runtime;
using The_Cookout.Views;

namespace The_Cookout.ViewModels
{
    public class SuccessPageViewModel : BaseViewModel
    {
        INavigation navigation;

        public ICommand CompleteRegistrationCommand => new Command(async () => await gotoCreateAccount());
        
        public SuccessPageViewModel(INavigation _navigation)
        {
            try
            {
                navigation = _navigation;
            }
            catch (Exception)
            {
                throw;
            }   
        }

        public async Task gotoCreateAccount()
        {
            try
            {
                string test = string.Empty;
                //await navigation.PushAsync(new Views.CreateAccount());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
        }
    }
}

