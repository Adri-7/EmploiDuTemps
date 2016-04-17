using Microsoft.AdMediator.Core.Models;
using System.Diagnostics;

namespace EmploiDuTempsUniv
{
    public class AdMediatorHandlers
    {
        public static void AdMediator_Bottom_AdSdkEvent(object sender, Microsoft.AdMediator.Core.Events.AdSdkEventArgs e)
        {
            Debug.WriteLine("AdSdk event {0} by {1}", e.EventName, e.Name);
        }

        public static void AdMediator_Bottom_AdMediatorError(object sender, Microsoft.AdMediator.Core.Events.AdMediatorFailedEventArgs e)
        {
            Debug.WriteLine("AdMediatorError:" + e.Error + " " + e.ErrorCode);
        }

        public static void AdMediator_Bottom_AdFilled(object sender, Microsoft.AdMediator.Core.Events.AdSdkEventArgs e)
        {
            Debug.WriteLine("AdFilled:" + e.Name);
        }

        public static void AdMediator_Bottom_AdError(object sender, Microsoft.AdMediator.Core.Events.AdFailedEventArgs e)
        {
            Debug.WriteLine("AdSdkError by {0} ErrorCode: {1} ErrorDescription: {2} Error: {3}", e.Name, e.ErrorCode, e.ErrorDescription, e.Error);

            var ad = (Microsoft.AdMediator.WindowsPhone81.AdMediatorControl)sender;
            if(e.ErrorCode.ToString().Contains("Timeout"))
            {
                ad.Disable();
                ad.Visibility = Windows.UI.Xaml.Visibility.Collapsed;
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
            }
            else
            {
                control.AdSdkOptionalParameters[AdSdkNames.MicrosoftAdvertising]["Width"] = 320;
                control.AdSdkOptionalParameters[AdSdkNames.MicrosoftAdvertising]["Height"] = 50;
                control.AdSdkOptionalParameters[AdSdkNames.AdDuplex]["Width"] = 320;
                control.AdSdkOptionalParameters[AdSdkNames.AdDuplex]["Height"] = 50;
            }
        }
    }
}