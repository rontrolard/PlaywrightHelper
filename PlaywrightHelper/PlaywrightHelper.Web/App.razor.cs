using Avalonia.ReactiveUI;
using Avalonia.Web.Blazor;

namespace PlaywrightHelper.Web;

public partial class App
{
    protected override void OnParametersSet()
    {
        base.OnParametersSet();

        WebAppBuilder.Configure<PlaywrightHelper.App>()
            .UseReactiveUI()
            .SetupWithSingleViewLifetime();
    }
}