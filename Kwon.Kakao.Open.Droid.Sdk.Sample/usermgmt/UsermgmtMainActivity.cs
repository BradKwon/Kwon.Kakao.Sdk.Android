﻿
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
using Com.Kakao.Usermgmt;
using Com.Kakao.Usermgmt.Response;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common;
using Kwon.Kakao.Open.Droid.Sdk.Sample.common.widget;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.usermgmt
{
    /**
     * 가입된 사용자가 보게되는 메인 페이지로 사용자 정보 불러오기/update, 로그아웃, 탈퇴 기능을 테스트 한다.
     */
    [Activity(Label = "UsermgmtMainActivity")]
    public class UsermgmtMainActivity : BaseActivity
    {
        private MeV2Response response;
        private ProfileLayout profileLayout;
        private ExtraUserPropertyLayout extraUserPropertyLayout;

        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            // Create your application here
            UserManagement.Instance.RequestUpdateProfile()
        }
    }
}
