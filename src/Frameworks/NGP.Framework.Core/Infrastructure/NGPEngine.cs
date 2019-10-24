/* ---------------------------------------------------------------------    
 * Copyright:
 * IXinWu Technology Co., Ltd. All rights reserved. 
 * 
 * NGPEngine Description:
 * NGPEngine
 *
 * Comment 					        Revision	Date        Author
 * -----------------------------    --------    --------    -----------
 * Created							1.0		    2019-8-21   hulei@ixinwu.com
 *
 * ------------------------------------------------------------------------------*/

using Autofac;
using Autofac.Extensions.DependencyInjection;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using NGP.Framework.Core.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace NGP.Framework.Core.Infrastructure
{
    /// <summary>
    /// NGP引擎
    /// </summary>
    public class NGPEngine : IEngine
    {
        #region Properties

        /// <summary>
        /// 获取或设置服务提供者
        /// </summary>
        private IServiceProvider _serviceProvider { get; set; }

        #endregion

        #region Utilities

        /// <summary>
        /// Get IServiceProvider
        /// </summary>
        /// <returns>IServiceProvider</returns>
        protected IServiceProvider GetServiceProvider()
        {
            var accessor = ServiceProvider.GetService<IHttpContextAccessor>();
            var context = accessor.HttpContext;
            return context?.RequestServices ?? ServiceProvider;
        }

        /// <summary>
        /// 运行启动任务
        /// </summary>
        /// <param name="typeFinder">Type finder</param>
        protected virtual void RunStartupTasks(ITypeFinder typeFinder)
        {
            // 查找启动任务
            var startupTasks = typeFinder.FindClassesOfType<IStartupTask>();

            // 构建启动任务实例
            var instances = startupTasks
                .Select(startupTask => (IStartupTask)Activator.CreateInstance(startupTask))
                .OrderBy(startupTask => startupTask.Order);

            // 执行任务
            foreach (var task in instances)
                task.Execute();
        }

        /// <summary>
        /// 注册依赖项
        /// </summary>
        /// <param name="services"></param>
        /// <param name="typeFinder"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        protected virtual IServiceProvider RegisterDependencies(IServiceCollection services, ITypeFinder typeFinder, IConfiguration configuration)
        {
            var containerBuilder = new ContainerBuilder();
            
            // 注入引擎
            containerBuilder.RegisterInstance(this).As<IEngine>().SingleInstance();

            // 注入配置
            containerBuilder.RegisterInstance(configuration).As<IConfiguration>().SingleInstance();

            // 注入类型查找器
            containerBuilder.RegisterInstance(typeFinder).As<ITypeFinder>().SingleInstance();

            var dependencyRegistrars = typeFinder.FindClassesOfType<IDependencyRegistrar>();

            var instances = dependencyRegistrars
                .Select(dependencyRegistrar => (IDependencyRegistrar)Activator.CreateInstance(dependencyRegistrar))
                .OrderBy(dependencyRegistrar => dependencyRegistrar.Order);

            foreach (var dependencyRegistrar in instances)
                dependencyRegistrar.Register(containerBuilder, typeFinder);

            // 使用注册的服务描述符集填充Autofac容器构建器
            containerBuilder.Populate(services);

            _serviceProvider = new AutofacServiceProvider(containerBuilder.Build());

            return _serviceProvider;
        }

        /// <summary>
        /// 注册并配置AutoMapper
        /// </summary>
        /// <param name="services">Collection of service descriptors</param>
        /// <param name="typeFinder">Type finder</param>
        protected virtual void AddAutoMapper(IServiceCollection services, ITypeFinder typeFinder)
        {           
            var mapperConfigurations = typeFinder.FindClassesOfType<INGPMapperProfile>();

            var instances = mapperConfigurations
                .Select(mapperConfiguration => (INGPMapperProfile)Activator.CreateInstance(mapperConfiguration))
                .OrderBy(mapperConfiguration => mapperConfiguration.Order);

            var config = new MapperConfiguration(cfg =>
            {
                foreach (var instance in instances)
                {
                    cfg.AddProfile(instance.GetType());
                }
            });

            AutoMapperConfiguration.Init(config);
        }

        /// <summary>
        /// 初始化数据库
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <param name="typeFinder"></param>
        protected virtual void InitializeDatabase(ITypeFinder typeFinder, IConfiguration configuration)
        {
            // 执行初始化数据库脚本
            var dbConfigTypes = typeFinder.FindClassesOfType<IInitializeDataBase>();
            foreach (var dbConfigType in dbConfigTypes)
            {
                var dbConfig = Activator.CreateInstance(dbConfigType) as IInitializeDataBase;
                dbConfig.Excute(configuration);
                break;

            }
        }

        /// <summary>
        /// 当前domain解析
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        private Assembly CurrentDomain_AssemblyResolve(object sender, ResolveEventArgs args)
        {
            //check for assembly already loaded
            var assembly = AppDomain.CurrentDomain.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            if (assembly != null)
                return assembly;

            //get assembly from TypeFinder
            var tf = Resolve<ITypeFinder>();
            assembly = tf.GetAssemblies().FirstOrDefault(a => a.FullName == args.Name);
            return assembly;
        }

        #endregion

        #region Methods

        /// <summary>
        /// 配置服务
        /// </summary>
        /// <param name="services"></param>
        /// <param name="configuration"></param>
        /// <returns></returns>
        public IServiceProvider ConfigureServices(IServiceCollection services, IConfiguration configuration)
        {
            // 配置启动配置
            var typeFinder = new WebAppTypeFinder();
            Singleton<ITypeFinder>.Instance = typeFinder;
            var startupConfigurations = typeFinder.FindClassesOfType<INGPStartup>();

            var instances = startupConfigurations
                .Select(startup => (INGPStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
                instance.ConfigureServices(services, configuration);

            // 添加AutoMapper
            AddAutoMapper(services, typeFinder);

            // 初始化数据库
            InitializeDatabase(typeFinder,configuration);

            // 注册依赖
            RegisterDependencies(services, typeFinder, configuration);

            // 运行启动任务
            RunStartupTasks(typeFinder);

            // 添加组件加载事件
            AppDomain.CurrentDomain.AssemblyResolve += CurrentDomain_AssemblyResolve;

            return _serviceProvider;
        }

        /// <summary>
        /// 配置请求管道
        /// </summary>
        /// <param name="application"></param>
        public void ConfigureRequestPipeline(IApplicationBuilder application)
        {
            var typeFinder = Resolve<ITypeFinder>();
            var startupConfigurations = typeFinder.FindClassesOfType<INGPStartup>();

            var instances = startupConfigurations
                .Select(startup => (INGPStartup)Activator.CreateInstance(startup))
                .OrderBy(startup => startup.Order);

            foreach (var instance in instances)
                instance.Configure(application);
        }

        /// <summary>
        /// 解析依赖对象
        /// </summary>
        /// <typeparam name="T">Type of resolved service</typeparam>
        /// <returns>Resolved service</returns>
        public T Resolve<T>() where T : class
        {
            return (T)Resolve(typeof(T));
        }

        /// <summary>
        /// 解析依赖对象
        /// </summary>
        /// <param name="type">Type of resolved service</param>
        /// <returns>Resolved service</returns>
        public object Resolve(Type type)
        {
            return GetServiceProvider().GetService(type);
        }

        /// <summary>
        /// 解析依赖对象
        /// </summary>
        /// <typeparam name="T">Type of resolved services</typeparam>
        /// <returns>Collection of resolved services</returns>
        public virtual IEnumerable<T> ResolveAll<T>()
        {
            return (IEnumerable<T>)GetServiceProvider().GetServices(typeof(T));
        }

        /// <summary>
        /// 解析未注册的对象
        /// </summary>
        /// <param name="type">Type of service</param>
        /// <returns>Resolved service</returns>
        public virtual object ResolveUnregistered(Type type)
        {
            Exception innerException = null;
            foreach (var constructor in type.GetConstructors())
            {
                try
                {
                    //try to resolve constructor parameters
                    var parameters = constructor.GetParameters().Select(parameter =>
                    {
                        var service = Resolve(parameter.ParameterType);
                        if (service == null)
                            
                            throw new NGPException(StatusCode.DependencyError);
                        return service;
                    });

                    //all is ok, so create instance
                    return Activator.CreateInstance(type, parameters.ToArray());
                }
                catch (Exception ex)
                {
                    innerException = ex;
                }
            }

            throw new NGPException(StatusCode.NoConstructorError, innerException);
        }

        #endregion

        #region Properties

        /// <summary>
        /// Service provider
        /// </summary>
        public virtual IServiceProvider ServiceProvider => _serviceProvider;

        #endregion
    }
}
