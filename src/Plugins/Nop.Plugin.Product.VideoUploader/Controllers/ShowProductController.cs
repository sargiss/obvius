using System;
using System.Security.Cryptography.X509Certificates;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Controllers;
using Nop.Services.Seo;
using Nop.Services.Catalog;
using Nop.Services.Media;

namespace Nop.Plugin.Product.VideoUploader.Controllers
{
    using Services;

    public class ShowProductController: BasePluginController
    {
        private readonly ProductResolveService _service; 
        private readonly IUrlRecordService _urlService;
        private readonly IProductService _productService;
        private readonly IPictureService _pictureService;
        

        public ShowProductController(ProductResolveService service, 
            IUrlRecordService urlService,
            IProductService productService,
            IPictureService pictureService
        )
        {
            _service = service; 
            _urlService = urlService;
            _productService = productService;
            _pictureService = pictureService;
        }

        [HttpGet]
        public ActionResult RedirectToProduct([FromRoute]string videoShortLink, [FromQuery]int? videoTimeSec)
        {
            var product = _service.Find(videoShortLink, videoTimeSec);
            return Redirect(Url.RouteUrl("product", new { SeName = _urlService.GetSeName(product) }));
        }

        [HttpGet]
        public ActionResult GetProductsForStream()
        {
            var products = _productService.SearchProducts(0, 5);
            return Json(products.Select(x => {
                var pictires = _productService.GetProductPicturesByProductId(x.Id);
                var pictureUrl = pictires.Any() ? new Uri(_pictureService.GetPictureUrl(pictires[0].PictureId)).PathAndQuery: ""; 
                return new {
                    Id = x.Id,
                    Name = x.Name,
                    Url = Url.RouteUrl("product", new { SeName = _urlService.GetSeName(x) }),
                    ImageUrl = pictureUrl,
                    Price = x.Price
                };
            }));
        }
    }
}