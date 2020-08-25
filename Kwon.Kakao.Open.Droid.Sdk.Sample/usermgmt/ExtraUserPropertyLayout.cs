using System;
using System.Collections.Generic;
using Android.Content;
using Android.Util;
using Android.Views;
using Android.Widget;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.usermgmt
{
    /**
     * 추가로 받고자 하는 사용자 정보를 나타내는 layout
     * 이름, 나이, 성별을 입력할 수 있다.
     * @author MJ
     */
    public class ExtraUserPropertyLayout : FrameLayout
    {
        // property key
        private static string NAME_KEY = "name";
        private static string AGE_KEY = "age";
        private static string GENDER_KEY = "gender";

        private EditText name;
        private EditText age;
        private KakaoDialogSpinner gender;

        public ExtraUserPropertyLayout(Context context) : base(context)
        {
            InitView();
        }

        public ExtraUserPropertyLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitView();
        }

        public ExtraUserPropertyLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            InitView();
        }

        private void InitView()
        {
            View view = Inflate(Context, Resource.Layout.layout_usermgmt_extra_user_property, this);
            name = view.FindViewById<EditText>(Resource.Id.name);
            age = view.FindViewById<EditText>(Resource.Id.age);
            gender = view.FindViewById<KakaoDialogSpinner>(Resource.Id.gender);
        }

        public Dictionary<string, string> GetProperties()
        {
            string nickNameValue = name.Text;
            string ageValue = age.Text;
            string genderValue = gender.GetSelectedItem().ToString();

            Dictionary<string, string> properties = new Dictionary<string, string>();
            properties.Add(NAME_KEY, nickNameValue);
            properties.Add(AGE_KEY, ageValue);
            if (genderValue != null)
                properties.Add(GENDER_KEY, genderValue);

            return properties;
        }

        public void ShowProperties(IDictionary<string, string> properties)
        {
            string nameValue = properties[NAME_KEY];
            if (nameValue != null)
                name.Text = nameValue;

            string ageValue = properties[AGE_KEY];
            if (ageValue != null)
                age.Text = ageValue;

            string genderValue = properties[GENDER_KEY];
            if (genderValue != null)
            {
                if (genderValue.Equals(Context.GetString(Resource.String.female), StringComparison.OrdinalIgnoreCase))
                {
                    gender.SetSelection(0);
                }
                else
                {
                    gender.SetSelection(1);
                }
            }
        }
    }
}
