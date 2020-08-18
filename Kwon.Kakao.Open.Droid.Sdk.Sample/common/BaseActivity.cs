using Android.App;
using Android.Content;
using AndroidX.Fragment.App;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    [Activity(Label = "BaseActivity")]
    public class BaseActivity : FragmentActivity
    {
        protected void ShowWaitingDialog()
        {
            //WaitingDialog.ShowWaitingDialog(this);
        }

        protected void CancelWaitingDialog()
        {
            //WaitingDialog.CancelWaitingDialog();
        }

        //protected void RedirectLoginActivity()
        //{
        //    Intent intent = new Intent(this, typeof(RootLoginActivity));
        //    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
        //    StartActivity(intent);
        //}

        //protected void redirectSignupActivity()
        //{
        //    Intent intent = new Intent(this, typeof(SampleSignupActivity));
        //    intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
        //    StartActivity(intent);
        //}
    }
}
