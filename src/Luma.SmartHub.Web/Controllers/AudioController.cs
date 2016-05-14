#if DNX451
using System.Linq;
using Luma.SmartHub.Audio;
using Microsoft.AspNet.Mvc;

namespace Luma.SmartHub.Web.Controllers
{
    public class AudioController : Controller
    {
        private readonly IAudioHub _audioHub;

        public AudioController(IAudioHub audioHub)
        {
            _audioHub = audioHub;
        }

        [HttpGet]
        public ActionResult Index()
        {
            ViewBag.Devices = _audioHub.Devices;

            return View();
        }

        [HttpPost]
        public ActionResult Index(string url, string deviceId)
        {
            var device = _audioHub.Devices.Single(c => c.Id == deviceId);

            _audioHub.Play(url, device);

            return Index();
        }
    }
}
#endif