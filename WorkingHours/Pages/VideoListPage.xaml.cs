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
using static WorkingHours.EF.Connection;
using WorkingHours.Model;
using System.Web;
using System.IO;
using WorkingHours.Modules;
using MediaToolkit;

namespace WorkingHours.Pages
{
    public partial class VideoListPage : Page
    {
        public VideoListPage(string userId)
        {
            InitializeComponent();

            User targetUser = db.Users.FirstOrDefault(user => user.IdUser.ToString() == userId);
            Directory.CreateDirectory(targetUser.VideoManager.VideoFolder);
            var myFiles = System.IO.Directory.GetFiles(targetUser.VideoManager.VideoFolder, "WorkingHours_Video_*.avi");

            profileTitle.Text = $"WorkingHours | {UserForRole.userForRole.Organization.Name} - {db.OrganizationTypes.Where(t => t.IdType == UserForRole.userForRole.Organization.IdType).FirstOrDefault().TypeName}";

            List<VideoInfo> videos = new List<VideoInfo>();
            foreach (string fl in myFiles) { videos.Add(new VideoInfo { videoName = System.IO.Path.GetFileName(fl), videoDuration = VideoDuration(fl), videoPath = fl }); };
            videoBox.ItemsSource = videos;

            NManager.CheckNotifications(btnNotification, this);
        }

        public static string VideoDuration(string fileName)
        {
            var inputFile = new MediaToolkit.Model.MediaFile { Filename = fileName };
            using (var engine = new Engine())
            {
                engine.GetMetadata(inputFile);
            }

            TimeSpan t = TimeSpan.FromSeconds(inputFile.Metadata.Duration.TotalSeconds);
            return string.Format("{0:D2}:{1:D2}:{2:D2}", t.Hours, t.Minutes, t.Seconds);
        }

        private void btnStaff_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/StaffPage.xaml", UriKind.Relative));
        }

        private void btnProfile_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/OrganizatorProfile.xaml", UriKind.Relative));
        }

        private void btnNotification_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/NotificationPage.xaml", UriKind.Relative));
        }

        private void TextBlock_MouseDown(object sender, MouseButtonEventArgs e)
        {
            VideoInfo video = (VideoInfo)videoBox.SelectedItem;
            this.NavigationService.Navigate(new PlayVideoPage(video.videoName, video.videoPath, video.videoDuration));
        }

        private void btnBreakTime_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new Uri("Pages/BreakPage.xaml", UriKind.Relative));
        }
    }
}
