using Android.App;
using Android.Content;
using AndroidX.Fragment.App;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    [Activity(Label = "BaseActivity")]
    public class BaseActivity : FragmentActivity
    {
        protected void ShowWaitingDialog()
        {
            WaitingDialog.ShowWaitingDialog(this);
        }

        protected void CancelWaitingDialog()
        {
            WaitingDialog.CancelWaitingDialog();
        }

        public void RedirectLoginActivity()
        {
            Intent intent = new Intent(this, typeof(RootLoginActivity));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        public void RedirectSignupActivity()
        {
            Intent intent = new Intent(this, typeof(SampleSignupActivity));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
            StartActivity(intent);
        }
    }
}
