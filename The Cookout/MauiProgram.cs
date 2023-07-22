using CommunityToolkit.Maui;
using Microsoft.AppCenter;
using Microsoft.AppCenter.Analytics;
using Microsoft.AppCenter.Crashes;
using Microsoft.Extensions.Logging;

namespace The_Cookout;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
            .UseMauiCommunityToolkit()
            .ConfigureFonts(fonts =>
			{
                fonts.AddFont("Antone-Block.otf", "AntoneBlock");
                fonts.AddFont("Antone-Clean.otf", "AntoneClean");
                fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

#if DEBUG
		builder.Logging.AddDebug();
#endif


        AppCenter.Start("ios=83bcb6f9-77e8-435b-8ba9-f1ffbf0e56bc;" +
                      "android=5a66adde-0f53-4f34-9b16-72ddda9b5c8b;",
                      typeof(Analytics), typeof(Crashes));

        return builder.Build();
	}
}

