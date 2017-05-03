using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

using System.Fabric;
using BackEndCameraActor.Interfaces;
using BackEndCameraActor.Common;
using Microsoft.ServiceFabric.Actors;
using Microsoft.ServiceFabric.Actors.Client;
using Microsoft.ServiceFabric.Services.Communication.Runtime;
using Microsoft.ServiceFabric.Services.Runtime;
using Newtonsoft.Json;

namespace FrontEndWebServer.Controllers
{
    [Route("api/[controller]")]
    public class CamerasController : Controller
    {
        private static readonly Uri backEndCameraActorServiceUri;

        static CamerasController()
        {
            backEndCameraActorServiceUri = new Uri(FabricRuntime.GetActivationContext().ApplicationName + "/BackEndCameraActorService");
        }

        // GET api/cameras
        [HttpGet]
        public async Task<IEnumerable<string>> Get()
        {
            return new string[] { "No Camera Identifier Provided." };
        }

        // GET api/cameras/5
        [HttpGet("{id}")]
        public async Task<IEnumerable<string>> Get(long id)
        {
            // Note that even this GET creates an Actor based on the id
            // passed in because of the OnActivateAsync() method. The
            // OnActivateAsync() method gets triggered the first time
            // any of the Actor methods are invoked. In this case, the
            // GetCameraAsync() method is called below. This means the
            // OnActivateSync() method gets triggered, which creates
            // the actor before the execution of GetCameraAsync.
            // This is something you need to watch out for.
            CameraObject camera = null;
            ActorId actorId = new ActorId(id);
            IBackEndCameraActor cameraActor = ActorProxy.Create<IBackEndCameraActor>(actorId, backEndCameraActorServiceUri);

            try
            {
                camera = await cameraActor.GetCameraAsync();
                System.Diagnostics.Trace.WriteLine("Getting Camera {0}.", actorId.ToString());
            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Error: {0}", exc.Message));
            }

            //return new string[] { JsonConvert.SerializeObject(camera) };
            return new string[] { camera.Location };
        }

        // POST api/cameras
        [HttpPost]
        public async Task<IActionResult> Post()
        {
            CameraObject camera = null;
            ActorId actorId = ActorId.CreateRandom();
            IBackEndCameraActor cameraActor = ActorProxy.Create<IBackEndCameraActor>(actorId, backEndCameraActorServiceUri);

            try
            {
                camera = await cameraActor.GetCameraAsync();
                System.Diagnostics.Trace.WriteLine("Getting Camera {0}.", actorId.ToString());
            }
            catch (Exception exc)
            {
                System.Diagnostics.Trace.WriteLine(String.Format("Error: {0}", exc.Message));
            }

            //return new string[] { JsonConvert.SerializeObject(camera) };
            return this.Ok(String.Format("Camera ID: {0}, Location: {1}, Status {2}", actorId, camera.Location, camera.Status.ToString()));
        }

        // PUT api/cameras/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/cameras/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
