using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using System.Text;
using System.Xml.Linq;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;

// “空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=234238 上有介绍

namespace KindleSender
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class ConfigPage : Page
    {
        EmailModel config = new EmailModel();
        public ConfigPage()
        {
            this.InitializeComponent();
        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            config = (e.Parameter as EmailModel) ?? new EmailModel();
            txtLoginName.Text = config.From ?? "";
            txtPassword.Text = config.Password ?? "";
            txtPort.Text = (config.Port == 0 ? 25 : config.Port).ToString();
            txtSmtp.Text = config.Smtp ?? "";
            txtKindle.Text = config.To ?? "";
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            if (txtLoginName.Text.Trim().Length < 1)
            {
                txtMsg.Text = "用户名不能为空！";
                return;
            }
            if (txtPassword.Text.Trim().Length < 1)
            {
                txtMsg.Text = "密码不能为空！";
                return;
            }
            if (txtSmtp.Text.Trim().Length < 1)
            {
                txtMsg.Text = "SMTP地址不能为空！";
                return;
            }
            if (txtPort.Text.Trim().Length < 1)
            {
                txtMsg.Text = "端口号不能为空！";
                return;
            }
            if (txtKindle.Text.Trim().Length < 1)
            {
                txtMsg.Text = "Kindle邮箱不能为空！";
                return;
            }
            txtMsg.Text = "";
            config.From = txtLoginName.Text;
            config.Password = txtPassword.Text;
            int port;
            if (!int.TryParse(txtPort.Text, out port))
            {
                port = 25;
            }
            config.Port = port;
            config.Smtp = txtSmtp.Text;
            config.To = txtKindle.Text;
            DAL dal = new DAL();
            config = dal.Edit(config);
            this.Frame.Navigate(typeof(MainPage), config);
        }
    }
}
