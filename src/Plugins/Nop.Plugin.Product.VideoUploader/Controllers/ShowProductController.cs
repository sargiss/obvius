using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Services.Seo;

namespace Nop.Plugin.Product.VideoUploader.Controllers
{
    using Services;

    public class ShowProductController: BasePluginController
    {
        ProductResolveService _service; 
        IUrlRecordService _urlService;

        public ShowProductController(ProductResolveService service, IUrlRecordService urlService)
        {
            _service = service; 
            _urlService = urlService;
        }

        [HttpGet]
        public ActionResult RedirectToProduct([FromRoute]string videoShortLink, [FromQuery]int? videoTimeSec)
        {
            var product = _service.Find(videoShortLink, videoTimeSec);
            return Redirect(Url.RouteUrl("product", new { SeName = _urlService.GetSeName(product) }));
        }
    }
}