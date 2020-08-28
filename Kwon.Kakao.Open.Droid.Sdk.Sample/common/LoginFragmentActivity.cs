
using Android.App;
using Android.OS;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    /**
     * Container activity for LoginFragment.
     */
    [Activity(Label = "@string/title_activity_login_fragment",
        LaunchMode = Android.Content.PM.LaunchMode.SingleTop,
        ParentActivity = typeof(RootLoginActivity))]
    public class LoginFragmentActivity : BaseActivity
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_login_fragment);

            if (ActionBar != null)
            {
                ActionBar.SetDisplayHomeAsUpEnabled(true);
            }
        }
    }
}
