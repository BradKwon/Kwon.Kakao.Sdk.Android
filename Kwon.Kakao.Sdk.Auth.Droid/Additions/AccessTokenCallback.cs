namespace Com.Kakao.Auth
{
    public abstract partial class AccessTokenCallback :
        global::Com.Kakao.Network.Callback.ResponseCallback<Com.Kakao.Auth.Authorization.Accesstoken.IAccessToken>,
        global::Com.Kakao.Auth.Authorization.Accesstoken.IAccessTokenListener
    {
        public override sealed unsafe void OnDidEnd() { }
        public override sealed unsafe void OnDidStart() { }
        public override sealed unsafe void OnFailure(global::Com.Kakao.Network.ErrorResult errorResult) { }
        public override sealed unsafe void OnFailureForUiThread(global::Com.Kakao.Network.ErrorResult errorResult) { }
        public abstract void OnAccessTokenFailure(global::Com.Kakao.Network.ErrorResult p0);
        public abstract void OnAccessTokenReceived(global::Com.Kakao.Auth.Authorization.Accesstoken.IAccessToken p0);
    }
}