using System.ComponentModel.DataAnnotations;
using Nop.Web.Framework.Models;
using Nop.Web.Framework.Mvc.ModelBinding;

namespace Nop.Plugin.Product.VideoUploader.Models
{
    public class ProductVideoModel: BaseNopEntityModel
    {
        //[UIHint("Picture")]
        //[NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]

        public int FromSec {get;set;}

        public int ToSec {get;set;}

        //[NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.Picture")]
        public string VideoUrl { get; set; }

        //[NopResourceDisplayName("Admin.Catalog.Products.Pictures.Fields.OverrideTitleAttribute")]
        public string OverrideTitleAttribute { get; set; }
    }
}