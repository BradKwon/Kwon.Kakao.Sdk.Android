
using System;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Widget;
using Com.Kakao.Auth;
using Com.Kakao.Util.Exception;
using Com.Kakao.Util.Helper.Log;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    /**
     * Sample Activity that dose not use fragment for login.
     */
    [Activity(Label = "SampleLoginActivity")]
    public class SampleLoginActivity : BaseActivity
    {
        private ISessionCallback callback;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.activity_sample_login);

            if (ActionBar != null)
            {
                ActionBar.SetDisplayHomeAsUpEnabled(true);
            }

            callback = new MySessionCallback(this);
            Session.CurrentSession.AddCallback(callback);
        }

        protected override void OnActivityResult(int requestCode, [GeneratedEnum] Result resultCode, Intent data)
        {
            if (Session.CurrentSession.HandleActivityResult(requestCode, (int)resultCode, data))
            {
                return;
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Session.CurrentSession.RemoveCallback(callback);
        }

        private class MySessionCallback : Java.Lang.Object, ISessionCallback
        {
            WeakReference<BaseActivity> baseActivity;

            public MySessionCallback(BaseActivity baseActivity)
            {
                this.baseActivity = new WeakReference<BaseActivity>(baseActivity);
            }

            public void OnSessionOpened()
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectSignupActivity();
                }
            }

            public void OnSessionOpenFailed(KakaoException p0)
            {
                var exception = p0;
                if (exception != null)
                {
                    Logger.E(exception);
                    if (baseActivity.TryGetTarget(out var activity))
                    {
                        Toast.MakeText(activity, exception.ToString(), ToastLength.Short).Show();
                    }
                }
            }
        }
    }
}
