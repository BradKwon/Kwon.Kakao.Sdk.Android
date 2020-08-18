using System;
using Android.App;
using Android.Content;
using Android.OS;
using Com.Kakao.Util.Helper.Log;
using Java.Lang;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget
{
    public class WaitingDialog
    {
        private static Handler mainHandler = new Handler(Looper.MainLooper);
        private static object waitingDialogLock = new object();
        private static Dialog waitingDialog;

        private static Dialog GetWaitingDialog(Context context)
        {
            lock (waitingDialogLock)
            {
                if (waitingDialog != null)
                {
                    return waitingDialog;
                }

                waitingDialog = new Dialog(context, Resource.Style.CustomProgressDialog);
                return waitingDialog;
            }
        }

        public static void ShowWaitingDialog(Context context)
        {
            ShowWaitingDialog(context, false);
        }

        public static void ShowWaitingDialog(Context context, bool cancelable)
        {
            ShowWaitingDialog(context, cancelable, null);
        }

        public static void ShowWaitingDialog(Context context, bool cancelable, IDialogInterfaceOnCancelListener listener)
        {
            CancelWaitingDialog();
            mainHandler.Post(new ShowWariningDialogRunnable(() =>
            {
                waitingDialog = GetWaitingDialog(context);
                // here we set layout of progress dialog
                waitingDialog.SetContentView(Resource.Layout.layout_waiting_dialog);
                waitingDialog.SetCancelable(cancelable);
                if (listener != null)
                {
                    waitingDialog.SetOnCancelListener(listener);
                }
                waitingDialog.Show();
            }));
        }

        public static void CancelWaitingDialog()
        {
            mainHandler.Post(new CancelWaitingDialogRunnable(() =>
            {
                try
                {
                    if (waitingDialog != null)
                    {
                        lock (waitingDialogLock)
                        {
                            waitingDialog.Cancel();
                            waitingDialog = null;
                        }
                    }
                }
                catch (Java.Lang.Exception e)
                {
                    Logger.D(e);
                }
            }));
        }

        private class CancelWaitingDialogRunnable : Java.Lang.Object, IRunnable
        {
            Action runAction;

            public CancelWaitingDialogRunnable(Action action)
            {
                runAction = action;
            }

            public void Run()
            {
                runAction.Invoke();
            }
        }

        private class ShowWariningDialogRunnable : Java.Lang.Object, IRunnable
        {
            Action runAction;

            public ShowWariningDialogRunnable(Action action)
            {
                runAction = action;
            }

            public void Run()
            {
                runAction.Invoke();
            }
        }
    }
}
