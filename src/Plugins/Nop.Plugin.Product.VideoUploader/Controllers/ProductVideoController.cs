using System.Collections.Generic;
using System.Linq;
using System.IO;
using Nop.Web.Framework.Controllers;
using Nop.Web.Framework.Mvc.Filters;
using Nop.Web.Framework;
using Nop.Web.Framework.Models;
using Microsoft.AspNetCore.Mvc;
using Nop.Web.Framework.Models.Extensions;
using Nop.Core;

namespace Nop.Plugin.Product.VideoUploader.Controllers
{
    using Services;
    using Models;
    using Routing;

    public class ProductVideoTestController: BaseController
    {
        [HttpGet]
        public JsonResult Test()
        {
            return Json("Ok");
        }
    }

    [AuthorizeAdmin]
    [Area(AreaNames.Admin)]
    public class ProductVideoController: BasePluginController
    {
        readonly VideoService _videoService;

        public ProductVideoController(VideoService videoService)
        {
            _videoService = videoService;
        }


        [HttpGet, HttpPost]
        public JsonResult List([FromRoute]int productId)
        {
            var videos = _videoService.GetVideos(productId);
            var res = (new ProductVideoListModel()).PrepareToGrid( 
                new VideoSearchModel(), 
                videos,
                () => videos.Select(v => new ProductVideoModel{
                    Id = v.Id,
                    FromSec = v.FromSec,
                    ToSec = v.ToSec,
                    OverrideTitleAttribute = v.VideoName,
                    VideoUrl = Routes.GetVideoPath(v.ShortLink)
                })
            );

            return Json(res);
        }

        [HttpPost, HttpGet]                           
        [RequestFormLimits(MultipartBodyLengthLimit = 10737418240)]
        [RequestSizeLimit(10737418240)]
        public JsonResult UploadVideo([FromRoute]int productId, FineUpload fineUpload)
        {     
            if (productId == 0)
                throw new System.Exception("Invalid product");
                       // считываем переданный файл в массив байтов
            using (var binaryReader = new BinaryReader(fineUpload.InputStream))
            {
                var res = _videoService.AddVideo(productId, fineUpload.Filename, binaryReader);   
                return Json(new {success = true, linkId = res.Id});
            }
            
        }

        [HttpPost]
        public JsonResult UploadData(ProductVideoModel productVideoModel)
        {
            if (productVideoModel.Id == 0)
                throw new System.Exception("Invalid video Id");

            _videoService.UpdateVideoLink((ulong)productVideoModel.Id, 
                productVideoModel.FromSec, 
                productVideoModel.ToSec,
                productVideoModel.OverrideTitleAttribute);

            return Json(new {linkId = productVideoModel.Id});
        }

        class VideoSearchModel : BaseSearchModel
        {
            public VideoSearchModel()
            {
            }
        }

        /*class C : IPagedList<ProductVideoModel>
        {
            public object this[int index] { get => throw new System.NotImplementedException(); set => throw new System.NotImplementedException(); }

            public int PageIndex => 0;

            public int PageSize => 5;

            public int TotalCount => 0;

            public int TotalPages => 0;

            public bool HasPreviousPage => throw new System.NotImplementedException();

            public bool HasNextPage => throw new System.NotImplementedException();

            public int Count => 0;

            public bool IsReadOnly => false;

            public void Add(object item)
            {
                throw new System.NotImplementedException();
            }

            public void Clear()
            {
                throw new System.NotImplementedException();
            }

            public bool Contains(object item)
            {
                throw new System.NotImplementedException();
            }

            public void CopyTo(object[] array, int arrayIndex)
            {
                throw new System.NotImplementedException();
            }

            public IEnumerator<ProductVideoModel> GetEnumerator()
            {
                throw new System.NotImplementedException();
            }

            public int IndexOf(object item)
            {
                throw new System.NotImplementedException();
            }

            public void Insert(int index, object item)
            {
                throw new System.NotImplementedException();
            }

            public bool Remove(object item)
            {
                throw new System.NotImplementedException();
            }

            public void RemoveAt(int index)
            {
                throw new System.NotImplementedException();
            }

            IEnumerator IEnumerable.GetEnumerator()
            {
                throw new System.NotImplementedException();
            }
        }*/
    }
}