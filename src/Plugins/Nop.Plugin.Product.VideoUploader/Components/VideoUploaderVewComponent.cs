using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Components;
using Nop.Web.Areas.Admin.Models.Catalog;

namespace Nop.Plugin.Product.VideoUploader
{
    using Models;

    [ViewComponent(Name = "VideoUploaderWidget")]
    public class VideoUploaderVewComponent: NopViewComponent
    {
        public IViewComponentResult Invoke(string widgetZone, object additionalData = null)
        {
            var productModel = (ProductModel)additionalData;
            return View("~/Plugins/Product.VideoUploader/Views/VideoUploaderWidget.cshtml", 
                new ProductVideoManagementModel{ ProductId = productModel.Id });
        }
    }
}