using Amazon.CognitoIdentityProvider;
using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AppCenter.Crashes;

namespace The_Cookout.Views;

public partial class ForgotPassword
{
    private AmazonCognitoIdentityProviderClient _client;

    public ForgotPassword()
	{
		InitializeComponent();
        _client = new AmazonCognitoIdentityProviderClient(Amazon.RegionEndpoint.GetBySystemName((App.Current as App).your_aws_region));
    }

    void CloseButton_Clicked(System.Object sender, System.EventArgs e)
    {
		try
		{
			Close();
		}
		catch (Exception ex)
		{
            Crashes.TrackError(ex);
			throw;
		}
    }

    async void SubmitButton_Clicked(System.Object sender, System.EventArgs e)
    {
        try
        {
            var response = await _client.ForgotPasswordAsync(
                new ForgotPasswordRequest
                {
                    ClientId = (App.Current as App).cognito_client_id,
                    Username = email_address.Text
                });

            if (response.HttpStatusCode == System.Net.HttpStatusCode.OK)
            {
                //Console.WriteLine($"Password reset code sent to user: {username}");
            }
            else
            {
                //Console.WriteLine($"Failed to send password reset code to user: {username}. HTTP status code: {response.HttpStatusCode}");
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Exception occurred while resetting password: {ex.Message}");
        }
    }
}
