
using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common;
using Kwon.Kakao.Open.Droid.Sdk.Sample.kakaostory;
using Kwon.Kakao.Open.Droid.Sdk.Sample.kakaotalk;
using Kwon.Kakao.Open.Droid.Sdk.Sample.usermgmt;
using static Android.Views.View;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample
{
    [Activity(LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class KakaoServiceListActivity : BaseActivity, IOnClickListener
    {
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_service_list);

            FindViewById(Resource.Id.kakao_story).SetOnClickListener(this);
            FindViewById(Resource.Id.kakao_talk).SetOnClickListener(this);
            FindViewById(Resource.Id.kakao_push).SetOnClickListener(this);
            FindViewById(Resource.Id.kakao_usermgmt).SetOnClickListener(this);
            FindViewById(Resource.Id.title_back).Visibility = ViewStates.Gone;
        }

        public void OnClick(View v)
        {
            switch (v.Id)
            {
                //case Resource.Id.kakao_story:
                //    StartActivity(new Intent(this, typeof(KakaoStoryMainActivity)));
                //    break;
                //case Resource.Id.kakao_talk:
                //    StartActivity(new Intent(this, typeof(KakaoTalkMainActivity)));
                //    break;
                //case Resource.Id.kakao_push:
                //    StartActivity(new Intent(this, typeof(PushMainActivity)));
                //    break;
                case Resource.Id.kakao_usermgmt:
                    StartActivity(new Intent(this, typeof(UsermgmtMainActivity)));
                    break;
                default:
                    break;
            }
        }
    }
}
