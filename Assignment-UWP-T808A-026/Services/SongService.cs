using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Assignment_UWP_T808A_026.Entity;
using Newtonsoft.Json;

namespace Assignment_UWP_T808A_026.Services
{
    class SongService : ISongService 
    {
        public Song CreateSong(MemberCredential memberCredential, Song song)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(memberCredential.token);
            var dataToSend = JsonConvert.SerializeObject(song);
            var content = new StringContent(dataToSend, Encoding.UTF8, "application/json");
            var response = httpClient.PostAsync(ProjectAPI.SONG_CREATE_URL, content).GetAwaiter().GetResult();
            var jsonContent = response.Content.ReadAsStringAsync().Result;
            var responseSong = JsonConvert.DeserializeObject<Song>(jsonContent);
            Debug.WriteLine("Create success with song name: " + responseSong.name);
            return responseSong;
        }

        public List<Song> GetAllSong(MemberCredential memberCredential)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(memberCredential.token);
            var response = httpClient.GetAsync(ProjectAPI.SONG_GETALL_URL).GetAwaiter().GetResult();
            var listSong = JsonConvert.DeserializeObject<List<Song>>(response.Content.ReadAsStringAsync().Result);
            return listSong;
        }

        public List<Song> GetMineSongs(MemberCredential memberCredential)
        {
            var httpClient = new HttpClient();
            httpClient.DefaultRequestHeaders.Authorization =
                new AuthenticationHeaderValue(memberCredential.token);
            var response = httpClient.GetAsync(ProjectAPI.SONG_GETMINE_URL).GetAwaiter().GetResult();
            var listSong = JsonConvert.DeserializeObject<List<Song>>(response.Content.ReadAsStringAsync().Result);
            return listSong;
        }
    }
}
