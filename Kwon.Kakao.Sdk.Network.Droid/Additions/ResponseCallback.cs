namespace Com.Kakao.Network.Callback
{
    public abstract partial class ResponseCallback<T> : Java.Lang.Object
    {
        public ResponseCallback() { }

        public virtual unsafe void OnDidEnd() { }
        public virtual unsafe void OnDidStart() { }
        public abstract void OnFailure(global::Com.Kakao.Network.ErrorResult errorResult);
        public virtual unsafe void OnFailureForUiThread(global::Com.Kakao.Network.ErrorResult errorResult) { }
        public abstract void OnSuccess(T result);
        public virtual unsafe void OnSuccessForUiThread(global::Java.Lang.Object result) { }
    }
}