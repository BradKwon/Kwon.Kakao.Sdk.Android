using System;
using System.Collections.Generic;
using Android.Runtime;
using Java.Interop;

namespace Com.Kakao.Auth.Network
{

    // Metadata.xml XPath interface reference: path="/api/package[@name='com.kakao.auth.network']/interface[@name='AuthorizedRequest']"
    [Register("com/kakao/auth/network/AuthorizedRequest", "", "Com.Kakao.Auth.Network.IAuthorizedRequestInvoker")]
    public partial interface IAuthorizedRequest : global::Com.Kakao.Network.IRequest
    {

        // Metadata.xml XPath method reference: path="/api/package[@name='com.kakao.auth.network']/interface[@name='AuthorizedRequest']/method[@name='setAccessToken' and count(parameter)=1 and parameter[1][@type='java.lang.String']]"
        [Register("setAccessToken", "(Ljava/lang/String;)V", "GetSetAccessToken_Ljava_lang_String_Handler:Com.Kakao.Auth.Network.IAuthorizedRequestInvoker, Kakao.Auth.Binding.Android")]
        void SetAccessToken(string p0);

        // Metadata.xml XPath method reference: path="/api/package[@name='com.kakao.auth.network']/interface[@name='AuthorizedRequest']/method[@name='setConfiguration' and count(parameter)=2 and parameter[1][@type='com.kakao.common.PhaseInfo'] and parameter[2][@type='com.kakao.common.IConfiguration']]"
        [Register("setConfiguration", "(Lcom/kakao/common/PhaseInfo;Lcom/kakao/common/IConfiguration;)V", "GetSetConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_Handler:Com.Kakao.Auth.Network.IAuthorizedRequestInvoker, Kakao.Auth.Binding.Android")]
        void SetConfiguration(global::Com.Kakao.Common.IPhaseInfo p0, global::Com.Kakao.Common.IConfiguration p1);

    }

    [global::Android.Runtime.Register("com/kakao/auth/network/AuthorizedRequest", DoNotGenerateAcw = true)]
    internal class IAuthorizedRequestInvoker : global::Java.Lang.Object, IAuthorizedRequest
    {

        internal new static readonly JniPeerMembers _members = new JniPeerMembers("com/kakao/auth/network/AuthorizedRequest", typeof(IAuthorizedRequestInvoker));

        static IntPtr java_class_ref
        {
            get { return _members.JniPeerType.PeerReference.Handle; }
        }

        public override global::Java.Interop.JniPeerMembers JniPeerMembers
        {
            get { return _members; }
        }

        protected override IntPtr ThresholdClass
        {
            get { return class_ref; }
        }

        protected override global::System.Type ThresholdType
        {
            get { return _members.ManagedPeerType; }
        }

        IntPtr class_ref;

        public static IAuthorizedRequest GetObject(IntPtr handle, JniHandleOwnership transfer)
        {
            return global::Java.Lang.Object.GetObject<IAuthorizedRequest>(handle, transfer);
        }

        static IntPtr Validate(IntPtr handle)
        {
            if (!JNIEnv.IsInstanceOf(handle, java_class_ref))
                throw new InvalidCastException(string.Format("Unable to convert instance of type '{0}' to type '{1}'.",
                            JNIEnv.GetClassNameFromInstance(handle), "com.kakao.auth.network.AuthorizedRequest"));
            return handle;
        }

        protected override void Dispose(bool disposing)
        {
            if (this.class_ref != IntPtr.Zero)
                JNIEnv.DeleteGlobalRef(this.class_ref);
            this.class_ref = IntPtr.Zero;
            base.Dispose(disposing);
        }

        public IAuthorizedRequestInvoker(IntPtr handle, JniHandleOwnership transfer) : base(Validate(handle), transfer)
        {
            IntPtr local_ref = JNIEnv.GetObjectClass(((global::Java.Lang.Object)this).Handle);
            this.class_ref = JNIEnv.NewGlobalRef(local_ref);
            JNIEnv.DeleteLocalRef(local_ref);
        }

        static Delegate cb_setAccessToken_Ljava_lang_String_;
#pragma warning disable 0169
        static Delegate GetSetAccessToken_Ljava_lang_String_Handler()
        {
            if (cb_setAccessToken_Ljava_lang_String_ == null)
                cb_setAccessToken_Ljava_lang_String_ = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, IntPtr>)n_SetAccessToken_Ljava_lang_String_);
            return cb_setAccessToken_Ljava_lang_String_;
        }

        static void n_SetAccessToken_Ljava_lang_String_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0)
        {
            global::Com.Kakao.Auth.Network.IAuthorizedRequest __this = global::Java.Lang.Object.GetObject<global::Com.Kakao.Auth.Network.IAuthorizedRequest>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            string p0 = JNIEnv.GetString(native_p0, JniHandleOwnership.DoNotTransfer);
            __this.SetAccessToken(p0);
        }
