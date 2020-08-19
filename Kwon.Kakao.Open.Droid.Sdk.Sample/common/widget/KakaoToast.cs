using System;
using Android.Content;
using Android.Views;
using Android.Widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget
{
    public class KakaoToast
    {
        public static Toast MakeToast(Context context, string body, ToastLength duration)
        {
            LayoutInflater inflater;
            inflater = (LayoutInflater)context.GetSystemService(Context.LayoutInflaterService);
            View v = inflater.Inflate(Resource.Layout.view_toast, null);
            TextView text = v.FindViewById<TextView>(Resource.Id.message);
            text.Text = body;

            Toast toast = new Toast(context);
            toast.SetGravity(GravityFlags.CenterVertical, 0, 0);
            toast.View = v;
            toast.Duration = duration;
            return toast;
        }
    }
}
