using System;
using Microsoft.AppCenter.Crashes;
using System.Windows.Input;
using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Amazon;
using Amazon.Runtime;
using NiftyNeighbor;

namespace NiftyNeighbor.ViewModels
{
    public class LoginPageViewModel : BaseViewModel
    {
        INavigation navigation;

        private string email;
        private string password;

        public ICommand SignInCommand => new Command(async () => await SignIn());
        public ICommand CreatAccountCommand => new Command(async () => await gotoCreateAccount());
        
        public LoginPageViewModel(INavigation _navigation)
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

        public string Email
        {
            get => email;
            set
            {
                email = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => password;
            set
            {
                password = value;
                OnPropertyChanged();
            }
        }

        async Task SignIn()
        {
            try
            {
                // are the email and password 
                if (string.IsNullOrEmpty(Email) || string.IsNullOrEmpty(Password))
                {
                    await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Email and Password are required", "OK");
                    return;
                }

                var credentials = new BasicAWSCredentials((App.Current as App).accessKey, (App.Current as App).secretyKey);

                // Instantiate the Cognito client
                var cognito = new AmazonCognitoIdentityProviderClient(credentials, RegionEndpoint.USEast1);

                // Define the authentication details
                var authDetails = new InitiateAuthRequest
                {
                    AuthFlow = AuthFlowType.USER_PASSWORD_AUTH,
                    ClientId = (App.Current as App).cognito_client_id,
                    AuthParameters = new Dictionary<string, string>
                    {
                        { "USERNAME", Email },
                        { "PASSWORD", Password }
                    }
                };

                // Call the Cognito service to authenticate the user
                var authResponse = await cognito.InitiateAuthAsync(authDetails);

                GetUserResponse getUser;

                // Check if the authentication was successful
                if (authResponse.AuthenticationResult != null)
                {
                    getUser = await cognito.GetUserAsync(
                       new GetUserRequest { AccessToken = authResponse.AuthenticationResult.AccessToken }
                   );

                    (App.Current as App).loggedInUser = getUser;    // set the user globally 
                    Console.WriteLine("Authentication successful!");
                    await Shell.Current.GoToAsync("/HomePage");
                }
                else
                {
                    await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Email and Password are required", "OK");
                    return;
                }

                return;
            }
            catch (Amazon.CognitoIdentityProvider.Model.NotAuthorizedException nAe)
            {
                Microsoft.AppCenter.Analytics.Analytics.TrackEvent("Login", new Dictionary<string, string> { { "Invalid", Email } });
                await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Invalid Email / Password Combination", "OK");
                return;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
        }

        public async Task gotoCreateAccount()
        {
            try
            {
               // await navigation.PushAsync(new Views.CreateAccount());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
        }
    }
}

