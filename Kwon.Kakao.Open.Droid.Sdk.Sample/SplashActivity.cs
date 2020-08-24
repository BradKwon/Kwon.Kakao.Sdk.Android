
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Kakao.Auth;
using Com.Kakao.Util.Exception;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample
{
    [Activity(Label = "@string/app_name", MainLauncher = true,
        LaunchMode = Android.Content.PM.LaunchMode.SingleTop)]
    public class SplashActivity : BaseActivity
    {
        private ISessionCallback callback;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            SetContentView(Resource.Layout.layout_splash);
            callback = new MySessionCallback(this);

            Session.CurrentSession.AddCallback(callback);
            FindViewById(Resource.Id.splash).PostDelayed(() => {
                if (!Session.CurrentSession.CheckAndImplicitOpen())
                {
                    RedirectToLoginActivity();
                }
            }, 500);
        }

        protected override void OnDestroy()
        {
            base.OnDestroy();
            Session.CurrentSession.RemoveCallback(callback);
        }

        private void GoToMainActivity()
        {
            Intent intent = new Intent(this, typeof(KakaoServiceListActivity));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        private void RedirectToLoginActivity()
        {
            Intent intent = new Intent(this, typeof(RootLoginActivity));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        private class MySessionCallback : Java.Lang.Object, ISessionCallback
        {
            WeakReference<SplashActivity> splashActivity;

            public MySessionCallback(SplashActivity splashActivity)
            {
                this.splashActivity = new WeakReference<SplashActivity>(splashActivity);
            }

            public void OnSessionOpened()
            {
                if (splashActivity.TryGetTarget(out var activity))
                {
                    activity.GoToMainActivity();
                }
            }

            public void OnSessionOpenFailed(KakaoException p0)
            {
                if (splashActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectToLoginActivity();
                }
            }
        }
    }
}
