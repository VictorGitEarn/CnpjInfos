using CI.DownloadFiles.Worker.Config;

namespace CI.DownloadFiles.Worker.Strategy
{
    public interface IDownloadFileProvider
    {
        bool DownloadFiles(SeleniumHelper helper);

        void DownloadFromDemo(SeleniumHelper helper);
    }
}
