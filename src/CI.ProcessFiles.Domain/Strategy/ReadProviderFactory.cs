using CI.ProcessFiles.Domain.Strategy.Providers;
using System;

namespace CI.ProcessFiles.Domain.Strategy
{
    public class ReadProviderFactory
    {
        public static IReadProvider CreateProvider(Provider provider)
        {
            switch (provider)
            {
                case Provider.ReceitaFederal:
                    return new ReceitaFederal();

                default:
                    throw new ApplicationException("Provedor de leitura não fornecido ou não implementado");
            }
        }
    }
}
