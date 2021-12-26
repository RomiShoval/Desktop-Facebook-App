using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace BasicFacebookFeatures.ApplicationLogic
{
    public class UserManger
    {
        public LoginResult m_LoginResult;
        public User m_LoggedInUser;

        public UserManger()
        {

        }

        public User LoggedInUser
        {
            get
            {
                return m_LoggedInUser;
            }
        }

        public void Login()
        {
            //Clipboard.SetText("design.patterns20aa"); /// the current password for Desig Patter
            m_LoginResult = FacebookService.Login(
                    /// (This is Desig Patter's App ID. replace it with your own)
                    "604062430717597",
                    /// requested permissions:
                    "email",
                    "public_profile",
                    "user_age_range",
                    "user_birthday",
                    "user_events",
                    "user_friends",
                    "user_gender",
                    "user_hometown",
                    "user_likes",
                    "user_link",
                    "user_location",
                    "user_photos",
                    "user_posts",
                    "user_videos"
                    /// add any relevant permissions
                    );
            m_LoggedInUser = m_LoginResult.LoggedInUser;
        }
    }
}
