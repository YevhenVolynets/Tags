using System;
using System.Collections.Generic;
using Plugin.Fingerprint;
using Plugin.Fingerprint.Abstractions;
using Xamarin.Forms;

namespace Tags
{
    public partial class AuthPage : ContentPage
    {
        public AuthPage()
        {
            InitializeComponent();
            //var text = DependencyService.Get<IAuthService>().GetAuthInfo();
            //btn.Text += text;
        }

        async void btn_Clicked(System.Object sender, System.EventArgs e)
        {
            //var result = await DependencyService.Get<IAuthService>().AuthenticateMe();
            //btn.Text = result ? "Yes" : "No";
            bool isFingerprintAvailable = await CrossFingerprint.Current.IsAvailableAsync(false);
            if (!isFingerprintAvailable)
            {
                await DisplayAlert("Error",
                    "Biometric authentication is not available or is not configured.", "OK");
                return;
            }

            AuthenticationRequestConfiguration conf =
                new AuthenticationRequestConfiguration("Authentication",
                "Authenticate access to your personal data");
            conf.AllowAlternativeAuthentication = false;
            conf.CancelTitle = "CancelTitle";
            conf.FallbackTitle = "FallbackTitle";
            conf.HelpTexts.Dirty = "Dirty";
            conf.HelpTexts.Insufficient = "Insufficient";
            conf.HelpTexts.MovedTooFast = "MovedTooFast";
            conf.HelpTexts.Partial = "partial";

            try
            {
                var type = await CrossFingerprint.Current.GetAuthenticationTypeAsync();
                var authResult = await CrossFingerprint.Current.AuthenticateAsync(conf);
                if (authResult.Authenticated)
                {
                    //Success  
                    await DisplayAlert("Success", "Authentication succeeded", "OK");
                }
                else
                {
                    await DisplayAlert("Error", "Authentication failed", "OK");
                }
            }
            catch (Exception ex)
            {

            }
            finally
            {
                CrossFingerprint.Dispose();
            }
     
        }
    }
}
