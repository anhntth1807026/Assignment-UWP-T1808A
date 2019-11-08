using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assignment_UWP_T808A_026.Entity
{
    class ProjectAPI
    {
        #region link

        public const string API = "https://2-dot-backup-server-003.appspot.com/";
        public const string MEMBER_REGISTER_URL = API + "_api/v2/members";
        public const string GET_UPLOAD_URL = API + "get-upload-token";
        public const string MEMBER_LOGIN_URL = API + "_api/v2/members/authentication";
        public const string MEMBER_GET_INFORMATION = API + "_api/v2/members/information";
        public const string SONG_CREATE_URL = API + "_api/v2/songs";
        public const string SONG_GETMINE_URL = API + "_api/v2/songs/get-mine";
        public const string SONG_GETALL_URL = API + "_api/v2/songs";

        #endregion

        #region variable

        public static MemberCredential CurrentMemberCredential;
        public static List<Song> ListSongSearch;
        public static string txtNavViewSearchBox;

        #endregion
    }
}
