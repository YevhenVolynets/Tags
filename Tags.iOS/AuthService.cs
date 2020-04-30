using System;
using System.Threading.Tasks;
using Foundation;
using LocalAuthentication;
using Tags.iOS;
using UIKit;

[assembly: Xamarin.Forms.Dependency(typeof(AuthService))]
namespace Tags.iOS
{
    public class AuthService : IAuthService
    {
        string BiometryType;

        public string GetAuthInfo()
        {
            var context = new LAContext();
            string buttonText;

            // Is login with biometrics possible?
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out var authError1))
            {
                // has Touch ID or Face ID
                if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
                {
                    context.LocalizedReason = "Authorize for access to secrets"; // iOS 11
                    BiometryType = context.BiometryType == LABiometryType.TouchId ? "Touch ID" : "Face ID";
                    buttonText = $"Login with {BiometryType}";
                }
                // No FaceID before iOS 11
                else
                {
                    buttonText = $"Login with Touch ID";
                }
            }

            // Is pin login possible?
            else if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out var authError2))
            {
                buttonText = $"Login"; // with device PIN
                BiometryType = "Device PIN";
            }

            // Local authentication not possible
            else
            {
                // Application might choose to implement a custom username/password
                buttonText = "Use unsecured";
                BiometryType = "none";
            }
            return buttonText;
        }

        public async Task<bool> AuthenticateMe()
        {
            var task = new TaskCompletionSource<bool>();
            var context = new LAContext();
            NSError AuthError;
            var localizedReason = new NSString("To access secrets");

            // Because LocalAuthentication APIs have been extended over time,
            // you must check iOS version before setting some properties
            context.LocalizedFallbackTitle = "Enter Username/Password";

            if (UIDevice.CurrentDevice.CheckSystemVersion(10, 0))
            {
                //context.LocalizedCancelTitle = "Cancel";
            }
            if (UIDevice.CurrentDevice.CheckSystemVersion(11, 0))
            {
                context.LocalizedReason = "Authorize for access to secrets";
                BiometryType = context.BiometryType == LABiometryType.TouchId ? "TouchID" : "FaceID";
            }

            // Check if biometric authentication is possible
            if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, out AuthError))
            {
                var replyHandler = new LAContextReplyHandler((success, error) =>
                {
                    task.SetResult(success);
                });
                context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthenticationWithBiometrics, localizedReason, replyHandler);
            }

            // Fall back to PIN authentication
            else if (context.CanEvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, out AuthError))
            {
                var replyHandler = new LAContextReplyHandler((success, error) =>
                {
                    task.SetResult(success);
                });
                context.EvaluatePolicy(LAPolicy.DeviceOwnerAuthentication, localizedReason, replyHandler);
            }

            // User hasn't configured any authentication: show dialog with options
            //else
            //{
            //    unAuthenticatedLabel.Text = "No device auth configured";
            //    var okCancelAlertController = UIAlertController.Create("No authentication", "This device does't have authentication configured.", UIAlertControllerStyle.Alert);
            //    okCancelAlertController.AddAction(UIAlertAction.Create("Use unsecured", UIAlertActionStyle.Default, alert => PerformSegue("AuthenticationSegue", this)));
            //    okCancelAlertController.AddAction(UIAlertAction.Create("Cancel", UIAlertActionStyle.Cancel, alert => Console.WriteLine("Cancel was clicked")));
            //    PresentViewController(okCancelAlertController, true, null);
            //}
            await task.Task;
            return task.Task.Result;
        }
    }
}