#pragma warning restore 0169

        IntPtr id_setAccessToken_Ljava_lang_String_;
        public unsafe void SetAccessToken(string p0)
        {
            if (id_setAccessToken_Ljava_lang_String_ == IntPtr.Zero)
                id_setAccessToken_Ljava_lang_String_ = JNIEnv.GetMethodID(class_ref, "setAccessToken", "(Ljava/lang/String;)V");
            IntPtr native_p0 = JNIEnv.NewString(p0);
            JValue* __args = stackalloc JValue[1];
            __args[0] = new JValue(native_p0);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_setAccessToken_Ljava_lang_String_, __args);
            JNIEnv.DeleteLocalRef(native_p0);
        }

        static Delegate cb_setConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_;
#pragma warning disable 0169
        static Delegate GetSetConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_Handler()
        {
            if (cb_setConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_ == null)
                cb_setConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_ = JNINativeWrapper.CreateDelegate((Action<IntPtr, IntPtr, IntPtr, IntPtr>)n_SetConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_);
            return cb_setConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_;
        }

        static void n_SetConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_(IntPtr jnienv, IntPtr native__this, IntPtr native_p0, IntPtr native_p1)
        {
            global::Com.Kakao.Auth.Network.IAuthorizedRequest __this = global::Java.Lang.Object.GetObject<global::Com.Kakao.Auth.Network.IAuthorizedRequest>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            global::Com.Kakao.Common.IPhaseInfo p0 = (global::Com.Kakao.Common.IPhaseInfo)global::Java.Lang.Object.GetObject<global::Com.Kakao.Common.IPhaseInfo>(native_p0, JniHandleOwnership.DoNotTransfer);
            global::Com.Kakao.Common.IConfiguration p1 = (global::Com.Kakao.Common.IConfiguration)global::Java.Lang.Object.GetObject<global::Com.Kakao.Common.IConfiguration>(native_p1, JniHandleOwnership.DoNotTransfer);
            __this.SetConfiguration(p0, p1);
        }
#pragma warning restore 0169

        IntPtr id_setConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_;
        public unsafe void SetConfiguration(global::Com.Kakao.Common.IPhaseInfo p0, global::Com.Kakao.Common.IConfiguration p1)
        {
            if (id_setConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_ == IntPtr.Zero)
                id_setConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_ = JNIEnv.GetMethodID(class_ref, "setConfiguration", "(Lcom/kakao/common/PhaseInfo;Lcom/kakao/common/IConfiguration;)V");
            JValue* __args = stackalloc JValue[2];
            __args[0] = new JValue((p0 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p0).Handle);
            __args[1] = new JValue((p1 == null) ? IntPtr.Zero : ((global::Java.Lang.Object)p1).Handle);
            JNIEnv.CallVoidMethod(((global::Java.Lang.Object)this).Handle, id_setConfiguration_Lcom_kakao_common_PhaseInfo_Lcom_kakao_common_IConfiguration_, __args);
        }

        static Delegate cb_getBodyEncoding;
#pragma warning disable 0169
        static Delegate GetGetBodyEncodingHandler()
        {
            if (cb_getBodyEncoding == null)
                cb_getBodyEncoding = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetBodyEncoding);
            return cb_getBodyEncoding;
        }

        static IntPtr n_GetBodyEncoding(IntPtr jnienv, IntPtr native__this)
        {
            global::Com.Kakao.Auth.Network.IAuthorizedRequest __this = global::Java.Lang.Object.GetObject<global::Com.Kakao.Auth.Network.IAuthorizedRequest>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return JNIEnv.NewString(__this.BodyEncoding);
        }
#pragma warning restore 0169

        IntPtr id_getBodyEncoding;
        public unsafe global::System.String BodyEncoding
        {
            get
            {
                if (id_getBodyEncoding == IntPtr.Zero)
                    id_getBodyEncoding = JNIEnv.GetMethodID(class_ref, "getBodyEncoding", "()Ljava/lang/String;");
                return JNIEnv.GetString(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getBodyEncoding), JniHandleOwnership.TransferLocalRef);
            }
        }

        static Delegate cb_getHeaders;
#pragma warning disable 0169
        static Delegate GetGetHeadersHandler()
        {
            if (cb_getHeaders == null)
                cb_getHeaders = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetHeaders);
            return cb_getHeaders;
        }

        static IntPtr n_GetHeaders(IntPtr jnienv, IntPtr native__this)
        {
            global::Com.Kakao.Auth.Network.IAuthorizedRequest __this = global::Java.Lang.Object.GetObject<global::Com.Kakao.Auth.Network.IAuthorizedRequest>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return global::Android.Runtime.JavaDictionary.ToLocalJniHandle((System.Collections.IDictionary)__this.Headers);
        }
#pragma warning restore 0169

        IntPtr id_getHeaders;
        public unsafe global::System.Collections.Generic.IDictionary<global::System.String, global::System.String> Headers
        {
            get
            {
                if (id_getHeaders == IntPtr.Zero)
                    id_getHeaders = JNIEnv.GetMethodID(class_ref, "getHeaders", "()Ljava/util/Map;");
                return (System.Collections.Generic.IDictionary<string, string>)global::Android.Runtime.JavaDictionary.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getHeaders), JniHandleOwnership.TransferLocalRef);
            }
        }

        static Delegate cb_getMethod;
