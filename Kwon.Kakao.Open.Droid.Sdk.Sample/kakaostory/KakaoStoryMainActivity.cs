
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
using Kwon.Kakao.Open.Droid.Sdk.Sample.common;
using static Android.Views.View;

namespace Kwon.Kakao.Open.Droid.Sdk.Sample.kakaostory
{
    /**
     * 카카오스토리 API인 프로필, 포스팅(이미지 업로드)를 테스트 한다.
     */
    [Activity(Label = "KakaoStoryMainActivity")]
    public class KakaoStoryMainActivity : BaseActivity, IOnClickListener
    {
        //private string noteContent;
        //private string photoContent;
        //private string linkContent;

        //private string execParam = "place=1111";
        //private string marketParam = "referrer=kakaostory";
        //private string scrapUrl = "http://developers.kakao.com";
        //private ProfileLayout profileLayout;
        //private Button getPostButton;
        //private Button deletePostButton;
        //private Button getPostsButton;
        //private string lastMyStoryId;

        public void OnClick(View v)
        {
            throw new NotImplementedException();
        }

        /**
         * @param savedInstanceState 기존 session 정보가 저장된 객체
         */
        protected override void OnCreate(Bundle savedInstanceState)
        {
            base.OnCreate(savedInstanceState);

            //noteContent = "A Rainbow - William Wordsworth\n" +
            //                "\n" +
            //                "My heart leaps up when I behold\n" +
            //                "A rainbow in the sky:\n" +
            //                "So was it when my life began;\n" +
            //                "So is it now I am a man;\n" +
            //                "So be it when I shall grow old,\n" +
            //                "Or let me die!\n" +
            //                "The Child is father of the Man;\n" +
            //                "I could wish my days to be\n" +
            //                "Bound each to each by natural piety.";
            //photoContent = "This cafe is really awesome!";
            //linkContent = "better than expected!";

            //InitializeView();
            //OnClickIsStoryUser();
        }

        protected override void OnResume()
        {
            base.OnResume();
            //CheckExecParams();
        }

        //private void OnClickIsStoryUser()
        //{
        //    Com.Kakao.KakaoStory KakaoStoryService.getInstance().requestIsStoryUser(new KakaoStoryResponseCallback<Boolean>() {
        //    @Override
        //    public void onSuccess(Boolean result)
        //    {
        //        if (result)
        //        {
        //            onClickProfile();
        //            return;
        //        }
        //        KakaoToast.makeToast(getApplicationContext(), "check story user : " + result, Toast.LENGTH_LONG).show();
        //        if (profileLayout != null)
        //        {
        //            profileLayout.setUserId("Not a KakaoStory user");
        //        }
        //    }
        //});
    }
}
