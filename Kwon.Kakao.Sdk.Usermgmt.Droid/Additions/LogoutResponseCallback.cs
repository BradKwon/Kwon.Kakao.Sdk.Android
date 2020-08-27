namespace Com.Kakao.Usermgmt.Callback
{
    public abstract partial class LogoutResponseCallback : global::Com.Kakao.Auth.ApiResponseCallback<Java.Lang.Long>
    {
        public abstract void OnCompleteLogout();
    }
}