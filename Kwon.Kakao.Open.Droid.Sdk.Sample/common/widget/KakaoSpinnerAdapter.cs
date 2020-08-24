using System;
using System.Collections.Generic;
using Android.Views;
using Android.Widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget
{
    /**
     * @author leoshin
     * Created by leoshin on 15. 6. 18..
     */
    public class KakaoSpinnerAdapter : BaseAdapter
    {
        private KakaoSpinnerItems items;
        private ISpinnerListener listener;
        private int selectedItemPosition = 0;

        private KakaoSpinnerAdapter()
        {
            this.items = null;
            this.listener = null;
        }

        public KakaoSpinnerAdapter(KakaoSpinnerItems items, ISpinnerListener listener)
        {
            this.items = items;
            this.listener = listener;
        }

        public override int Count => items.GetSize();

        public override Java.Lang.Object GetItem(int position)
        {
            return items.GetTitle(position);
        }

        public override long GetItemId(int position)
        {
            return 0;
        }

        public override View GetView(int position, View convertView, ViewGroup parent)
        {
            View item = convertView;
            if (item == null)
            {
                LayoutInflater inflater = LayoutInflater.From(parent.Context);
                item = inflater.Inflate(Resource.Layout.view_spinner_item, parent, false);
            }

            TextView title = item.FindViewById<TextView>(Resource.Id.menu_title);
            title.Text = items.GetTitle(position);

            if (listener != null)
            {
                CheckBox checkBox = item.FindViewById<CheckBox>(Resource.Id.menu_checkbox);
                int selectedPosition = selectedItemPosition;
                checkBox.Checked = selectedPosition == position;
                item.Click += (s, e) =>
                {
                    selectedItemPosition = position;
                    listener.OnItemSelected(this, position);
                };
            }

            return item;
        }

        public int GetSelectedItemPosition()
        {
            return selectedItemPosition;
        }
    }

    public interface ISpinnerListener
    {
        void OnItemSelected(BaseAdapter adapter, int position);
    }

    public class KakaoSpinnerItems
    {
        private List<string> titleList;
        private int iconResId;

        public KakaoSpinnerItems(int iconResId, List<string> titleList)
        {
            this.titleList = titleList;
            this.iconResId = iconResId;
        }

        public string GetTitle(int position)
        {
            return titleList[position];
        }

        public int GetIconResId()
        {
            return iconResId;
        }

        public int GetSize()
        {
            return titleList.Count;
        }
    }
}
