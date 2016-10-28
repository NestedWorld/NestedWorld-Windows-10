using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Foundation.Metadata;
using Windows.UI;
using Windows.UI.ViewManagement;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Media;

namespace NestedWorld.UI
{
    public class TitleBarCustom
    {
        public static void BarCustom(Color backgroundColor, Color foregroundColor)
        {
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = backgroundColor;
                    titleBar.ButtonForegroundColor = foregroundColor;
                    titleBar.BackgroundColor = backgroundColor;
                    titleBar.ForegroundColor = foregroundColor;
                }
            }
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = backgroundColor;
                    statusBar.ForegroundColor = foregroundColor;
                }
            }
        }

        public static void ApplyToContainerHomePage()
        {
            TitleBarCustom.BarCustom(Utils.ColorUtils.GetColorFromHex("#FF1C1C25"), Utils.ColorUtils.GetColorFromHex("#FFFFFFFF"));
        }
        public static void ApplyToContainerMainPage()
        {
            TitleBarCustom.BarCustom(Utils.ColorUtils.GetColorFromHex("#FFFFFFFF"), Utils.ColorUtils.GetColorFromHex("#FF1C1C25"));         
        }
    }
}
