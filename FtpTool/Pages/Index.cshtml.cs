using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using FluentFTP;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace FtpTool.Pages
{
    [BindProperties]
    public class IndexModel : PageModel
    {
        /// <summary>
        /// returns the list of all files inside the specified path
        /// </summary>
        public List<FtpListItem> FtpListItems { get; set; }

        // create an FTP client
        FtpClient client = new FtpClient("ftp://test.rebex.net/");
        public void OnGet()
        {
          

            // specify the login credentials, unless you want to use the "anonymous" user account
            client.Credentials = new NetworkCredential("demo", "password");

            // begin connecting to the server
            client.Connect();

            // get a list of files and directories in the "/htdocs" folder
            FtpListItems = client.GetListing("/pub/example").ToList();

            //foreach (FtpListItem item in client.GetListing("/pub/example"))
            //{

            //    // if this is a file
            //    if (item.Type == FtpFileSystemObjectType.File)
            //    {

            //        // get the file size
            //        long size = client.GetFileSize(item.FullName);

            //        // calculate a hash for the file on the server side (default algorithm)
            //        FtpHash hash = client.GetChecksum(item.FullName);
            //    }

            //    // get modified date/time of the file or folder
            //    DateTime time = client.GetModifiedTime(item.FullName);

            //}

            //// upload a file
            //client.UploadFile(@"C:\MyVideo.mp4", "/htdocs/MyVideo.mp4");

            //// rename the uploaded file
            //client.Rename("/htdocs/MyVideo.mp4", "/htdocs/MyVideo_2.mp4");

            //// download the file again
            //client.DownloadFile(@"C:\MyVideo_2.mp4", "/htdocs/MyVideo_2.mp4");

            //// compare the downloaded file with the server
            //if (client.CompareFile(@"C:\MyVideo_2.mp4", "/htdocs/MyVideo_2.mp4") == FtpCompareResult.Equal) { }

            //// delete the file
            //client.DeleteFile("/htdocs/MyVideo_2.mp4");

            //// upload a folder and all its files
            //client.UploadDirectory(@"C:\website\videos\", @"/public_html/videos", FtpFolderSyncMode.Update);

            //// upload a folder and all its files, and delete extra files on the server
            //client.UploadDirectory(@"C:\website\assets\", @"/public_html/assets", FtpFolderSyncMode.Mirror);

            //// download a folder and all its files
            //client.DownloadDirectory(@"C:\website\logs\", @"/public_html/logs", FtpFolderSyncMode.Update);

            //// download a folder and all its files, and delete extra files on disk
            //client.DownloadDirectory(@"C:\website\dailybackup\", @"/public_html/", FtpFolderSyncMode.Mirror);

            //// delete a folder recursively
            //client.DeleteDirectory("/htdocs/extras/");

            //// check if a file exists
            //if (client.FileExists("/htdocs/big2.txt")) { }

            //// check if a folder exists
            //if (client.DirectoryExists("/htdocs/extras/")) { }

            //// upload a file and retry 3 times before giving up
            //client.RetryAttempts = 3;
            //client.UploadFile(@"C:\MyVideo.mp4", "/htdocs/big.txt", FtpRemoteExists.Overwrite, false, FtpVerify.Retry);

            // disconnect! good bye!
            client.Disconnect();
        }

        public void OnGetDownloadFile(string fullName,string name)
        {
            if (string.IsNullOrEmpty(fullName) || string.IsNullOrEmpty(name))
            {
                return;
            }
            // specify the login credentials, unless you want to use the "anonymous" user account
            client.Credentials = new NetworkCredential("demo", "password");

            try
            {
                // begin connecting to the server
                client.Connect();
            }
            catch(Exception ex)
            {
                return;
            }


            client.DownloadFile(string.Format("C:\\Users\\aksha\\Downloads\\{0}", name), fullName);
           
        }

    }
}
