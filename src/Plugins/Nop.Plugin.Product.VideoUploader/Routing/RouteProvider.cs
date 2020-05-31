using Microsoft.AspNetCore.Routing;
using Nop.Web.Framework.Mvc.Routing;
using Nop.Web.Framework;
using Microsoft.AspNetCore.Builder;

namespace Nop.Plugin.Product.VideoUploader.Routing
{
    using Controllers;

    public class RouteProvider : IRouteProvider
    {
        public int Priority => 666;

        public void RegisterRoutes(IEndpointRouteBuilder endpointRouteBuilder)
        {
            endpointRouteBuilder.MapControllerRoute(
                "VideoUploaderPlugin_UploadVideo",
                "Plugin/ProductVideo/uploadVideo/{productId}",
                new { controller = "ProductVideo", action = nameof(ProductVideoController.UploadVideo), area = AreaNames.Admin});

            endpointRouteBuilder.MapControllerRoute(
                "VideoUploaderPlugin_UploadData",
                "Plugin/ProductVideo/uploadData",
                new { controller = "ProductVideo", action = nameof(ProductVideoController.UploadData), area = AreaNames.Admin });

            endpointRouteBuilder.MapControllerRoute(
                "VideoUploaderPlugin_GetList",
                "Plugin/ProductVideo/list/{productId}",
                new { controller = "ProductVideo", action = nameof(ProductVideoController.List), area = AreaNames.Admin });

            endpointRouteBuilder.MapControllerRoute(
                "VideoUploaderPlugin_ShowProduct",
                "videos/{videoShortLink}",
                new { controller = "ShowProduct", action = nameof(ShowProductController.RedirectToProduct) });

            endpointRouteBuilder.MapControllerRoute(
                "VideoUploaderPlugin_ShowProduct",
                "stream/products",
                new { controller = "ShowProduct", action = nameof(ShowProductController.GetProductsForStream) });


            endpointRouteBuilder.MapControllerRoute(
                "VideoUploaderPlugin_Test",
                "Plugin/ProductVideo/test",
                new { controller = "ProductTestVideo", action = nameof(ProductVideoTestController.Test) });
        }
    }
}