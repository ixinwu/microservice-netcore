// Copyright (c) Brock Allen & Dominick Baier. All rights reserved.
// Licensed under the Apache License, Version 2.0. See LICENSE in the project root for license information.


using IdentityServer4.Extensions;
using IdentityServer4.Models;
using IdentityServer4.Services;
using Microsoft.Extensions.Logging;
using NGP.Framework.Core;
using NGP.Framework.Core.Data;
using System.Linq;
using System.Threading.Tasks;

namespace NGP.Identity.Api.IdentityExtensions
{
    /// <summary>
    /// Profile service for test users
    /// </summary>
    /// <seealso cref="IdentityServer4.Services.IProfileService" />
    public class NGPUserProfileService : IProfileService
    {
        /// <summary>
        /// adoRepository
        /// </summary>
        private readonly IAdoRepository _adoRepository;

        /// <summary>
        /// Initializes a new instance of the <see cref="NGPUserProfileService"/> class.
        /// </summary>
        /// <param name="adoRepository"></param>
        public NGPUserProfileService(IAdoRepository adoRepository)
        {
            _adoRepository = adoRepository;
        }

        /// <summary>
        /// This method is called whenever claims about the user are requested (e.g. during token creation or via the userinfo endpoint)
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task GetProfileDataAsync(ProfileDataRequestContext context)
        {
            //depending on the scope accessing the user data.
            var claims = context.Subject.Claims.ToList();

            //set issued claims to return
            context.IssuedClaims = claims.ToList();

            return Task.CompletedTask;
        }

        /// <summary>
        /// This method gets called whenever identity server needs to determine if the user is valid or active (e.g. if the user's account has been deactivated since they logged in).
        /// (e.g. during token issuance or validation).
        /// </summary>
        /// <param name="context">The context.</param>
        /// <returns></returns>
        public virtual Task IsActiveAsync(IsActiveContext context)
        {
            // TODO:数据库获取subject
            context.IsActive = true;

            return Task.CompletedTask;
        }
    }
}