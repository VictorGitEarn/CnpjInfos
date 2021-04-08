using CI.DownloadFiles.Worker.Config;
using CI.DownloadFiles.Worker.Strategy.Providers;
using System;

namespace CI.DownloadFiles.Worker.Strategy
{
    public class ProviderFactory
    {
        public static IDownloadFileProvider CreateProvider(Provider provider, ConfigurationHelper config)
        {
            switch (provider)
            {
                case Provider.ReceitaFederal:
                    return new ReceitaFederal(config);

                default:
                    throw new ApplicationException("Provedor de download não fornecido ou não implementado");
            }
        }
    }
}
