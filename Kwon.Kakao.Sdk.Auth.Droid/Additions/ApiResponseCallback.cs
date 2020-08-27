namespace Com.Kakao.Auth
{
    public abstract partial class ApiResponseCallback<T> : global::Com.Kakao.Network.Callback.ResponseCallback<T>
    {
        public unsafe ApiResponseCallback() { }
        public override unsafe void OnFailure(global::Com.Kakao.Network.ErrorResult errorResult) { }
        public virtual unsafe void OnNotSignedUp() { }
        public abstract void OnSessionClosed(global::Com.Kakao.Network.ErrorResult errorResult);
    }
}