using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using LightBuzz.SMTP;
using Windows.ApplicationModel.Email;
using Windows.Storage;
using Windows.Storage.Pickers;
using System.Text;
using Windows.Storage.Streams;

//“空白页”项模板在 http://go.microsoft.com/fwlink/?LinkId=402352&clcid=0x409 上有介绍

namespace KindleSender
{
    /// <summary>
    /// 可用于自身或导航至 Frame 内部的空白页。
    /// </summary>
    public sealed partial class MainPage : Page
    {
        EmailModel config = new EmailModel();

        public MainPage()
        {
            this.InitializeComponent();

        }
        protected override void OnNavigatedTo(NavigationEventArgs e)
        {
            base.OnNavigatedTo(e);

            config = e.Parameter as EmailModel;
            if (config == null)
            {
                this.Frame.Navigate(typeof(ConfigPage), null);
            }
        }

        private async void Button_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                FileOpenPicker filepicker = new FileOpenPicker();
                filepicker.FileTypeFilter.Add(".mobi");
                var files = await filepicker.PickMultipleFilesAsync();
                if (files != null && files.Count > 0)
                {
                    Loading.IsActive = true;
                    StringBuilder str = new StringBuilder();
                    List<EmailAttachment> listAttachment = new List<EmailAttachment>();
                    foreach (var item in files)
                    {
                        str.AppendFormat("{0}\r", item.Path);
                        var rastream = RandomAccessStreamReference.CreateFromFile(item);
                        listAttachment.Add(new EmailAttachment(item.Path, rastream));
                    }
                    txtPath.Text = str.ToString();

                    using (SmtpClient client = new SmtpClient(config.Smtp, config.Port, false, config.From, config.Password))
                    {
                        EmailMessage emailMessage = new EmailMessage();

                        emailMessage.To.Add(new EmailRecipient(config.To));

                        foreach (var item in listAttachment)
                        {
                            emailMessage.Attachments.Add(item);
                        }
                        emailMessage.Subject = "Kindle";
                        emailMessage.Body = "Kindle";

                        var result = await client.SendMail(emailMessage);
                        if (result)
                        {
                            Loading.IsActive = false;
                            txtPath.Text += "\r发送成功！";
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Loading.IsActive = false;
                throw;
            }
        }

        private void ButtonConfig_Click(object sender, RoutedEventArgs e)
        {
            this.Frame.Navigate(typeof(ConfigPage), config);
        }

    }
}
