using MongoDB.Bson;
using MongoDB.Bson.Serialization;
using MongoDB.Bson.Serialization.Options;
using MongoDB.Bson.Serialization.Serializers;
using Munchkin.Application.DbContext.MongoDb.Base;
using Munchkin.Shared.Cards.Base.Doors;
using Munchkin.Shared.Models;

namespace Munchkin.Application.DbContext.MongoDb.Entities
{
    public class CombatFieldMapConfiguration : IBsonClassMapConfiguration<CombatField>
    {
        public void Configure(BsonClassMap<CombatField> cm)
        {
            cm.AutoMap();
            cm.MapMember(c => c.MonsterEnhancers)
              .SetSerializer(
                new DictionaryInterfaceImplementerSerializer<Dictionary<Guid, List<MonsterEnhancerCard>>>(
                  DictionaryRepresentation.ArrayOfDocuments));
        }
    }
}
