using System;
using System.Collections.Generic;
using Android.App;
using Android.Content;
using Android.Content.Res;
using Android.Util;
using Android.Views;
using Android.Widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget
{
    /**
     * @author leo.shin
     */
    public class KakaoDialogSpinner : LinearLayout
    {
        private List<string> entryList = null;
        private string title = null;
        private Dialog dialog = null;
        private ListView listView = null;
        private KakaoSpinnerAdapter adapter = null;
        private int iconResId = 0;
        private TextView spinner;
        private bool showTitleDivider = false;
        private int titleBgResId = 0;
        private int titleTextColor = 0;
        private ISpinnerListener listener;

        public KakaoDialogSpinner(Context context) : base(context)
        {
            InitView(context, null);
        }

        public KakaoDialogSpinner(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitView(context, null);
        }

        private void InitView(Context context, IAttributeSet attrs)
        {
            LayoutInflater inflater = LayoutInflater.From(context);
            View layout = inflater.Inflate(Resource.Layout.view_spinner, this, false);
            AddView(layout);

            if (attrs != null)
            {
                TypedArray a = context.ObtainStyledAttributes(attrs, Resource.Styleable.KakaoDialogSpinner);
                title = a.GetString(Resource.Styleable.KakaoDialogSpinner_kakao_prompt);
                iconResId = a.GetResourceId(Resource.Styleable.KakaoDialogSpinner_kakao_icon, 0);
                titleBgResId = a.GetResourceId(Resource.Styleable.KakaoDialogSpinner_kakao_dialogTitle, 0);
                titleTextColor = a.GetResourceId(Resource.Styleable.KakaoDialogSpinner_kakao_titleTextColor, 0);
                showTitleDivider = a.GetBoolean(Resource.Styleable.KakaoDialogSpinner_kakao_showTitleDivider, false);

                int entriesResId = a.GetResourceId(Resource.Styleable.KakaoDialogSpinner_kakao_entries, 0);
                if (entriesResId > 0)
                {
                    entryList = new List<string>(Resources.GetStringArray(entriesResId));
                }

                a.Recycle();
            }

            ImageView icon = layout.FindViewById<ImageView>(Resource.Id.menu_icon);
            if (iconResId > 0)
            {
                icon.Visibility = ViewStates.Visible;
                icon.SetBackgroundResource(iconResId);
            }
            else
            {
                icon.Visibility = ViewStates.Gone;
            }

            spinner = layout.FindViewById<TextView>(Resource.Id.menu_title);
            if (entryList != null && entryList.Count > 0)
            {
                spinner.Text = entryList[0];
            }

            DialogBuilder builder = new DialogBuilder(context);
            if (title != null)
            {
                builder = builder.SetTitle(title);
            }

            builder.SetTitleBgResId(titleBgResId);
            builder.SetTitleTextColor(titleTextColor);
            builder.SetShowTitleDivider(showTitleDivider);

            if (entryList != null)
            {
                listView = (ListView)inflater.Inflate(Resource.Layout.view_custom_list, null, false);
                builder.SetView(listView);
                dialog = builder.Create();

                adapter = new KakaoSpinnerAdapter(new KakaoSpinnerItems(iconResId, entryList), new MySpinnerListener((adapter, position) =>
                {
                    spinner.Text = entryList[position];
                    if (dialog != null)
                    {
                        dialog.Dismiss();
                    }

                    if (listener != null)
                    {
                        listener.OnItemSelected(adapter, position);
                    }
                }));
                listView.SetAdapter(adapter);
            }

            this.Click += (s, e) =>
            {
                ShowDialog();
            };
        }

        public void SetTitle(string title)
        {
            this.title = title;
        }

        public void SetOnListener(ISpinnerListener l)
        {
            this.listener = l;
        }

        private void ShowDialog()
        {
            if (dialog != null)
            {
                dialog.Show();
            }
        }

        public object GetSelectedItem()
        {
            if (adapter != null && listView != null)
            {
                return adapter.GetItem(adapter.GetSelectedItemPosition());
            }

            return null;
        }

        public void SetSelection(int position)
        {
            spinner.Text = entryList[position];
        }

        public int GetSelectedItemPosition()
        {
            return adapter.GetSelectedItemPosition();
        }

        private class MySpinnerListener : Java.Lang.Object, ISpinnerListener
        {
            Action<BaseAdapter, int> action;

            public MySpinnerListener(Action<BaseAdapter, int> action)
            {
                this.action = action;
            }

            public void OnItemSelected(BaseAdapter adapter, int position)
            {
                action.Invoke(adapter, position);
            }
        }
    }
}
