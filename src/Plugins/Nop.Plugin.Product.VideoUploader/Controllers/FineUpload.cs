using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.IO;
using System.Threading.Tasks;

namespace Nop.Plugin.Product.VideoUploader.Controllers
{
    [ModelBinder(typeof(ModelBinder))]
    public class FineUpload
    {
        public string Filename { get; set; }
        public Stream InputStream { get; set; }

        public void SaveAs(string destination, bool overwrite = false, bool autoCreateDirectory = true)
        {
            if (autoCreateDirectory)
            {
                var directory = new FileInfo(destination).Directory;
                if (directory != null) directory.Create();
            }

            using (var file = new FileStream(destination, overwrite ? FileMode.Create : FileMode.CreateNew))
                InputStream.CopyTo(file);
        }

        public class ModelBinder : IModelBinder
        {
            public object BindModel(ModelBindingContext bindingContext)
            {
                var request = bindingContext.HttpContext.Request;
                var formUpload = request.Form.Files.Count > 0;

                // find filename
                var xFileName = request.Headers["X-File-Name"];
                var qqFile = request.Form["qqfile"];
                var formFilename = formUpload ? request.Form.Files[0].FileName : null;

                if (!string.IsNullOrEmpty(qqFile))
                    formFilename = qqFile;
                if (!string.IsNullOrEmpty(xFileName))
                    formFilename = xFileName;    

                var upload = new FineUpload
                {
                    Filename = formFilename,
                    InputStream = formUpload ? request.Form.Files[0].OpenReadStream() : request.Body
                };

                return upload;
            }

            public Task BindModelAsync(ModelBindingContext bindingContext)
            {
                bindingContext.Result = ModelBindingResult.Success(BindModel(bindingContext));
                return Task.CompletedTask;
            }
        }

    }
}