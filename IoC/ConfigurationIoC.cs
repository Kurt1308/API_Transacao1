using Adaptador.Interfaces;
using Adaptador.Map;
using Aplicacao.Interface;
using Aplicacao.Servico;
using Autofac;
using Core.Interface.Repositorio;
using Core.Interface.Servico;
using Repositorio.RepositorioTransacao;
using Servico.Servico;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IoC
{
    public class ConfigurationIoC
    {
        public static void Load(ContainerBuilder builder)
        {
            #region IOC SERVICE
            builder.RegisterType<ServicoTransacao>().As<IServicoTransacao>();
            #endregion

            #region IOC REPOSITORY
            builder.RegisterType<RepositorioTransacao>().As<IRepositorioTransacao>();
            #endregion

            #region IOC MAPPER
            builder.RegisterType<MapperTransacao>().As<IMapperTransacao>();
            #endregion

            #region IOC APPLICATION
            builder.RegisterType<AplicacaoTransacao>().As<IAplicacaoTransacao>();
            #endregion
            #region IOC VALIDATOR
            //builder.RegisterType<ValidacaoConciliacao>().As<IValidacaoConciliacao>();

            #endregion
        }
    }
}
