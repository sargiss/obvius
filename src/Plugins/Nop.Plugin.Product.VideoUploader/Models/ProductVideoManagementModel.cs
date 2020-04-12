namespace Nop.Plugin.Product.VideoUploader.Models
{
    public class ProductVideoManagementModel
    {
        public ProductVideoManagementModel()
        {
            AddVideoModel = new ProductVideoModel();
        }

        public int VideoListPageSize => 5;

        public string AvailablePageSizes => "5,10";
        
        public int ProductId {get;set;}

        public ProductVideoModel AddVideoModel {get;set;}
    }
}