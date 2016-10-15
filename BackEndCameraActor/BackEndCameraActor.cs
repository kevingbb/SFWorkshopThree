using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Runtime;
using Microsoft.ServiceFabric.Actors.Client;
using BackEndCameraActor.Interfaces;

using BackEndCameraActor.Common;

namespace BackEndCameraActor
{
    /// <remarks>
    /// This class represents an actor.
    /// Every ActorID maps to an instance of this class.
    /// The StatePersistence attribute determines persistence and replication of actor state:
    ///  - Persisted: State is written to disk and replicated.
    ///  - Volatile: State is kept in memory only and replicated.
    ///  - None: State is kept in memory only and not replicated.
    /// </remarks>
    [StatePersistence(StatePersistence.Persisted)]
    internal class BackEndCameraActor : Actor, IBackEndCameraActor
    {
        private static readonly string StatePropertyName = "CameraObject";

        /// <summary>
        /// Initializes a new instance of BackEndCameraActor
        /// </summary>
        /// <param name="actorService">The Microsoft.ServiceFabric.Actors.Runtime.ActorService that will host this actor instance.</param>
        /// <param name="actorId">The Microsoft.ServiceFabric.Actors.ActorId for this actor instance.</param>
        public BackEndCameraActor(ActorService actorService, ActorId actorId)
            : base(actorService, actorId)
        {
        }

        /// <summary>
        /// This method is called whenever an actor is activated.
        /// An actor is activated the first time any of its methods are invoked.
        /// </summary>
        protected override async Task OnActivateAsync()
        {
            ActorEventSource.Current.ActorMessage(this, "Actor activated.");

            // The StateManager is this actor's private state store.
            // Data stored in the StateManager will be replicated for high-availability for actors that use volatile or persisted state storage.
            // Any serializable object can be saved in the StateManager.
            // For more information, see https://aka.ms/servicefabricactorsstateserialization

            string location = "LOC" + this.Id.ToString();
            CameraObject newObject = new CameraObject(location, 0);

            //ActorEventSource.Current.ActorMessage(this, "StateCheck {0}", (await this.StateManager.ContainsStateAsync(StatePropertyName)).ToString());

            await this.StateManager.TryAddStateAsync<CameraObject>(StatePropertyName, newObject);
            await base.OnActivateAsync();
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public async Task<CameraObject> GetCameraAsync()
        {
            CameraObject camera = await this.StateManager.GetStateAsync<CameraObject>(StatePropertyName);

            return camera;
        }
    }
}
