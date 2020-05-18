using LM.Domain.UnitOfWork;
using LM.Infra.UnitOfWork;
using LM.Responses;
using LM.VUTTR.Api.Domain;
using LM.VUTTR.Api.Domain.Tools.Commands;
using LM.VUTTR.Api.Domain.Tools.Commands.Handlers;
using LM.VUTTR.Api.Domain.Tools.Commands.Handlers.Contracts;
using LM.VUTTR.Api.Domain.Tools.Repository.Contracts;
using LM.VUTTR.Api.Domain.Users.Repository.Contracts;
using LM.VUTTR.Api.Dtos.Tools;
using LM.VUTTR.Api.Infra;
using LM.VUTTR.Api.Infra.Context;
using LM.VUTTR.Api.Infra.Repositories;
using LM.VUTTR.Api.Queries;
using LM.VUTTR.Api.Queries.Contratcs;
using LM.VUTTR.Api.Shared;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Collections.Generic;
using System.Text;

namespace LM.VUTTR.Api.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static void AddJWTAuthentication(this IServiceCollection services, string key)
        {
            services.AddAuthentication(x =>
            {
                x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
            })
            .AddJwtBearer(x =>
            {
                x.RequireHttpsMetadata = false;
                x.SaveToken = true;
                x.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuerSigningKey = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.ASCII.GetBytes(key)),
                    ValidateIssuer = false,
                    ValidateAudience = false
                };
            });
        }

        public static void ConfigureDependencyInjection(this IServiceCollection services)
        {
            services.AddHttpContextAccessor();

            services.AddScoped<IUnitOfWork, UnitOfWork<VUTTRContext>>();

            services.AddTransient<IUserRepository, UserRepository>();
            services.AddTransient<IToolRepository, ToolRepository>();

            services.AddTransient<IUser, Users>();

            services.AddTransient<IGetToolQueryHandler, GetToolQueryHandler>();
            services.AddTransient<IGetToolsQueryHandler, GetToolsQueryHandler>();
            services.AddTransient<IGetTagsQueryHandler, GetTagsQueryHandler>();

            services.AddTransient<ICreateToolCommandHandle, CreateToolCommandHandle>();
            services.AddTransient<IUpdateToolCommandHandle, UpdateToolCommandHandle>();
            services.AddTransient<IRemoveToolCommandHandle, RemoveToolCommandHandle>();
        }
    }
}