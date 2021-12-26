using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using BasicFacebookFeatures.ApplicationLogic;

namespace BasicFacebookFeatures
{
    public partial class FormMain : Form
    {
       
        UserManger m_user;
        FeaturesLogic m_closeFriendsList = new FeaturesLogic();
        
        
        public FormMain(UserManger i_user)
        {
            InitializeComponent();
            m_user = i_user;
            FacebookWrapper.FacebookService.s_CollectionLimit = 100;
            fetchprofilePic();
            fetchUserName();
            fetchHometown();
            fetchAlbumsList();
            fetchFriendsList();
            fetchEventsList();
            fetchLikedPagesList();
            fetchSingleFriends();
        }
        

        private void fetchprofilePic()
        {
            profilePic.Image = m_user.LoggedInUser.ImageLarge;
        }

        private void fetchUserName()
        {
            try
            {
                userName.Text = m_user.LoggedInUser.Name;
            }
            catch (Exception)
            {

                userName.Text = "No user name was specified";
            }
        }

        private void fetchHometown()
        {
            try
            {
                if (m_user.LoggedInUser.Hometown != null)
                {
                    Hometown.Text = m_user.LoggedInUser.Location.Name;
                }
            }
            catch (Exception)
            {
                Hometown.Text = "No Hometown was specified";
            }

        }

        private void fetchEventsList()
        {
            try
            {
                foreach (Event fbEvent in m_user.LoggedInUser.Events)
                {
                    eventsList.Items.Add(fbEvent.Name + " Starts at: " + fbEvent.StartTime);
                }
                //MessageBox.Show(eventsList.Items[0].ToString());
            }
            catch (Exception)
            {
                MessageBox.Show("No events to retrieve :(");
                //throw;
            }
        }

        private void fetchAlbumsList()
        {
            try
            {
                foreach (Album album in m_user.LoggedInUser.Albums)
                {
                    albumsList.Items.Add(album.Name);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No Albums to retrieve :(");
            }

        }

        private void albumsList_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (albumsList.Items.Count > 0)
            {
                fetchDisplayAlbumsPhotos(albumsList.SelectedItem.ToString());
            }
        }

        private void fetchDisplayAlbumsPhotos(string i_AlbumName)
        {
            ImageList imagelist = new ImageList();
            imagelist.ImageSize = new Size(40, 40);
            foreach (Photo photo in m_user.LoggedInUser.PhotosTaggedIn)
            {
                if (photo.Album.Name == i_AlbumName)
                {
                    imagelist.Images.Add(photo.ImageNormal);
                }

            }

            PhotosToShow.SmallImageList = imagelist;
            for (int i = 0; i < imagelist.Images.Count; i++)
            {
                ListViewItem item = new ListViewItem();
                item.ImageIndex = i;
                PhotosToShow.Items.Add(item);
            }
        }

        private void fetchFriendsList()
        {
            try
            {
                if (m_user.LoggedInUser.Friends.Count == 0)
                {
                    MessageBox.Show("you dont have any friends :(");
                }
                foreach (User friend in m_user.LoggedInUser.Friends)
                {
                    FriendsCheckedListBox.Items.Add(friend);
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No friends to retrieve :(");
            }

        }

        private void fetchLikedPagesList()
        {
            try
            {
                foreach (Page page in m_user.LoggedInUser.LikedPages)
                {
                    likedPagesList.Items.Add(page.Name);
                }
                if (m_user.LoggedInUser.LikedPages.Count == 0)
                {
                    MessageBox.Show("you dont have any liked pages :(");
                }
            }
            catch (Exception)
            {
                MessageBox.Show("No liked pages to retrieve :(");
                //throw;
            }

        }

        private void buttonLogout_Click(object sender, EventArgs e)
        {
			FacebookService.LogoutWithUI();
		}

        private void addCloseFriendsButton_Click(object sender, EventArgs e)
        {
            string timeLeftForBirthday = "";
            foreach (User checkedFriend in FriendsCheckedListBox.CheckedItems)
            {
                m_closeFriendsList.AddCloseFriends(checkedFriend);
                CloseFriendsListBox.Items.Add(checkedFriend);
                timeLeftForBirthday = m_closeFriendsList.FetchCurrentMonthBirthdayList();
                CurrentMonthBirthdaysListBox.Items.Add(string.Format("{0}{1}", checkedFriend.Name, timeLeftForBirthday));
            }  
        }

        private void removeCloseFriendsButton_Click(object sender, EventArgs e)
        {
            foreach (User checkedFriend in FriendsCheckedListBox.CheckedItems)
            {
                CloseFriendsListBox.Items.Remove(checkedFriend);
                m_closeFriendsList.RemoveCloseFriends(checkedFriend);
                for (int i = 0; i < CurrentMonthBirthdaysListBox.Items.Count; i++)
                {
                    CurrentMonthBirthdaysListBox.Items.RemoveAt(i);
                }
            }
        }

        private void buttonSetStatus_Click(object sender, EventArgs e)
        {
            try
            {
                Status postedStatus = m_user.LoggedInUser.PostStatus(textBoxBirthdayBless.Text);
                MessageBox.Show("Status Posted! ID: " + postedStatus.Id);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
        }

        public void fetchSingleFriends()
        {
            foreach (User friend in m_user.LoggedInUser.Friends)
            {
                if (friend.SignificantOther == null)
                {
                    singleFriendsListBox.Items.Add(friend);
                }
            }
            if (m_user.LoggedInUser.Friends.Count == 0)
            {
                MessageBox.Show("you don't have any friends :(");
            }
        }

        private void singleFriendsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (singleFriendsListBox.Items.Count > 0)
            {
                displaySingleProfile(singleFriendsListBox.SelectedItem.ToString());
            }
        }

        private void displaySingleProfile(string i_SingleFriendName)
        {
            foreach (User friend in m_user.LoggedInUser.Friends)
            {
                if (friend.Name == i_SingleFriendName)
                {
                    singleProfilePicture.Image = friend.ImageLarge;
                    singleNameLabel.Text = friend.Name;
                    //singleLocationLabel.Text = "Living in: " + friend.Location.Name;
                    singleBirthdayLabel.Text = "Birthday: " + friend.Birthday;

                    foreach (Page page in m_user.LoggedInUser.LikedPages)
                    {
                        foreach (Page friendPage in friend.LikedPages)
                        {
                            if (friendPage.Name == page.Name)
                            {
                                mutualLikesListBox.Items.Add(page.Name);
                            }
                        }
                    }

                    foreach (Event fbEvent in friend.Events)
                    {
                        singleEventsListBox.Items.Add(fbEvent);
                    }
                }
            }
        }

        private void singleEventsListBox_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (singleEventsListBox.Items.Count > 0)
            {
                MessageBox.Show((singleEventsListBox.SelectedItem as Event).Description);
            }
        }
    }
}
