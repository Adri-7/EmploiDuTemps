using Microsoft.AdMediator.Core.Models;
using System.Diagnostics;
using Windows.UI.Popups;
using System;
using Windows.Storage;

namespace EmploiDuTempsUniv
{
    public class AdMediatorHandlers
    {
        private static ApplicationDataContainer roamingSettings = ApplicationData.Current.RoamingSettings;

        public static void AdMediator_Bottom_AdSdkEvent(object sender, Microsoft.AdMediator.Core.Events.AdSdkEventArgs e)
        {
            Debug.WriteLine("AdSdk event {0} by {1}", e.EventName, e.Name);
        }

        public static async void AdMediator_Bottom_AdMediatorError(object sender, Microsoft.AdMediator.Core.Events.AdMediatorFailedEventArgs e)
        {
            string message = "AdMediatorError:" + e.Error + " " + e.ErrorCode;

            Debug.WriteLine(message);

            MessageDialog dialog = new MessageDialog(message);

            if(roamingSettings.Values["debugAds"] != null && ((bool)roamingSettings.Values["debugAds"]) == true)
            {
                try
                {
                    await dialog.ShowAsync();
                }
                catch (Exception ex)
                {
                }
            }
        }

        public static void AdMediator_Bottom_AdFilled(object sender, Microsoft.AdMediator.Core.Events.AdSdkEventArgs e)
        {
            Debug.WriteLine("AdFilled:" + e.Name);
        }

        public static async void AdMediator_Bottom_AdError(object sender, Microsoft.AdMediator.Core.Events.AdFailedEventArgs e)
        {
            string message = "AdSdkError by " + e.Name + " ErrorCode: " + e.ErrorCode + " ErrorDescription: " + e.ErrorDescription + " Error: " + e.Error;
            Debug.WriteLine(message);

            MessageDialog dialog = new MessageDialog(message);

            var ad = (Microsoft.AdMediator.WindowsPhone81.AdMediatorControl)sender;
            if(e.ErrorCode.ToString().Contains("Timeout"))
            {
                //Hang
                ad.Disable();
                ad.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
            }

            if (roamingSettings.Values["debugAds"] != null && ((bool)roamingSettings.Values["debugAds"]) == true)
            {
                try
                {
                    await dialog.ShowAsync();
                }
                catch(Exception ex)
                {
                }
            }
        }

        public static void InitAdsOptionalSize(Microsoft.AdMediator.WindowsPhone81.AdMediatorControl control)
        {
            if(control.Height >= 80 && control.ActualWidth >= 480)
            {
                control.AdSdkOptionalParameters[AdSdkNames.MicrosoftAdvertising]["Width"] = 480;
                control.AdSdkOptionalParameters[AdSdkNames.MicrosoftAdvertising]["Height"] = 80;
                control.AdSdkOptionalParameters[AdSdkNames.AdDuplex]["Width"] = 480;
                control.AdSdkOptionalParameters[AdSdkNames.AdDuplex]["Height"] = 80;
                control.AdSdkOptionalParameters[AdSdkNames.Smaato]["Width"] = 320;
                control.AdSdkOptionalParameters[AdSdkNames.Smaato]["Height"] = 50;
            }
            else
            {
                control.AdSdkOptionalParameters[AdSdkNames.MicrosoftAdvertising]["Width"] = 320;
                control.AdSdkOptionalParameters[AdSdkNames.MicrosoftAdvertising]["Height"] = 50;
                control.AdSdkOptionalParameters[AdSdkNames.AdDuplex]["Width"] = 320;
                control.AdSdkOptionalParameters[AdSdkNames.AdDuplex]["Height"] = 50;
                control.AdSdkOptionalParameters[AdSdkNames.Smaato]["Width"] = 320;
                control.AdSdkOptionalParameters[AdSdkNames.Smaato]["Height"] = 50;
            }
        }
    }
}