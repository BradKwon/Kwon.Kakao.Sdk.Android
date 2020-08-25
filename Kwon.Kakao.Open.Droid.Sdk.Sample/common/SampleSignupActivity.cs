
using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Widget;
using Com.Kakao.Auth;
using Com.Kakao.Network;
using Com.Kakao.Usermgmt;
using Com.Kakao.Usermgmt.Callback;
using Com.Kakao.Usermgmt.Response;
using Com.Kakao.Util;
using Com.Kakao.Util.Helper.Log;
using Java.Lang;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget;
using Kwon.Kakao.Open.Droid.Sdk.Sample.usermgmt;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    /**
     * 유효한 세션이 있다는 검증 후
     * me를 호출하여 가입 여부에 따라 가입 페이지를 그리던지 Main 페이지로 이동 시킨다.
     */
    [Activity(ConfigurationChanges = Android.Content.PM.ConfigChanges.ScreenSize | Android.Content.PM.ConfigChanges.Orientation,
        LaunchMode = Android.Content.PM.LaunchMode.SingleTop,
        Theme = "@android:style/Theme.Translucent.NoTitleBar")]
    public class SampleSignupActivity : BaseActivity
    {
        /**
         * Main으로 넘길지 가입 페이지를 그릴지 판단하기 위해 me를 호출한다.
         * @param savedInstanceState 기존 session 정보가 저장된 객체
         */
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            RequestMe();
        }

        protected void ShowSignup()
        {
            SetContentView(Resource.Layout.layout_usermgmt_signup);
            ExtraUserPropertyLayout extraUserPropertyLayout = FindViewById<ExtraUserPropertyLayout>(Resource.Id.extra_user_property);
            Button signupButton = FindViewById<Button>(Resource.Id.buttonSignup);
            signupButton.Click += (s, e) => RequestSignUp(extraUserPropertyLayout.GetProperties());
        }

        private void RequestSignUp(IDictionary<string, string> properties)
        {
            UserManagement.Instance.RequestSignup(new RequestSignupResponseCallback(() => RequestMe(), (errorResult) =>
            {
                string message = "UsermgmtResponseCallback : failure : " + errorResult;
                Logger.W(message);
                KakaoToast.MakeToast(ApplicationContext, message, ToastLength.Long).Show();
                Finish();
            }), properties);
        }

        /**
         * 사용자의 상태를 알아 보기 위해 me API 호출을 한다.
         */
        protected void RequestMe()
        {
            UserManagement.Instance.Me(new MyMeV2ResponseCallback(
                (errorResult) =>
                {
                    string message = "failed to get user info. msg=" + errorResult;
                    Logger.D(message);

                    int result = errorResult.ErrorCode;
                    if (result == Com.Kakao.Usermgmt.ApiErrorCode.ClientErrorCode)
                    {
                        KakaoToast.MakeToast(ApplicationContext, GetString(Resource.String.error_message_for_service_unavailable), ToastLength.Short).Show();
                        Finish();
                    }
                    else
                    {
                        RedirectLoginActivity();
                    }
                },
                (errorResult) =>
                {
                    Logger.E("onSessionClosed");
                    RedirectLoginActivity();
                },
                (result) =>
                {
                    if (result.HasSignedUp() == OptionalBoolean.False)
                    {
                        ShowSignup();
                    }
                    else
                    {
                        RedirectMainActivity();
                    }
                }));
        }

        private void RedirectMainActivity()
        {
            Intent intent = new Intent(this, typeof(KakaoServiceListActivity));
            intent.SetFlags(ActivityFlags.NewTask | ActivityFlags.ClearTask | ActivityFlags.ClearTop);
            StartActivity(intent);
        }

        private class MyMeV2ResponseCallback : MeV2ResponseCallback
        {
            Action<ErrorResult> failureAction;
            Action<ErrorResult> sessionClosedAction;
            Action<MeV2Response> successAction;

            public MyMeV2ResponseCallback(Action<ErrorResult> failureAction, Action<ErrorResult> sessionClosedAction, Action<MeV2Response> successAction)
            {
                this.failureAction = failureAction;
                this.sessionClosedAction = sessionClosedAction;
                this.successAction = successAction;
            }

            public override void OnFailure(ErrorResult errorResult)
            {
                failureAction.Invoke(errorResult);
            }

            public override void OnSessionClosed(ErrorResult p0)
            {
                sessionClosedAction.Invoke(p0);
            }

            public override void OnSuccess(Java.Lang.Object p0)
            {
                successAction.Invoke(p0 as MeV2Response);
            }
        }

        private class RequestSignupResponseCallback : ApiResponseCallback
        {
            Action successAction;
            Action<ErrorResult> failureAction;

            public RequestSignupResponseCallback(Action successAction, Action<ErrorResult> failureAction)
            {
                this.successAction = successAction;
                this.failureAction = failureAction;
            }

            public override void OnSessionClosed(ErrorResult p0)
            {
            }

            public override void OnSuccess(Java.Lang.Object p0)
            {
                successAction.Invoke();
            }

            public override void OnFailure(ErrorResult errorResult)
            {
                failureAction.Invoke(errorResult);
            }
        }
    }
}
