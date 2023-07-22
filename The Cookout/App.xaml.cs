using Amazon.CognitoIdentityProvider.Model;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using The_Cookout.Views;

namespace The_Cookout;

public partial class App : Application
{
    public string AppName = "The Cookout";

    public string accessKey = "AKIAQIWCP3DOAHJPUL5T";
    public string secretyKey = "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";

    public string cognito_client_id = "7kv7cto8h1230rkfhe6al50g31";
    public string cognito_pool_id   = "us-east-1_Vw05Z6mwA";
    public string your_aws_region   = "us-east-1";

    public GetUserResponse loggedInUser = null;

    public App()
	{
		InitializeComponent();

		MainPage = new AppShell();

        Routing.RegisterRoute(nameof(RegisterView), typeof(RegisterView));
        Routing.RegisterRoute(nameof(LoginView), typeof(LoginView));
        Routing.RegisterRoute(nameof(SuccessView), typeof(SuccessView));
        Routing.RegisterRoute(nameof(HomePage), typeof(HomePage));
    }

    protected override void OnStart()
    {
        try
        {
            base.OnStart();
        }
        catch (Exception ex)
        {
            Crashes.TrackError(ex);
        }     
    }
}

