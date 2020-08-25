
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    /**
     * 샘플에서 사용하게 될 로그인 페이지
     * 세션을 오픈한 후 action을 override해서 사용한다.
     *
     * @author MJ
     */
    [Activity(ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation,
        LaunchMode = Android.Content.PM.LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Light.NoTitleBar")]
    public class RootLoginActivity : BaseActivity, View.IOnClickListener
    {
        public void OnClick(View v)
        {
            Intent intent;
            switch (v.Id)
            {
                case Resource.Id.button_login_with_activity:
                    intent = new Intent(this, typeof(SampleLoginActivity));
                    StartActivity(intent);
                    break;
                case Resource.Id.button_login_with_fragment:
                    intent = new Intent(this, typeof(LoginFragmentActivity));
                    StartActivity(intent);
                    break;
                default:
                    break;
            }
        }

        /**
         * 로그인 버튼을 클릭 했을시 access token을 요청하도록 설정한다.
         *
         * @param savedInstanceState 기존 session 정보가 저장된 객체
         */
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_common_kakao_login);
            Button activityButton = FindViewById<Button>(Resource.Id.button_login_with_activity);
            Button fragmentButton = FindViewById<Button>(Resource.Id.button_login_with_fragment);
            activityButton.SetOnClickListener(this);
            fragmentButton.SetOnClickListener(this);
        }
    }
}
