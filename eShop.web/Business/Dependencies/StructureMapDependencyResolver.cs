﻿using StructureMap;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using IContainer = StructureMap.IContainer;

namespace eShop.web.Business.Dependencies
{
    public class StructureMapDependencyResolver : IDependencyResolver
    {
        private readonly IContainer _container;

        public StructureMapDependencyResolver(IContainer container)
        {
            _container = container;
        }

        public object GetService(Type serviceType)
        {
            if (serviceType.IsInterface || serviceType.IsAbstract)
            {
                return GetInterfaceService(serviceType);
            }

            return GetConcreteService(serviceType);
        }

        public IEnumerable<object> GetServices(Type serviceType)
        {
            return _container.GetAllInstances(serviceType).Cast<object>();
        }

        private object GetConcreteService(Type serviceType)
        {
            try
            {
                return _container.GetInstance(serviceType);
            }
            catch (StructureMapException)
            {
                return null;
            }
        }

        private object GetInterfaceService(Type serviceType)
        {
            if (_container == null)
            {
                return null;
            }

            return _container.TryGetInstance(serviceType);
        }
    }
}