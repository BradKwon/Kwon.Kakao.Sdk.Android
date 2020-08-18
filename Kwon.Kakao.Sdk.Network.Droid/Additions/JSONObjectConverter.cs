using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Com.Kakao.Network.Response;
using Org.Json;
using Object = Java.Lang.Object;

namespace Com.Kakao.Network.Response
{
    public abstract partial class JSONObjectConverter
    {
        public Object Convert(Java.Lang.Object p0)
        {
            return Convert((JSONObject)p0);
        }

        Object IResponseConverter.FromArray(JSONArray p0, int p1)
        {
            return FromArray(p0, p1);
        }

    }
}