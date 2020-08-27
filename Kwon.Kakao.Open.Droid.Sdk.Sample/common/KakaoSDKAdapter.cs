using System;
using Android.Content;
using Android.OS;
using Android.Telephony;
using Android.Widget;
using Com.Kakao.Auth;
using Com.Kakao.Network;
using Com.Kakao.Util.Helper;
using Com.Kakao.Util.Helper.Log;
using Java.IO;
using Java.Lang;
using Java.Util;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    public class KakaoSDKAdapter : KakaoAdapter
    {
        protected static string PROPERTY_DEVICE_ID = "device_id";

        /**
         * Session Config에 대해서는 default값들이 존재한다.
         * 필요한 상황에서만 override해서 사용하면 됨.
         * @return Session의 설정값.
         */
        public override ISessionConfig SessionConfig => new MySessionConfig();

        public override IApplicationConfig ApplicationConfig => GlobalApplication.GetGlobalApplicationContext() as IApplicationConfig;

        public override IPushConfig PushConfig => base.PushConfig;

        private class MyPushConfig : Java.Lang.Object, IPushConfig
        {
            private IApplicationConfig applicationConfig;

            public MyPushConfig(IApplicationConfig applicationConfig)
            {
                this.applicationConfig = applicationConfig;
            }

            /**
             * [주의!] 아래 예제는 샘플앱에서 사용되는 것으로 기기정보 일부가 포함될 수 있습니다. 실제 릴리즈 되는 앱에서 사용하기 위해서는 사용자로부터 개인정보 취급에 대한 동의를 받으셔야 합니다.
             *
             * 한 사용자에게 여러 기기를 허용하기 위해 기기별 id가 필요하다.
             * ANDROID_ID가 기기마다 다른 값을 준다고 보장할 수 없어, 보완된 로직이 포함되어 있다.
             * @return 기기의 unique id
             */
            public string DeviceUUID
            {
                get
                {
                    string deviceUUID;
                    SharedPreferencesCache cache = Session.CurrentSession.AppCache;
                    string id = cache.GetString(PROPERTY_DEVICE_ID);

                    if (id != null)
                    {
                        deviceUUID = id;
                        return deviceUUID;
                    }
                    else
                    {
                        UUID uuid = null;
                        Context context = applicationConfig.ApplicationContext;
                        string androidId = Android.Provider.Settings.Secure.GetString(context.ContentResolver, Android.Provider.Settings.Secure.AndroidId);
                        try
                        {
                            if (!"9774d56d682e549c".Equals(androidId))
                            {
                                uuid = UUID.NameUUIDFromBytes(System.Text.Encoding.UTF8.GetBytes(androidId));
                            }
                            else
                            {
                                string deviceId = ((TelephonyManager)context.GetSystemService(Context.TelephonyService)).DeviceId;
                                uuid = deviceId != null ? UUID.NameUUIDFromBytes(System.Text.Encoding.UTF8.GetBytes(deviceId)) : UUID.RandomUUID();
                            }
                        }
                        catch (UnsupportedEncodingException e)
                        {
                            throw new RuntimeException(e);
                        }

                        Bundle bundle = new Bundle();
                        bundle.PutString(PROPERTY_DEVICE_ID, uuid.ToString());
                        cache.Save(bundle);

                        deviceUUID = uuid.ToString();
                        return deviceUUID;
                    }
                }
            }

            public ApiResponseCallback<Java.Lang.Integer> TokenRegisterCallback => new MyApiResponseCallback(applicationConfig);

            NativeApiResponseCallback IPushConfig.TokenRegisterCallback => throw new NotImplementedException();
        }

        private class MyApiResponseCallback : ApiResponseCallback<Java.Lang.Integer>
        {
            private IApplicationConfig applicationConfig;

            public MyApiResponseCallback(IApplicationConfig applicationConfig)
            {
                this.applicationConfig = applicationConfig;
            }

            public override void OnFailure(ErrorResult errorResult)
            {
                KakaoToast.MakeToast(applicationConfig.ApplicationContext, errorResult.ToString(), ToastLength.Short).Show();
            }

            public override void OnSessionClosed(ErrorResult p0)
            {
                Logger.E(p0.ErrorMessage);
                Logger.W("login again...");
            }

            public override void OnNotSignedUp()
            {
                Logger.E("You should signup first");
            }

            public override void OnSuccess(Integer result)
            {
                KakaoToast.MakeToast(applicationConfig.ApplicationContext, "succeeded to register fcm token...", ToastLength.Short).Show();
            }
        }

        private class MySessionConfig : Java.Lang.Object, ISessionConfig
        {
            public ApprovalType ApprovalType => ApprovalType.Individual;

            public bool IsSaveFormData => true;

            public bool IsSecureMode => true;

            public bool IsUsingWebviewTimer => true;

            public AuthType[] GetAuthTypes()
            {
                return new AuthType[] { AuthType.KakaoLoginAll };
            }
        }
    }
}
