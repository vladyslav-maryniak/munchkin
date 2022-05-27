using MongoDB.Bson.Serialization;
using System.Reflection;

namespace Munchkin.Application.DbContext.MongoDb.Base
{
    public class MapRegistrar
    {
        public static void ApplyConfiguration<T>()
               where T : class
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap<T>();
            }
        }

        public static void ApplyConfiguration<T>(IBsonClassMapConfiguration<T> configuration)
            where T : class
        {
            if (!BsonClassMap.IsClassMapRegistered(typeof(T)))
            {
                BsonClassMap.RegisterClassMap<T>(configuration.Configure);
            }
        }

        public void ApplyMapConfigurationFromAssembly(Assembly assembly)
        {
            var applyEntityConfigurationMethod = typeof(MapRegistrar)
                .GetMethods()
                .Single(
                    e => e.Name == nameof(ApplyConfiguration)
                        && e.ContainsGenericParameters
                        && e.GetParameters().SingleOrDefault()?.ParameterType.GetGenericTypeDefinition()
                            == typeof(IBsonClassMapConfiguration<>));

            foreach (var type in assembly.GetTypes())
            {
                foreach (var @interface in type.GetInterfaces())
                {
                    if (!@interface.IsGenericType)
                    {
                        continue;
                    }

                    if (@interface.GetGenericTypeDefinition() == typeof(IBsonClassMapConfiguration<>))
                    {
                        var target = applyEntityConfigurationMethod.MakeGenericMethod(@interface.GenericTypeArguments[0]);
                        target.Invoke(this, new[] { Activator.CreateInstance(type) });
                    }
                }
            }
        }
    }
}
