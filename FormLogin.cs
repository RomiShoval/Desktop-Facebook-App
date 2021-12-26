using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using FacebookWrapper.ObjectModel;
using FacebookWrapper;
using BasicFacebookFeatures.ApplicationLogic;

namespace BasicFacebookFeatures
{
    public partial class FormLogin : Form
    {

        UserManger m_user = new UserManger();
        public FormLogin()
        {
            InitializeComponent();
        }

        private void buttonLogin_Click(object sender, EventArgs e) 
        {
            m_user.Login();
            FormMain form = new FormMain(m_user);
            form.ShowDialog();
            Close();
        }
    }
}
