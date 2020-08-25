
using System;
using System.Collections.Generic;
using System.Linq;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Views;
using Android.Widget;
using Com.Kakao.Auth;
using Com.Kakao.Auth.Authorization.Accesstoken;
using Com.Kakao.Auth.Network.Response;
using Com.Kakao.Network;
using Com.Kakao.Usermgmt;
using Com.Kakao.Usermgmt.Callback;
using Com.Kakao.Usermgmt.Response;
using Com.Kakao.Util;
using Com.Kakao.Util.Helper.Log;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.usermgmt
{
    /**
     * 가입된 사용자가 보게되는 메인 페이지로 사용자 정보 불러오기/update, 로그아웃, 탈퇴 기능을 테스트 한다.
     */
    [Activity(ConfigurationChanges = Android.Content.PM.ConfigChanges.KeyboardHidden | Android.Content.PM.ConfigChanges.Orientation | Android.Content.PM.ConfigChanges.ScreenSize,
        LaunchMode = Android.Content.PM.LaunchMode.SingleTop,
        WindowSoftInputMode = SoftInput.AdjustResize | SoftInput.StateHidden)]
    public class UsermgmtMainActivity : BaseActivity
    {
        private MeV2Response response;
        private ProfileLayout profileLayout;
        private ExtraUserPropertyLayout extraUserPropertyLayout;

        /**
         * 로그인 또는 가입창에서 넘긴 유저 정보가 있다면 저장한다.
         *
         * @param savedInstanceState 기존 session 정보가 저장된 객체
         */
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            InitializeView();
            profileLayout.RequestMe();
        }

        /**
         * 사용자의 정보를 변경 저장하는 API를 호출한다.
         */
        private void OnClickUpdateProfile()
        {
            var properties = extraUserPropertyLayout.GetProperties();
            UserManagement.Instance.RequestUpdateProfile(new UsermgmtResponseCallback(this), properties);
        }

        private void OnClickAccessTokenInfo()
        {
            AuthService.Instance.RequestAccessTokenInfo(new AccessTokenApiResponseCallback(this));
        }

        private void OnClickLogout()
        {
            UserManagement.Instance.RequestLogout(new MyLogoutResponseCallback(this));
        }

        private void OnClickUnlink()
        {
            string appendMessage = GetString(Resource.String.com_kakao_confirm_unlink);
            new AlertDialog.Builder(this)
                .SetMessage(appendMessage)
                .SetPositiveButton(GetString(Resource.String.com_kakao_ok_button), UnlinkPositiveButtonHandler)
                .SetNegativeButton(GetString(Resource.String.com_kakao_cancel_button), UnlinkNegativeButtonHandler)
                .Show();
        }

        private void UnlinkNegativeButtonHandler(object sender, DialogClickEventArgs e)
        {
            var dialog = sender as AlertDialog;
            dialog.Dismiss();
        }

        private void UnlinkPositiveButtonHandler(object sender, DialogClickEventArgs e)
        {
            UserManagement.Instance.RequestUnlink(new MyUnLinkResponseCallback(this));
        }

        private void InitializeView()
        {
            SetContentView(Resource.Layout.layout_usermgmt_main);
            ((TextView)FindViewById(Resource.Id.text_title)).Text = GetString(Resource.String.text_usermgmt);
            FindViewById(Resource.Id.title_back).Click += (s, e) => Finish();

            InitializeButtons();
            InitializeProfileView();
        }

        private void InitializeButtons()
        {
            Button buttonMe = FindViewById<Button>(Resource.Id.buttonMe);
            buttonMe.Click += (s, e) => UpdateScopes();

            Button buttonUpdateProfile = FindViewById<Button>(Resource.Id.buttonUpdateProfile);
            buttonUpdateProfile.Click += (s, e) => OnClickUpdateProfile();

            Button logoutButton = FindViewById<Button>(Resource.Id.logout_button);
            logoutButton.Click += (s, e) => OnClickLogout();

            Button unlinkButton = FindViewById<Button>(Resource.Id.unlink_button);
            unlinkButton.Click += (s, e) => OnClickUnlink();

            Button tokenInfoButton = FindViewById<Button>(Resource.Id.token_info_button);
            tokenInfoButton.Click += (s, e) => OnClickAccessTokenInfo();
        }

        private void InitializeProfileView()
        {
            profileLayout = FindViewById<ProfileLayout>(Resource.Id.com_kakao_user_profile);
            profileLayout.SetMeV2ResponseCallback(new MyMeV2ResponseCallback(this, (result) =>
            {
                response = result;
                KakaoToast.MakeToast(ApplicationContext, "succeeded to get user profile", ToastLength.Short).Show();
                UpdateLayouts(result);
            }));

            extraUserPropertyLayout = FindViewById<ExtraUserPropertyLayout>(Resource.Id.extra_user_property);
        }

        private void UpdateScopes()
        {
            List<string> scopes = new List<string>();
            if (response.KakaoAccount.ProfileNeedsAgreement() == OptionalBoolean.True)
            {
                scopes.Add("profile");
            }
            if (response.KakaoAccount.EmailNeedsAgreement() == OptionalBoolean.True)
            {
                scopes.Add("account_email");
            }
            if (response.KakaoAccount.PhoneNumberNeedsAgreement() == OptionalBoolean.True)
            {
                scopes.Add("phone_number");
            }
            if (response.KakaoAccount.AgeRangeNeedsAgreement() == OptionalBoolean.True)
            {
                scopes.Add("age_range");
            }
            if (response.KakaoAccount.BirthdayNeedsAgreement() == OptionalBoolean.True)
            {
                scopes.Add("birthday");
            }
            if (response.KakaoAccount.GenderNeedsAgreement() == OptionalBoolean.True)
            {
                scopes.Add("gender");
            }

            if (!scopes.Any())
            {
                KakaoToast.MakeToast(ApplicationContext, "User has all the required scopes", ToastLength.Long).Show();
                return;
            }

            Session.CurrentSession.UpdateScopes(this, scopes,
                new MyAccessTokenCallback((accessToken) =>
                {
                    profileLayout.RequestMe();
                },
                (errorResult) =>
                {
                    KakaoToast.MakeToast(ApplicationContext, "Failed to update scopes", ToastLength.Long).Show();
                }));
        }

        private void UpdateLayouts(MeV2Response result)
        {
            if (result != null)
            {
            }
            profileLayout.SetUserInfo(result);

            if (result.Properties != null)
            {
                extraUserPropertyLayout.ShowProperties(result.Properties);
            }
        }

        private class MyAccessTokenCallback : AccessTokenCallback
        {
            Action<IAccessToken> receivedAction;
            Action<ErrorResult> failureAction;

            public MyAccessTokenCallback(Action<IAccessToken> receivedAction, Action<ErrorResult> failureAction)
            {
                this.receivedAction = receivedAction;
                this.failureAction = failureAction;
            }

            public override void OnAccessTokenFailure(ErrorResult p0)
            {
                failureAction.Invoke(p0);
            }

            public override void OnAccessTokenReceived(IAccessToken p0)
            {
                receivedAction.Invoke(p0);
            }

            public override void OnSuccess(Java.Lang.Object p0)
            {
            }
        }

        private class MyMeV2ResponseCallback : MeV2ResponseCallback
        {
            WeakReference<BaseActivity> baseActivity;
            Action<MeV2Response> action;

            public MyMeV2ResponseCallback(BaseActivity baseActivity, Action<MeV2Response> action)
            {
                this.baseActivity = new WeakReference<BaseActivity>(baseActivity);
                this.action = action;
            }

            public override void OnFailure(ErrorResult errorResult)
            {
                string message = "failed to get user info. msg=" + errorResult;
                Logger.E(message);

                if (baseActivity.TryGetTarget(out var activity))
                {
                    KakaoToast.MakeToast(activity, message, ToastLength.Long).Show();
                }
            }

            public override void OnSessionClosed(ErrorResult p0)
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectSignupActivity();
                }
            }

            public override void OnSuccess(Java.Lang.Object p0)
            {
                action.Invoke(p0 as MeV2Response);
            }
        }

        private class MyUnLinkResponseCallback : UnLinkResponseCallback
        {
            WeakReference<BaseActivity> baseActivity;

            public MyUnLinkResponseCallback(BaseActivity baseActivity)
            {
                this.baseActivity = new WeakReference<BaseActivity>(baseActivity);
            }

            public override void OnFailure(ErrorResult errorResult)
            {
                Logger.E(errorResult.ToString());
            }

            public override void OnSessionClosed(ErrorResult p0)
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectSignupActivity();
                }
            }

            public override void OnNotSignedUp()
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectSignupActivity();
                }
            }

            public override void OnSuccess(Java.Lang.Object p0)
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectSignupActivity();
                }
            }
        }

        private class MyLogoutResponseCallback : LogoutResponseCallback
        {
            WeakReference<BaseActivity> baseActivity;

            public MyLogoutResponseCallback(BaseActivity baseActivity)
            {
                this.baseActivity = new WeakReference<BaseActivity>(baseActivity);
            }

            public override void OnCompleteLogout()
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectSignupActivity();
                }
            }

            public override void OnSuccess(Java.Lang.Object p0)
            {
            }
        }

        private class AccessTokenApiResponseCallback : ApiResponseCallback
        {
            WeakReference<BaseActivity> baseActivity;

            public AccessTokenApiResponseCallback(BaseActivity baseActivity)
            {
                this.baseActivity = new WeakReference<BaseActivity>(baseActivity);
            }

            public override void OnNotSignedUp()
            {
                // not happened
            }

            public override void OnFailure(ErrorResult errorResult)
            {
                string message = "failed to get access token info. msg=" + errorResult;
                Logger.E(message);

                if (baseActivity.TryGetTarget(out var activity))
                {
                    KakaoToast.MakeToast(activity, message, ToastLength.Long).Show();
                }
            }

            public override void OnSessionClosed(ErrorResult p0)
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectSignupActivity();
                }
            }

            public override void OnSuccess(Java.Lang.Object p0)
            {
                var accessTokenInfoResponse = p0 as AccessTokenInfoResponse;
                long userId = accessTokenInfoResponse.UserId;
                Logger.D("this access token is for userId=" + userId);

                long expiresIn = accessTokenInfoResponse.ExpiresIn;
                Logger.D("this access token expires after " + expiresIn + " seconds.");

                if (baseActivity.TryGetTarget(out var activity))
                {
                    KakaoToast.MakeToast(activity, "this access token for user(id=" + userId + ") expires after " + expiresIn + " seconds.", ToastLength.Long).Show();
                }

                // Deprecated
                long expiresInMilis = accessTokenInfoResponse.ExpiresInMillis;
                Logger.D("this access token expires after " + expiresInMilis + " milliseconds. (Deprecated)");
            }
        }

        private class UsermgmtResponseCallback : ApiResponseCallback
        {
            WeakReference<BaseActivity> baseActivity;

            public UsermgmtResponseCallback(BaseActivity baseActivity)
            {
                this.baseActivity = new WeakReference<BaseActivity>(baseActivity);
            }

            public override void OnNotSignedUp()
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    activity.RedirectSignupActivity();
                }
            }

            public override void OnSessionClosed(ErrorResult p0)
            {
                string message = "failed to get user info. msg=" + p0;
                Logger.E(message);

                if (baseActivity.TryGetTarget(out var activity))
                {
                    KakaoToast.MakeToast(activity, message, ToastLength.Long).Show();
                }
            }

            public override void OnSuccess(Java.Lang.Object p0)
            {
                if (baseActivity.TryGetTarget(out var activity))
                {
                    KakaoToast.MakeToast(activity, "succeeded to update user profile", ToastLength.Short).Show();
                    Logger.D("succeeded to update user profile");
                }
            }
        }
    }
}
