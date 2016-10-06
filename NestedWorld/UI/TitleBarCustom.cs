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
        public static void ApplyToContainerHomePage()
        {
            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = ((SolidColorBrush)Application.Current.Resources["BleuTwoBrush"]).Color;
                    titleBar.ButtonForegroundColor = ((SolidColorBrush)Application.Current.Resources["BackrgoundBrush"]).Color;
                    titleBar.BackgroundColor = ((SolidColorBrush)Application.Current.Resources["BleuTwoBrush"]).Color;
                    titleBar.ForegroundColor = ((SolidColorBrush)Application.Current.Resources["BackrgoundBrush"]).Color;
                }
            }

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = ((SolidColorBrush)Application.Current.Resources["BleuTwoBrush"]).Color;
                    statusBar.ForegroundColor = ((SolidColorBrush)Application.Current.Resources["BackrgoundBrush"]).Color;
                }
            }
        }
        public static void ApplyToContainerMainPage()
        {
            //PC customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.ApplicationView"))
            {
                var titleBar = ApplicationView.GetForCurrentView().TitleBar;
                if (titleBar != null)
                {
                    titleBar.ButtonBackgroundColor = ((SolidColorBrush)Application.Current.Resources["BackrgoundBrush"]).Color;
                    titleBar.ButtonForegroundColor = Colors.Black;
                    titleBar.BackgroundColor = ((SolidColorBrush)Application.Current.Resources["BackrgoundBrush"]).Color;
                    titleBar.ForegroundColor = Colors.Black;
                }
            }

            //Mobile customization
            if (ApiInformation.IsTypePresent("Windows.UI.ViewManagement.StatusBar"))
            {
                var statusBar = StatusBar.GetForCurrentView();
                if (statusBar != null)
                {
                    statusBar.BackgroundOpacity = 1;
                    statusBar.BackgroundColor = ((SolidColorBrush)Application.Current.Resources["BackrgoundBrush"]).Color;
                    statusBar.ForegroundColor = Colors.Black;
                }
            }
        }
    }
}
