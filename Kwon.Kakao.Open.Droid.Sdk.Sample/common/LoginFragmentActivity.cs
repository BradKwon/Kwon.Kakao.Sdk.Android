
using Android.App;
using Android.OS;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    /**
     * Container activity for LoginFragment.
     */
    [Activity(Label = "LoginFragmentActivity")]
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
