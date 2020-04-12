using Nop.Core;

namespace Nop.Plugin.Product.VideoUploader.Data
{
    public class ProductVideo: BaseEntity
    {
        public int FromSec {get;set;}
        public int ToSec {get;set;}
        public int ProductId {get;set;}
        public string VideoName{get;set;}
        public string FilePath {get;set;}
        public string ShortLink {get;set;}
        public bool Linked {get;set;}
    }
}