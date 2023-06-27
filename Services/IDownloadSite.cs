using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WSC.Services
{
    public interface IDownloadSite
    {
        public Task DownloadAsync(string url, string destinationToDownload,string linksDirectory);
        public  Task<List<string>> GetTheLinks(string url, int subNodeCount);

        public void ClearList();
    }
}
