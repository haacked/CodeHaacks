using System.Linq;
using System.ServiceModel;
using System.ServiceModel.Web;

namespace WebApiHaack.DemoWeb.Api
{
    [ServiceContract]
    public class CommentsApi
    {
        [WebGet]
        public IQueryable<Comment> Get()
        {
            return new[] { new Comment 
        { 
            Title = "This is neato", 
            Body = "Ok, not as neat as I originally thought." } 
        }.AsQueryable();
        }

        [WebGet(UriTemplate = "auth"), RequireAuthorization]
        public IQueryable<Comment> GetAuth()
        {
            return new[] { new Comment 
        { 
            Title = "This is secured neato", 
            Body = "Ok, a bit neater than I originally thought." } 
        }.AsQueryable();
        }
    }
}