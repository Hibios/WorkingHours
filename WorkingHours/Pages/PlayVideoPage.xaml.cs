using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using System.Windows.Threading;
using WorkingHours.Modules;
using static WorkingHours.EF.Connection;

namespace WorkingHours.Pages
{
    public partial class PlayVideoPage : Page
    {
        private TimeSpan TotalTime;
        private string videoDuration;
        DispatcherTimer timerVideoTime;

        public PlayVideoPage(string videoName, string videoPath, string videoD)
        {
            InitializeComponent();

            txbVideoName.Text = videoName;
            videoWidget.Source = new Uri(videoPath, UriKind.Absolute);
            videoDuration = videoD;

            profileTitle.Text = $"WorkingHours | {UserForRole.userForRole.Organization.Name} - {db.OrganizationTypes.Where(t => t.IdType == UserForRole.userForRole.Organization.IdType).FirstOrDefault().TypeName}";

            progressBar.AddHandler(MouseLeftButtonUpEvent,
                      new MouseButtonEventHandler(timeSlider_MouseLeftButtonUp),
                      true);

            videoWidget.ScrubbingEnabled = true;
            videoWidget.Position = new TimeSpan(0);
            videoWidget.Pause();

            NManager.CheckNotifications(btnNotification, this);
        }

        private void videoWidget_MediaOpened(object sender, RoutedEventArgs e)
        {
            TotalTime = videoWidget.NaturalDuration.TimeSpan;

            // Create a timer that will update the counters and the time slider
            timerVideoTime = new DispatcherTimer();
            timerVideoTime.Interval = TimeSpan.FromSeconds(1);
            timerVideoTime.Tick += new EventHandler(timer_Tick);
            timerVideoTime.Start();
        }

        void timer_Tick(object sender, EventArgs e)
        {
            // Check if the movie finished calculate it's total time
            if (videoWidget.NaturalDuration.TimeSpan.TotalSeconds > 0)
            {
                if (TotalTime.TotalSeconds > 0)
                {
                    // Updating time slider
                    progressBar.Value = videoWidget.Position.TotalSeconds /
                                       TotalTime.TotalSeconds;

                    string crnt = string.Format("{0:D2}:{1:D2}:{2:D2}", videoWidget.Position.Hours, videoWidget.Position.Minutes, videoWidget.Position.Seconds);
                    txbVideoInfo.Text = crnt + " / " + videoDuration;
                }
            }
        }

        private void stopVideo()
        {
            timerVideoTime.Stop();
            videoWidget.Stop();
        }

        private void timeSlider_MouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            if (TotalTime.TotalSeconds > 0)
            {
                videoWidget.Position = TimeSpan.FromSeconds(progressBar.Value * TotalTime.TotalSeconds);
            }
        }

        private void progressBar_OnMouseMove(object sender, MouseEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                var slider = (Slider)sender;
                Point position = e.GetPosition(slider);
                double d = 1.0d / slider.ActualWidth * position.X;
                var p = slider.Maximum * d;
                slider.Value = p;
            }
        }


        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            stopVideo();
            this.NavigationService.Navigate(new Uri("Pages/OrganizatorProfile.xaml", UriKind.Relative));
        }

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            stopVideo();
            this.NavigationService.Navigate(new Uri("Pages/NotificationPage.xaml", UriKind.Relative));
        }
        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            stopVideo();
            this.NavigationService.Navigate(new Uri("Pages/StaffPage.xaml", UriKind.Relative));
        }

        private void videoButton_Click(object sender, RoutedEventArgs e)
        {
            Button controlButton = sender as Button;
            Image controlButtonBtnImage = (Image)controlButton.Content;
            if (controlButtonBtnImage.Source.ToString() == "pack://application:,,,/WorkingHours;component/Resources/PlayVideo.png")
            {
                videoWidget.Play();
                controlButtonBtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/WorkingHours;component/Resources/StopVideo.png"));
            }
            else
            {
                videoWidget.Pause();
                controlButtonBtnImage.Source = new BitmapImage(new Uri("pack://application:,,,/WorkingHours;component/Resources/PlayVideo.png"));
            }
        }

        private void btnBreakTime_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/BreakPage.xaml", UriKind.Relative));
        }
    }
}
