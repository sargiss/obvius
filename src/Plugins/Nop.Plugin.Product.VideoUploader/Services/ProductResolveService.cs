using System.Linq;
using NopProduct = Nop.Core.Domain.Catalog.Product;
using Nop.Data;

namespace Nop.Plugin.Product.VideoUploader.Services
{
    using Data;

    public class ProductResolveService
    {
        private readonly IRepository<ProductVideo> _videoRepository;
        private readonly IRepository<NopProduct> _productRepository;

        public ProductResolveService(IRepository<ProductVideo> videoRepository, 
            IRepository<NopProduct> productRepository)
        {
            _videoRepository = videoRepository;
            _productRepository = productRepository;
        }

        public NopProduct Find(string shortVideoLink, int? videoStreamPositionSec)
        {
            return shortVideoLink == "any" ? FindAny():
                FindLinked(shortVideoLink, videoStreamPositionSec);
        }

        private NopProduct FindAny()
        {
            return _videoRepository.Table
                .Join(_productRepository.Table.Where(x => !x.Deleted), 
                    pv => pv.ProductId, 
                    p => p.Id, 
                    (pv, p) => p)
                .FirstOrDefault();
        }

        private NopProduct FindLinked(string shortVideoLink, int? videoStreamPositionSec)
        {
            var videoLinks = _videoRepository.Table
                .Where(x => x.ShortLink == shortVideoLink)
                .OrderBy(x => x.FromSec).ToList();

            ProductVideo productVideo = videoLinks.FirstOrDefault();
            if (productVideo != null && videoStreamPositionSec.HasValue)
            {
                productVideo = videoLinks.FirstOrDefault(x => x.FromSec <= videoStreamPositionSec.Value 
                    && x.ToSec >= videoStreamPositionSec.Value);
            }

            if (productVideo == null)
            {
                return null;
            }

            return _productRepository.GetById(productVideo.ProductId);
        }
    }
}