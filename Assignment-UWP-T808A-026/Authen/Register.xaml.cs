using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Media.Capture;
using Windows.Storage;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Media.Imaging;
using Windows.UI.Xaml.Navigation;
using Assignment_UWP_T808A_026.Entity;
using Assignment_UWP_T808A_026.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace Assignment_UWP_T808A_026.Authen
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class Register : Page
    {
        private string _gender = "Gender";
        private StorageFile photo;
        private IMemberService _memberService;
        private IFileService _fileService;

        #region event

        public Register()
        {
            this.InitializeComponent();
            this._memberService = new MemberService();
            this._fileService = new LocalFileService();
        }

        private void ButtonBase_OnClick(object sender, RoutedEventArgs e)
        {
            var birthdaySelectedDate = this.Birthday.SelectedDate;
            if (birthdaySelectedDate == null)
            {
                birthdaySelectedDate = DateTime.Now;
            }
            var birthday = birthdaySelectedDate.Value.ToString("yyyy-MM-dd");

            var member = new Member
            {
                firstName = this.FirstName.Text,
                lastName = this.LastName.Text,
                avatar = AvatarUrl.Text,
                phone = this.Phone.Text,
                address = this.Address.Text,
                introduction = this.Introduction.Text,
                email = this.Email.Text,
                gender = _gender.Equals("Male") ? 1 : (_gender.Equals("Female") ? 0 : 2),
                birthday = birthday,
                password = this.Password.Password
            };

            var responseMember = this._memberService.Register(member);
            if (responseMember != null)
            {
                Debug.WriteLine("Register success with id: " + responseMember.id);
            }
            else
            {
                Debug.WriteLine("Register fails !");
            }

            var memberLogin = new MemberLogin();
            memberLogin.email = member.email;
            memberLogin.password = member.password;
            var memberCredential = this._memberService.Login(memberLogin);
            this._fileService.SaveMemberCredentialToFile(memberCredential);
            ProjectAPI.CurrentMemberCredential = memberCredential;
            this.Frame.Navigate(typeof(Views.AllMusic));
        }

        private void RadionButton_OnChecked(object sender, RoutedEventArgs e)
        {
            if (sender is RadioButton checkedRadio)
            {
                this._gender = checkedRadio.Tag.ToString();
            }
        }

        private void BtnOrLogin_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                this.Frame.Navigate(typeof(Login));

            }
            catch (Exception ex)
            {
                Debug.WriteLine(ex.ToString());
            }
        }

        private void CapturePhoto(object sender, RoutedEventArgs e)
        {
            ProcessCaptureImage();
        }

        #endregion

        #region method

        private async void ProcessCaptureImage()
        {
            CameraCaptureUI captureUI = new CameraCaptureUI();
            captureUI.PhotoSettings.Format = CameraCaptureUIPhotoFormat.Jpeg;
            captureUI.PhotoSettings.CroppedSizeInPixels = new Size(200, 200);

            this.photo = await captureUI.CaptureFileAsync(CameraCaptureUIMode.Photo);

            if (this.photo == null)
            {
                return;
            }

            string uploadUrl = GetUploadUrl();
            HttpUploadFile(uploadUrl, "myFile", "image/jpeg");
            Avatar.Visibility = Visibility;

        }

        public async void HttpUploadFile(string url, string paramName, string contentType)
        {
            string boundary = "---------------------------" + DateTime.Now.Ticks.ToString("x"); byte[] boundarybytes = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "\r\n"); HttpWebRequest wr = (HttpWebRequest)WebRequest.Create(url); wr.ContentType = "multipart/form-data; boundary=" + boundary; wr.Method = "POST"; Stream rs = await wr.GetRequestStreamAsync(); rs.Write(boundarybytes, 0, boundarybytes.Length); string header = string.Format("Content-Disposition: form-data; name=\"{0}\"; filename=\"{1}\"\r\nContent-Type: {2}\r\n\r\n", paramName, "path_file", contentType); byte[] headerbytes = System.Text.Encoding.UTF8.GetBytes(header); rs.Write(headerbytes, 0, headerbytes.Length);

            // write file.
            Stream fileStream = await this.photo.OpenStreamForReadAsync(); byte[] buffer = new byte[4096]; int bytesRead = 0; while ((bytesRead = fileStream.Read(buffer, 0, buffer.Length)) != 0) { rs.Write(buffer, 0, bytesRead); }
            byte[] trailer = System.Text.Encoding.ASCII.GetBytes("\r\n--" + boundary + "--\r\n"); rs.Write(trailer, 0, trailer.Length); WebResponse wresp = null; try
            {
                wresp = await wr.GetResponseAsync(); Stream stream2 = wresp.GetResponseStream(); StreamReader reader2 = new StreamReader(stream2);
                string imageUrl = reader2.ReadToEnd(); Debug.WriteLine("Image url:" + imageUrl); Avatar.Source = new BitmapImage(new Uri(imageUrl, UriKind.Absolute)); AvatarUrl.Text = imageUrl;
            }
            catch (Exception ex) { Debug.WriteLine("Error uploading file", ex.StackTrace); Debug.WriteLine("Error uploading file", ex.InnerException); if (wresp != null) { wresp = null; } }
            finally { wr = null; }
        }

        private string GetUploadUrl()
        {
            var httpClient = new HttpClient();
            // thực hiện gửi dữ liệu với phương thức post.
            var response = httpClient.GetAsync(ProjectAPI.GET_UPLOAD_URL).GetAwaiter().GetResult();
            // lấy kết quả trả về từ server.
            var uploadUrl = response.Content.ReadAsStringAsync().Result;
            Debug.WriteLine("Upload url: " + uploadUrl);
            return uploadUrl;
        }

        #endregion
    }
}
