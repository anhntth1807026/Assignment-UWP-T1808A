using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_UWP_T808A_026.Entity;

namespace Assignment_UWP_T808A_026.Services
{
    interface IFileService
    {
        Task<bool> SaveMemberCredentialToFile(MemberCredential memberCredential);

        Task<MemberCredential> ReadMemberCredentialFromFile();

        void SignOutByDeleteToken();
    }
}
