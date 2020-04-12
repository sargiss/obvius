using System.Linq;
using System.IO;
using Nop.Core.Infrastructure;
using Nop.Data;
using Nop.Core;
using Nop.Web.Framework.Models.Extensions;
using Nop.Web.Framework.Models;

namespace Nop.Plugin.Product.VideoUploader.Services
{
    using Data;

    public class VideoService
    {
        private readonly INopFileProvider _fileProvider;
        private readonly IRepository<ProductVideo> _repository;

        public VideoService(INopFileProvider fileProvider, IRepository<ProductVideo> repository)
        {
            _fileProvider = fileProvider;
            _repository = repository;

            var videoDir = _fileProvider.GetAbsolutePath("videos");
            if (!Directory.Exists(videoDir))
            {
                Directory.CreateDirectory(videoDir);   
            }
        }

        public ProductVideo AddVideo(int productId, string fileName, BinaryReader videoData)
        {
            fileName = $"{productId}_{Path.GetFileName(fileName)}";
            var fileToSave = _fileProvider.GetAbsolutePath("videos", Path.GetRandomFileName());

            using(var file = File.Create(fileToSave))
            {
                var bufferSize = 5000000;

                byte[] buffer;
                do
                {
                    buffer = videoData.ReadBytes(bufferSize);
                    file.Write(buffer, 0, buffer.Length);
                }
                while(buffer.Length == bufferSize);
            }

            var res = new ProductVideo{
                ProductId = productId,
                FilePath = fileToSave,
                VideoName = fileName,
                Linked = false
            };

            _repository.Insert(res);

            return res;
        }

        public void UpdateVideoLink(ulong linkId, int secFrom, int secTo, string customName)
        {
            var pv = _repository.GetById(linkId);

            pv.FromSec = secFrom;
            pv.ToSec = secTo;
            if (!string.IsNullOrEmpty(customName))
            {
                pv.VideoName = customName;
            }
            if (string.IsNullOrEmpty(pv.ShortLink))
            {
                pv.ShortLink = Infrastructure.ShortVideoLinkHelper.GetVideoLink(linkId);
            } 
            pv.Linked = true;

            _repository.Update(pv);
        }

        public IPagedList<ProductVideo> GetVideos(int productId)
        {
            var res = _repository.Table.Where(x => x.ProductId == productId && x.Linked).ToList();
            return res.ToPagedList(new PictureRequestModel());
        }

        public void DeleteVideo(int videoId)
        {

        }

        class PictureRequestModel: IPagingRequestModel
        {
            public int Page => 0;
            public int PageSize => int.MaxValue;
        }
    }
}