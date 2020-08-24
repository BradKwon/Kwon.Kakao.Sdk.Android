using System;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Text.Method;
using Android.Views;
using Android.Widget;
using Java.Lang;
using static Android.Views.View;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget
{
    /**
     * @author leo.shin
     * Created by leoshin on 15. 6. 19..
     */
    public class DialogBuilder
    {
        private Context context = null;
        public string title = null;
        public string message = null;
        public string positiveBtnText = null;
        public string negativeBtnText = null;
        public View contentView = null;
        public int titleBgResId = 0;
        public int titleTextColor = 0;
        public bool showTitleDivider = true;
        public IDialogInterfaceOnClickListener positiveListener = null;
        public IDialogInterfaceOnClickListener negativeListner = null;

        public DialogBuilder(Context context)
        {
            this.context = context;
        }

        public DialogBuilder SetTitle(string title)
        {
            this.title = title;
            return this;
        }

        public DialogBuilder SetTitle(int titleResId)
        {
            this.title = context.GetString(titleResId);
            return this;
        }

        public DialogBuilder SetMessage(string message)
        {
            this.message = message;
            return this;
        }

        public DialogBuilder SetMessage(int messageResId)
        {
            this.message = context.GetString(messageResId);
            return this;
        }

        public DialogBuilder SetPositiveButton(int positiveResId, IDialogInterfaceOnClickListener positiveListener)
        {
            this.positiveBtnText = context.GetString(positiveResId);
            this.positiveListener = positiveListener;
            return this;
        }

        public DialogBuilder SetNegativeButton(int negativeResId, IDialogInterfaceOnClickListener negativeListner)
        {
            this.negativeBtnText = context.GetString(negativeResId);
            this.negativeListner = negativeListner;
            return this;
        }

        public DialogBuilder SetView(View view)
        {
            this.contentView = view;
            return this;
        }

        public DialogBuilder SetTitleBgResId(int titleBgResId)
        {
            this.titleBgResId = titleBgResId;
            return this;
        }

        public DialogBuilder SetShowTitleDivider(bool showTitleDivider)
        {
            this.showTitleDivider = showTitleDivider;
            return this;
        }

        public DialogBuilder SetTitleTextColor(int titleTextColor)
        {
            this.titleTextColor = titleTextColor;
            return this;
        }

        public Dialog Create()
        {
            return new CustomDialog(context, this);
        }
    }

    public class CustomDialog : Dialog
    {
        private DialogBuilder builder;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            WindowManagerLayoutParams lpWindow = new WindowManagerLayoutParams();
            lpWindow.Flags = WindowManagerFlags.DimBehind;
            lpWindow.DimAmount = 0.8f;
            Window.Attributes = lpWindow;

            SetContentView(Resource.Layout.view_popup);
            InitView();
        }

        public CustomDialog(Context context, DialogBuilder builder)
            : base(context, Android.Resource.Style.ThemeTranslucentNoTitleBar)
        {
            // Dialog 배경을 투명 처리 해준다.
            this.builder = builder;
        }

        [Deprecated]
        private void InitView()
        {
            string title = builder.title;
            string message = builder.message;
            string negativeBtnText = builder.negativeBtnText;
            string positiveBtnText = builder.positiveBtnText;
            IDialogInterfaceOnClickListener positiveListener = builder.positiveListener;
            IDialogInterfaceOnClickListener negativeListner = builder.negativeListner;
            View contentView = builder.contentView;
            bool showTitleDivider = builder.showTitleDivider;
            int titleBgResId = builder.titleBgResId;
            int titleTextColor = builder.titleTextColor;

            TextView titleView = FindViewById<TextView>(Resource.Id.title);
            if (title != null && title.Length > 0)
            {
                titleView.Text = title;
            }
            else
            {
                FindViewById(Resource.Id.popup_header).Visibility = ViewStates.Gone;
            }

            if (titleBgResId > 0)
            {
                titleView.SetBackgroundResource(titleBgResId);
            }

            if (titleTextColor > 0)
            {
                titleView.SetTextColor(titleView.Context.Resources.GetColor(titleTextColor));
            }

            ImageView titleDivider = FindViewById<ImageView>(Resource.Id.divide);
            if (showTitleDivider)
            {
                titleDivider.Visibility = ViewStates.Visible;
            }
            else
            {
                titleDivider.Visibility = ViewStates.Gone;
            }

            TextView messageView = FindViewById<TextView>(Resource.Id.content);
            if (message != null && message.Length > 0)
            {
                messageView.Text = message;
                messageView.MovementMethod = new ScrollingMovementMethod();
            }
            else
            {
                messageView.Visibility = ViewStates.Gone;
            }

            if (contentView != null)
            {
                FrameLayout container = FindViewById<FrameLayout>(Resource.Id.content_group);
                container.Visibility = ViewStates.Visible;
                container.AddView(contentView);
            }

            Button positiveBtn = FindViewById<Button>(Resource.Id.bt_right);
            if (positiveBtnText != null && positiveBtnText.Length > 0)
            {
                positiveBtn.Text = positiveBtnText;
            }
            else
            {
                positiveBtn.Visibility = ViewStates.Gone;
            }

            Button negativeBtn = FindViewById<Button>(Resource.Id.bt_left);
            if (negativeBtnText != null && negativeBtnText.Length > 0)
            {
                negativeBtn.Text = negativeBtnText;
            }
            else
            {
                negativeBtn.Visibility = ViewStates.Gone;
            }

            if (positiveBtn.Visibility == ViewStates.Visible && negativeBtn.Visibility == ViewStates.Gone)
            {
                positiveBtn.SetBackgroundResource(Resource.Drawable.popup_btn_c);
            }

            negativeBtn.Click += (s, e) =>
            {
                if (negativeListner != null)
                {
                    negativeListner.OnClick(this, 0);
                }
                Dismiss();
            };

            positiveBtn.Click += (s, e) =>
            {
                if (positiveListener != null)
                {
                    positiveListener.OnClick(this, 0);
                }
                Dismiss();
            };

            FindViewById(Resource.Id.root).Click += (s, e) =>
            {
                Dismiss();
            };

            FindViewById(Resource.Id.popup).Click += (s, e) =>
            {
                // skip
            };
        }
    }
}
