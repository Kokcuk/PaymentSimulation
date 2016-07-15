using System;
using System.Collections.Generic;

namespace PaymentSimulation
{
    public sealed class Locator
    {
        private static volatile Locator instance;
        private static object syncRoot = new Object();

        public Locator()
        {
            Services = new Dictionary<Type, object>();
        }

        public static Locator Instance
        {
            get
            {
                if (instance == null)
                {
                    lock (syncRoot)
                    {
                        if (instance == null)
                            instance = new Locator();
                    }
                }

                return instance;
            }
        }

        public Dictionary<Type, object> Services { get; set; }

        public void Register(object endpoint)
        {
            Services.Add(endpoint.GetType(), endpoint);
        }

        public void Register(Type objType, object endpoint)
        {
            Services.Add(objType, endpoint);
        }

        public T GetService<T>() where T : class
        {
            var service = Services[typeof (T)];
            return service as T;
        }
    }
}