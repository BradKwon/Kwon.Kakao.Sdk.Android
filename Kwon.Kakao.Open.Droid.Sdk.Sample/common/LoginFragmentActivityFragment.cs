
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Fragment.App;
using Com.Kakao.Auth;
using Com.Kakao.Usermgmt;
using Com.Kakao.Util.Exception;
using Com.Kakao.Util.Helper.Log;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    /**
     * Sample login fragmeent. Note that LoginButton's setFragment() should be called in order for
     * login button to work properly.
     */
    public class LoginFragmentActivityFragment : Fragment
    {
        private SessionCallback callback;

        public LoginFragmentActivityFragment() { }

        public override void OnActivityResult(int requestCode, int resultCode, Intent data)
        {
            if (Session.CurrentSession.HandleActivityResult(requestCode, resultCode, data))
            {
                return;
            }

            base.OnActivityResult(requestCode, resultCode, data);
        }

        public override View OnCreateView(LayoutInflater inflater, ViewGroup container, Bundle savedInstanceState)
        {
            View view = inflater.Inflate(Resource.Layout.fragment_login_fragment, container, false);

            LoginButton loginButton = view.FindViewById<LoginButton>(Resource.Id.login_button_fragment);
            loginButton.SetSuportFragment(this); // set fragment for LoginButton

            callback = new SessionCallback(this);
            Session.CurrentSession.AddCallback(callback);
            return view;
        }

        public override void OnDestroyView()
        {
            base.OnDestroyView();
            Session.CurrentSession.RemoveCallback(callback);
        }

        private class SessionCallback : Java.Lang.Object, ISessionCallback
        {
            WeakReference<LoginFragmentActivityFragment> fragment;

            public SessionCallback(LoginFragmentActivityFragment fragment)
            {
                this.fragment = new WeakReference<LoginFragmentActivityFragment>(fragment);
            }

            public void OnSessionOpened()
            {
                if (fragment.TryGetTarget(out var frag))
                {
                    Intent intent = new Intent(frag.Context, typeof(SampleSignupActivity));
                    frag.StartActivity(intent);
                }
            }

            public void OnSessionOpenFailed(KakaoException p0)
            {
                var exception = p0;
                if (exception != null)
                {
                    Logger.E(exception);
                }
            }
        }
    }
}
