using EventStore.ClientAPI;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using V1.Eventos;
using V1.Snapshots;

namespace V1.Repositorios
{
    public class RepositorioDeEventos : IRepositorioDeEventos
    {
        private const string EventType = "EventType";
        private readonly IEventStoreConnection conexao;

        public RepositorioDeEventos(IEventStoreConnection conexao)
        {
            this.conexao = conexao;
        }

        public void Salvar(Guid entidadeId, IEnumerable<Evento> eventos, int versaoEsperada)
        {
            conexao.AppendToStreamAsync(
                entidadeId.ToString(),
                versaoEsperada == 0 ? ExpectedVersion.Any : versaoEsperada,
                eventos.Select(e => Serialize(e, entidadeId))).Wait();
        }

        public IEnumerable<Evento> BuscarEventosPor(Guid entidadeId, int inicio)
        {
            var eventos = conexao.ReadStreamEventsForwardAsync(entidadeId.ToString(), inicio, 200, false).Result;
            return eventos.Events.Select(e => (Evento)Deserialize(e));
        }

        private EventData Serialize(object @event, Guid id)
        {
            var type = @event.GetType();

            var headers = new Dictionary<string, object>
            {
                { EventType, type.AssemblyQualifiedName }
            };

            var data = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(@event));
            var metadata = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(headers));

            return new EventData(id, type.Name, true, data, metadata);
        }

        private object Deserialize(ResolvedEvent @event)
        {
            var metadata = @event.OriginalEvent.Metadata;
            var data = @event.OriginalEvent.Data;
            var type = JObject.Parse(Encoding.UTF8.GetString(metadata)).Property(EventType).Value;

            return JsonConvert.DeserializeObject(Encoding.UTF8.GetString(data), Type.GetType((string)type));
        }

        #region Snapshots

        public void Salvar(Guid entidadeId, Snapshot snapshot)
        {
            conexao.AppendToStreamAsync(
                string.Format("{0}-ss", entidadeId), 
                ExpectedVersion.Any,
                Serialize(snapshot, entidadeId)).Wait();
        }

        public T BuscarSnapshotPor<T>(Guid entidadeId) where T : Snapshot
        {
            var stream = string.Format("{0}-ss", entidadeId);
            var ev = conexao.ReadStreamEventsBackwardAsync(stream, StreamPosition.End, 1, false).Result;

            return ev.Events.Any() ? (T)Deserialize(ev.Events.Single()) : null;
        }

        #endregion
    }
}