namespace Com.Kakao.Usermgmt
{
    public partial class UserManagement
    {
        public virtual unsafe void RequestUnlink(global::Com.Kakao.Usermgmt.Callback.UnLinkResponseCallback @callback) { }
        public virtual unsafe void RequestUpdateProfile(global::Com.Kakao.Auth.ApiResponseCallback<Java.Lang.Long> @callback, global::System.Collections.Generic.IDictionary<string, string> properties) { }
        public virtual unsafe void RequestLogout(global::Com.Kakao.Usermgmt.Callback.LogoutResponseCallback @callback) { }
        public virtual unsafe void RequestSignup(global::Com.Kakao.Auth.ApiResponseCallback<Java.Lang.Long> @callback, global::System.Collections.Generic.IDictionary<string, string> properties) { }
        public virtual unsafe global::Java.Util.Concurrent.IFuture Me(global::Com.Kakao.Usermgmt.Callback.MeV2ResponseCallback @callback) { return null; }
    }
}