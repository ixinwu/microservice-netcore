// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4;
using IdentityServer4.Models;
using System.Collections.Generic;

namespace NGP.Identity.Api.Configs
{
    /// <summary>
    /// 一开始的数据初始化
    /// </summary>
    public static class IdentityConfig
    {
        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new IdentityResource[]
            {
                new IdentityResources.OpenId(),
                new IdentityResources.Profile()
            };
        }

        public static IEnumerable<ApiResource> GetApis()
        {
            return new List<ApiResource>
            {
                new ApiResource("debt_api", "债务基础api"),
                new ApiResource("ngp_analysis", "ngp解析api"),
                // 添加认证服务的扩展api
                new ApiResource(IdentityServerConstants.LocalApi.ScopeName)
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    // 三方客户端
                    ClientId = "thread_client",
                    Description = "银行对接api采用此授权",
                    // 没有用户使用client / secret进行验证
                    AllowedGrantTypes = GrantTypes.ClientCredentials,
                    ClientSecrets =
                    {
                        // 每个客户端scret应该不一致
                        new Secret("secret".Sha256())
                    },
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    // 客户端访问的接口范围
                    AllowedScopes = { "debt_api", "ngp_analysis" },
                },
                new Client
                {
                    // 债务api客户端
                    ClientId = "debt_api_client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                    ClientSecrets =
                    {
                        new Secret("debt_secret".Sha256())
                    },
                    AlwaysSendClientClaims = true,
                    AlwaysIncludeUserClaimsInIdToken = true,
                    AllowedScopes = { "debt_api",
                        "ngp_analysis",
                        IdentityServerConstants.LocalApi.ScopeName},
                    AllowOfflineAccess = true
                }
            };
        }
    }
}