#pragma warning disable 0169
        static Delegate GetGetMethodHandler()
        {
            if (cb_getMethod == null)
                cb_getMethod = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetMethod);
            return cb_getMethod;
        }

        static IntPtr n_GetMethod(IntPtr jnienv, IntPtr native__this)
        {
            global::Com.Kakao.Auth.Network.IAuthorizedRequest __this = global::Java.Lang.Object.GetObject<global::Com.Kakao.Auth.Network.IAuthorizedRequest>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return JNIEnv.NewString(__this.Method);
        }
#pragma warning restore 0169

        IntPtr id_getMethod;
        public unsafe global::System.String Method
        {
            get
            {
                if (id_getMethod == IntPtr.Zero)
                    id_getMethod = JNIEnv.GetMethodID(class_ref, "getMethod", "()Ljava/lang/String;");
                return JNIEnv.GetString(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getMethod), JniHandleOwnership.TransferLocalRef);
            }
        }

        static Delegate cb_getMultiPartList;
#pragma warning disable 0169
        static Delegate GetGetMultiPartListHandler()
        {
            if (cb_getMultiPartList == null)
                cb_getMultiPartList = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetMultiPartList);
            return cb_getMultiPartList;
        }

        static IntPtr n_GetMultiPartList(IntPtr jnienv, IntPtr native__this)
        {
            global::Com.Kakao.Auth.Network.IAuthorizedRequest __this = global::Java.Lang.Object.GetObject<global::Com.Kakao.Auth.Network.IAuthorizedRequest>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return global::Android.Runtime.JavaList.ToLocalJniHandle((System.Collections.IList)__this.MultiPartList);
        }
#pragma warning restore 0169

        IntPtr id_getMultiPartList;
        public unsafe System.Collections.Generic.IList<Kakao.Network.Multipart.Part> MultiPartList
        {
            get
            {
                if (id_getMultiPartList == IntPtr.Zero)
                    id_getMultiPartList = JNIEnv.GetMethodID(class_ref, "getMultiPartList", "()Ljava/util/List;");
                return (System.Collections.Generic.IList<Com.Kakao.Network.Multipart.Part>)global::Android.Runtime.JavaList.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getMultiPartList), JniHandleOwnership.TransferLocalRef);
            }
        }

        static Delegate cb_getParams;
#pragma warning disable 0169
        static Delegate GetGetParamsHandler()
        {
            if (cb_getParams == null)
                cb_getParams = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetParams);
            return cb_getParams;
        }

        static IntPtr n_GetParams(IntPtr jnienv, IntPtr native__this)
        {
            global::Com.Kakao.Auth.Network.IAuthorizedRequest __this = global::Java.Lang.Object.GetObject<global::Com.Kakao.Auth.Network.IAuthorizedRequest>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return global::Android.Runtime.JavaDictionary.ToLocalJniHandle((System.Collections.IDictionary)__this.Params);
        }
#pragma warning restore 0169

        IntPtr id_getParams;
        public unsafe global::System.Collections.Generic.IDictionary<global::System.String, global::System.String> Params
        {
            get
            {
                if (id_getParams == IntPtr.Zero)
                    id_getParams = JNIEnv.GetMethodID(class_ref, "getParams", "()Ljava/util/Map;");
                return (System.Collections.Generic.IDictionary<string, string>)global::Android.Runtime.JavaDictionary.FromJniHandle(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getParams), JniHandleOwnership.TransferLocalRef);
            }
        }

        static Delegate cb_getUrl;
#pragma warning disable 0169
        static Delegate GetGetUrlHandler()
        {
            if (cb_getUrl == null)
                cb_getUrl = JNINativeWrapper.CreateDelegate((Func<IntPtr, IntPtr, IntPtr>)n_GetUrl);
            return cb_getUrl;
        }

        static IntPtr n_GetUrl(IntPtr jnienv, IntPtr native__this)
        {
            global::Com.Kakao.Auth.Network.IAuthorizedRequest __this = global::Java.Lang.Object.GetObject<global::Com.Kakao.Auth.Network.IAuthorizedRequest>(jnienv, native__this, JniHandleOwnership.DoNotTransfer);
            return JNIEnv.NewString(__this.Url);
        }
#pragma warning restore 0169

        IntPtr id_getUrl;
        public unsafe global::System.String Url
        {
            get
            {
                if (id_getUrl == IntPtr.Zero)
                    id_getUrl = JNIEnv.GetMethodID(class_ref, "getUrl", "()Ljava/lang/String;");
                return JNIEnv.GetString(JNIEnv.CallObjectMethod(((global::Java.Lang.Object)this).Handle, id_getUrl), JniHandleOwnership.TransferLocalRef);
            }
        }

    }

}
