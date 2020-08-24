using Android.App;
using Android.Content;
using Android.Text;
using Android.Util;
using Android.Views;
using Android.Widget;
using Com.Kakao.Usermgmt;
using Com.Kakao.Usermgmt.Callback;
using Com.Kakao.Usermgmt.Response;
using Com.Kakao.Usermgmt.Response.Model;
using Com.Kakao.Util;
using Java.Lang;
using Volley.Toolbox;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget
{
    /**
     * 기본 UserProfile(사용자 ID, 닉네임, 프로필 이미지)을 그려주는 Layout.
     * </br>
     * 1. 프로필을 노출할 layout에 {@link com.kakao.sdk.sample.common.widget.ProfileLayout}을 선언한다.
     * </br>
     * 2. {@link com.kakao.sdk.sample.common.widget.ProfileLayout#setMeV2ResponseCallback(MeV2ResponseCallback)}를 이용하여 사용자정보 요청 결과에 따른 callback을 설정한다.
     * </br>
     *
     * @author MJ
     */
    public class ProfileLayout : FrameLayout
    {
        private MeV2ResponseCallback meV2ResponseCallback;

        private string email;
        private string phoneNumber;
        private string nickname;
        private string userId;
        private string birthDay;
        private string ageRange;
        private string gender;
        private string countryIso;
        private NetworkImageView profile;
        private NetworkImageView background;
        private TextView profileDescription;

        public ProfileLayout(Context context) : base(context)
        {
            InitView();
        }

        public ProfileLayout(Context context, IAttributeSet attrs) : base(context, attrs)
        {
            InitView();
        }

        public ProfileLayout(Context context, IAttributeSet attrs, int defStyleAttr) : base(context, attrs, defStyleAttr)
        {
            InitView();
        }

        /**
         * 사용자정보 요청 결과에 따른 callback을 설정한다.
         *
         * @param callback 사용자정보 요청 결과에 따른 callback
         */
        public void SetMeV2ResponseCallback(MeV2ResponseCallback callback)
        {
            this.meV2ResponseCallback = callback;
        }

        public void SetUserInfo(MeV2Response response)
        {
            UserAccount account = response.KakaoAccount;
            SetUserId(response.Id.ToString());
            if (account != null)
            {
                if (account.EmailNeedsAgreement() == OptionalBoolean.True)
                {
                    SetEmail(Context.GetString(Resource.String.needs_account_email_scope));
                }
                else
                {
                    SetEmail(account.Email);
                }
                if (account.PhoneNumberNeedsAgreement() == OptionalBoolean.True)
                {
                    SetPhoneNumber(Context.GetString(Resource.String.needs_phone_number_scope));
                }
                else
                {
                    SetPhoneNumber(account.PhoneNumber);
                }
                if (account.BirthdayNeedsAgreement() == OptionalBoolean.True)
                {
                    SetBirthDay(account.Birthday);
                }
                if (account.Profile.ProfileImageUrl != null)
                {
                    SetProfileURL(account.Profile.ProfileImageUrl);
                }
                if (account.AgeRange != null)
                {
                    SetAgeRange(account.AgeRange);
                }
                if (account.Gender != null)
                {
                    SetGender(account.Gender);
                }

                if (account.Profile.Nickname != null)
                {
                    SetNickname(account.Profile.Nickname);
                }
            }
            UpdateLayout();
        }

        public void SetEmail(string email)
        {
            this.email = email;
            UpdateLayout();
        }

        public void SetPhoneNumber(string phoneNumber)
        {
            this.phoneNumber = phoneNumber;
            UpdateLayout();
        }

        /**
         * 프로필 이미지에 대해 view를 update한다.
         *
         * @param profileImageURL 화면에 반영할 프로필 이미지
         */
        public void SetProfileURL(string profileImageURL)
        {
            if (profile != null && profileImageURL != null)
            {
                Application app = GlobalApplication.GetGlobalApplicationContext();
                if (app == null)
                    throw new UnsupportedOperationException("needs com.kakao.GlobalApplication in order to use ImageLoader");
                profile.SetImageUrl(profileImageURL, ((GlobalApplication)app).GetImageLoader());
            }
        }

        public void SetBgImageURL(string bgImageURL)
        {
            if (bgImageURL != null)
            {
                Application app = GlobalApplication.GetGlobalApplicationContext();
                if (app == null)
                    throw new UnsupportedOperationException("needs com.kakao.GlobalApplication in order to use ImageLoader");
                background.SetImageUrl(bgImageURL, ((GlobalApplication)app).GetImageLoader());
            }
        }

        public void SetDefaultBgImage(int imageResId)
        {
            if (background != null)
            {
                background.SetBackgroundResource(imageResId);
            }
        }

        public void SetDefaultProfileImage(int imageResId)
        {
            if (profile != null)
            {
                profile.SetBackgroundResource(imageResId);
            }
        }

        public void SetCountryIso(string countryIso)
        {
            this.countryIso = countryIso;
            UpdateLayout();
        }

        /**
         * 별명 view를 update한다.
         *
         * @param nickname 화면에 반영할 별명
         */
        public void SetNickname(string nickname)
        {
            this.nickname = nickname;
            UpdateLayout();
        }

        public void SetBirthDay(string birthDay)
        {
            this.birthDay = birthDay;
            UpdateLayout();
        }

        public void SetAgeRange(AgeRange ageRange)
        {
            if (ageRange != null)
            {
                this.ageRange = ageRange.Value;
            }
        }

        public void SetGender(Gender gender)
        {
            if (gender != null)
            {
                this.gender = gender.Value;
            }
        }

        public void SetBackground(NetworkImageView background)
        {
            this.background = background;
        }

        /**
         * 사용자 아이디 view를 update한다.
         *
         * @param userId 화면에 반영할 사용자 아이디
         */
        public void SetUserId(string userId)
        {
            this.userId = userId;
            UpdateLayout();
        }

        private void UpdateLayout()
        {
            StringBuilder builder = new StringBuilder();

            if (!TextUtils.IsEmpty(email))
            {
                builder.Append(Resources.GetString(Resource.String.com_kakao_profile_email)).Append('\n').Append(email).Append('\n');
            }
            if (!TextUtils.IsEmpty(phoneNumber))
            {
                builder.Append(Resources.GetString(Resource.String.com_kakao_profile_phone_number)).Append('\n').Append(phoneNumber).Append('\n');
            }
            if (nickname != null && nickname.Length > 0)
            {
                builder.Append(Resources.GetString(Resource.String.com_kakao_profile_nickname)).Append("\n").Append(nickname).Append("\n");
            }

            if (userId != null && userId.Length > 0)
            {
                builder.Append(Resources.GetString(Resource.String.com_kakao_profile_userId)).Append("\n").Append(userId).Append("\n");
            }
            if (gender != null)
            {
                builder.Append(Resources.GetString(Resource.String.com_kakao_profile_gender)).Append(" ").Append(gender).Append("\n");
            }
            if (ageRange != null)
            {
                builder.Append(Resources.GetString(Resource.String.com_kakao_profile_age_range)).Append(" ").Append(ageRange).Append("\n");
            }
            if (birthDay != null && birthDay.Length > 0)
            {
                builder.Append(Resources.GetString(Resource.String.com_kakao_profile_birthday)).Append(" ").Append(birthDay);
            }
            if (countryIso != null)
            {
                builder.Append(Resources.GetString(Resource.String.kakaotalk_country_label)).Append("\n").Append(countryIso);
            }
            if (profileDescription != null)
            {
                profileDescription.Text = builder.ToString();
            }
        }

        private void InitView()
        {
            View view = Inflate(Context, Resource.Layout.layout_common_kakao_profile, this);

            profile = view.FindViewById<NetworkImageView>(Resource.Id.com_kakao_profile_image);
            background = view.FindViewById<NetworkImageView>(Resource.Id.background);
            profileDescription = view.FindViewById<TextView>(Resource.Id.profile_description);
        }

        /**
         * 사용자 정보를 요청한다.
         */
        public void RequestMe()
        {
            UserManagement.Instance.Me(meV2ResponseCallback);
        }
    }
}
