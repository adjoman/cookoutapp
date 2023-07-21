using System;
using Amazon;
using Amazon.Runtime;
using System.Windows.Input;
using System.ComponentModel;
using Microsoft.AppCenter.Crashes;
using Amazon.CognitoIdentityProvider;
using System.Runtime.CompilerServices;
using Amazon.CognitoIdentityProvider.Model;
using System.Text.RegularExpressions;
using The_Cookout;

namespace The_Cookout.ViewModels
{
    public class RegisterViewModel : INotifyPropertyChanged
    {
        static AWSCredentials credentials = new BasicAWSCredentials((App.Current as App).accessKey, (App.Current as App).secretyKey);

        AmazonCognitoIdentityProviderClient provider =
            new AmazonCognitoIdentityProviderClient(credentials, Amazon.RegionEndpoint.USEast1);

        private string _name;
        private string _email;
        private string _phone;
        private string _password;
        private string _confirmPassword;

        public string FullName
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged();
            }
        }

        public string Password
        {
            get => _password;
            set
            {
                _password = value;
                OnPropertyChanged();
            }
        }

        public string ConfirmPassword
        {
            get => _confirmPassword;
            set
            {
                _confirmPassword = value;
                OnPropertyChanged();
            }
        }

        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged();
            }
        }

        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged();
            }
        }

        public ICommand RegisterCommand { get; }
        public ICommand CancelCommand { get; }

        public RegisterViewModel()
        {
            try
            {
                RegisterCommand = new Command(async () => await OnRegisterClicked());
                CancelCommand = new Command(async () => await Cancel());
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
            }
        }

        // Cancel Button
        async Task Cancel()
        {
            try
            {
                await Shell.Current.GoToAsync("/LoginView");
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
        }

        async Task OnRegisterClicked()
        {
            try
            {
                // Initialize the Cognito client with your AWS credentials
                var cognitoClient = new AmazonCognitoIdentityProviderClient(
                    new Amazon.Runtime.BasicAWSCredentials((App.Current as App).accessKey, (App.Current as App).secretyKey),
                    RegionEndpoint.USEast1
                );

                if (!Regex.IsMatch(Email, @"^([\w-\.]+)@((\[[0-9]{1,3}\.[0-9]{1,3}\.[0-9]{1,3}\.)|(([\w-]+\.)+))([a-zA-Z]{2,4}|[0-9]{1,3})(\]?)$"))
                {
                    await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Invalid Email ", "OK");
                    return;
                }

                if (string.IsNullOrEmpty(Phone))
                {
                    await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Mobile # Required. ex: 40412345678", "OK");
                    return;
                }
                else
                {
                    Phone = Phone.Replace("-", "");   // remove dashses

                    // if the first number is a 1
                    if (Phone.Substring(0, 1) == "1")
                    {
                        await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Remove the 1 from the mobile number", "Ok");
                        return;
                    }

                    // we need a 10 digit phone number
                    if (Phone.Length < 10)
                    {
                        await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Mobile # Not Valid. ex: 40412345678", "Ok");
                        return;
                    }

                    if (Phone.Substring(0, 2) != "+1")
                    {
                        Phone = "+1" + Phone;    // format properly for Cognito
                    }
                }

                // Create the request object to register the user
                var registerRequest = new SignUpRequest
                {
                    ClientId        = (App.Current as App).cognito_client_id,
                    Password        = Password,
                    Username        = Utlilities.ParseBeforeAtSymbol(Email),
                    UserAttributes  = new List<AttributeType>
                    {
                        new AttributeType { Name = "family_name", Value = FullName },
                        new AttributeType { Name = "given_name", Value = FullName },
                        new AttributeType { Name = "email", Value = Email },
                        new AttributeType { Name = "phone_number", Value = Phone },
                    }
                };

                var registerResponse = await cognitoClient.SignUpAsync(registerRequest);
                //await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Welcome to " + (App.Current as App).AppName + ".", "OK");
                await Shell.Current.GoToAsync("/SuccessView");
            }
            catch (Amazon.CognitoIdentityProvider.Model.NotAuthorizedException nAE)
            {
                await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Invalid Email / Password Combination", "OK");
                return;
            }
            catch(Amazon.CognitoIdentityProvider.Model.UsernameExistsException uE)
            {
                await Application.Current.MainPage.DisplayAlert((App.Current as App).AppName, "Username exists. Please login.", "OK");
                return;
            }
            catch (Exception ex)
            {
                Crashes.TrackError(ex);
                throw;
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        protected void OnPropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}

