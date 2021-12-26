using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;

namespace BasicFacebookFeatures.ApplicationLogic
{
    public class FeaturesLogic
    {
        public List<User> m_closeFriendsList = new List<User>();

        public FeaturesLogic()
        {
            
        }
        public string FetchCurrentMonthBirthdayList()
        {
            foreach (User friend in m_closeFriendsList)
            {
                String timeLeftForBirthday = "";
                if (friend.Birthday != null)
                {
                    DateTime friendbirthdaydate = Convert.ToDateTime(friend.Birthday);
                    if (DateTime.Now.Month == friendbirthdaydate.Month)
                    {
                        double daysleftforbirthday = (friendbirthdaydate - DateTime.Today).TotalDays;
                        double hoursleftforbirthday = (friendbirthdaydate - DateTime.Today).TotalHours;
                        double minutesleftforbirthday = (friendbirthdaydate - DateTime.Today).TotalMinutes;
                        timeLeftForBirthday = string.Format("Days,hours,minutes left until birthday are : {0} , {1} , {2}",
                            daysleftforbirthday, hoursleftforbirthday, minutesleftforbirthday);
                    }
                    else
                    {
                        timeLeftForBirthday ="'s birthday isn't this month):";
                    }
                    return timeLeftForBirthday;
                }

            }
            return "Birthday date isn't available!";
        }

        public void AddCloseFriends(User i_checkedFriend)
        {
            m_closeFriendsList.Add(i_checkedFriend);
        }

        public void RemoveCloseFriends(User i_checkedFriend)
        {
            m_closeFriendsList.Remove(i_checkedFriend);
        }
    }
}
