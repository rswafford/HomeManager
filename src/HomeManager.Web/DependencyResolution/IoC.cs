// --------------------------------------------------------------------------------------------------------------------
// <copyright file="IoC.cs" company="Web Advanced">
// Copyright 2012 Web Advanced (www.webadvanced.com)
// Licensed under the Apache License, Version 2.0 (the "License");
// you may not use this file except in compliance with the License.
// You may obtain a copy of the License at
//
// http://www.apache.org/licenses/LICENSE-2.0

// Unless required by applicable law or agreed to in writing, software
// distributed under the License is distributed on an "AS IS" BASIS,
// WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
// See the License for the specific language governing permissions and
// limitations under the License.
// </copyright>
// --------------------------------------------------------------------------------------------------------------------


using System.Data.Entity;
using System.Reflection;
using HomeManager.Domain.Entities;
using HomeManager.Domain.Entities.Core;
using HomeManager.Domain.Entities.Media;
using StructureMap;
namespace HomeManager.Web.DependencyResolution {
    public static class IoC {
        public static IContainer Initialize() {
            ObjectFactory.Initialize(x =>
                        {
                            x.Scan(scan =>
                            {
                                        scan.Assembly("HomeManager.Domain");
                                        scan.Assembly("HomeManager.Model");
                                        scan.TheCallingAssembly();
                                        scan.WithDefaultConventions();
  
                                    });
            //                x.For<IExample>().Use<Example>();
                            x.For<DbContext>().Use<EntitiesContext>();

                            x.For<IEntityRepository<User>>().Use<EntityRepository<User>>();
                            x.For<IEntityRepository<Role>>().Use<EntityRepository<Role>>();
                            x.For<IEntityRepository<UserInRole>>().Use < EntityRepository<UserInRole>>();

                            x.For<IEntityRepository<Movie>>().Use<EntityRepository<Movie>>();
                            x.For<IEntityRepository<MovieFormat>>().Use<EntityRepository<MovieFormat>>();
                            x.For<IEntityRepository<MovieGenre>>().Use<EntityRepository<MovieGenre>>();
                            x.For<IEntityRepository<MovieInGenre>>().Use<EntityRepository<MovieInGenre>>();

                            x.For<IEntityRepository<TvEpisode>>().Use<EntityRepository<TvEpisode>>();
                            x.For<IEntityRepository<TvShow>>().Use<EntityRepository<TvShow>>();

                            x.For<IEntityRepository<UserTvEpisode>>().Use<EntityRepository<UserTvEpisode>>();
                            x.For<IEntityRepository<UserMovie>>().Use<EntityRepository<UserMovie>>();

                        });
            return ObjectFactory.Container;
        }
    }
}