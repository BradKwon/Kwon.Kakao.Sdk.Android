using Android.App;
using Android.Content;
using Android.OS;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget
{
    public class WaitingDialog
    {
        private static Handler mainHandler = new Handler(Looper.MainLooper);
        private static object waitingDialogLock = new object();
        private static Dialog waitingDialog;

        private static Dialog GetWaitingDialog(Context context)
        {
            lock(waitingDialogLock) {
                if (waitingDialog != null)
                {
                    return waitingDialog;
                }

                waitingDialog = new Dialog(context, Resource.Style.CustomProgressDialog);
                return waitingDialog;
            }
        }
    }
}
