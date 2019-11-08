using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Assignment_UWP_T808A_026.Entity;

namespace Assignment_UWP_T808A_026.Services
{
    class GoogleDriveFileService : IFileService
    {
        public Task<bool> SaveMemberCredentialToFile(MemberCredential memberCredential)
        {
            throw new NotImplementedException();
        }

        public Task<MemberCredential> ReadMemberCredentialFromFile()
        {
            throw new NotImplementedException();
        }

        public void SignOutByDeleteToken()
        {
            throw new NotImplementedException();
        }
    }
}
