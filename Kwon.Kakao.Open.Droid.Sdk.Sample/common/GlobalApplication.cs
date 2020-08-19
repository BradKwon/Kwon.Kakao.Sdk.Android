using System;
using Android.App;
using Android.Graphics;
using Android.OS;
using Android.Runtime;
using Com.Kakao.Auth;
using Com.Kakao.Util.Helper.Log;
using Java.Lang;
using Volley;
using Volley.Toolbox;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common
{
    /**
     * 이미지 캐시를 앱 수준에서 관리하기 위한 애플리케이션 객체이다.
     * 로그인 기반 샘플앱에서 사용한다.
     *
     * @author MJ
     */
    [Application]
    public class GlobalApplication : Application
    {
        private static volatile GlobalApplication instance = null;
        private ImageLoader imageLoader;

        /**
         * singleton 애플리케이션 객체를 얻는다.
         *
         * @return singleton 애플리케이션 객체
         */
        public static GlobalApplication GetGlobalApplicationContext()
        {
            if (instance == null)
                throw new IllegalStateException("this application does not inherit com.kakao.GlobalApplication");
            return instance;
        }

        /**
         * 이미지 로더, 이미지 캐시, 요청 큐를 초기화한다.
         */
        public override void OnCreate()
        {
            base.OnCreate();
            instance = this;

            KakaoSDK.Init(new KakaoSDKAdapter());
            PushService.Init();

            RequestQueue requestQueue = Volley.Toolbox.Volley.NewRequestQueue(this);
            var imageCache = new MyImageCache();

            imageLoader = new ImageLoader(requestQueue, imageCache);

            CreateNotificationChannel();
        }

        /**
         * 이미지 로더를 반환한다.
         *
         * @return 이미지 로더
         */
        public ImageLoader GetImageLoader()
        {
            return imageLoader;
        }

        /**
         * 애플리케이션 종료시 singleton 어플리케이션 객체 초기화한다.
         */
        public override void OnTerminate()
        {
            base.OnTerminate();
            instance = null;
        }

        /**
         * For API level above or equalt o 26, Separate notification
         */
        private void CreateNotificationChannel()
        {
            if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.O)
            {
                NotificationManager nm = (NotificationManager)GetSystemService(NotificationService);
                if (nm == null)
                {
                    Logger.E("Failed to fetch NotificationManager from context.");
                    return;
                }
                string channelId = "kakao_push_channel";
                string channelName = "Kakao SDK Push";
                NotificationChannel channel = new NotificationChannel(channelId, channelName, NotificationImportance.Default);
                channel.EnableLights(true);
                channel.LightColor = Color.Red;
                channel.EnableVibration(true);
                nm.CreateNotificationChannel(channel);
                Logger.D("successfully created a notification channel.");
            }
        }

        private class MyImageCache : Java.Lang.Object, ImageLoader.IImageCache
        {
            AndroidX.Collection.LruCache imageCache = new AndroidX.Collection.LruCache(30);

            public Bitmap GetBitmap(string p0)
            {
                return imageCache.Get(p0) as Bitmap;
            }

            public void PutBitmap(string p0, Bitmap p1)
            {
                imageCache.Put(p0, p1);
            }
        }
    }
}
