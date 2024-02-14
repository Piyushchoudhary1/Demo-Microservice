using Mango.Web.Models;
using Mango.Web.Models.Dto;

namespace Mango.Web.Service.IService
{
    public interface IBaseService : IDisposable
    {

        Task<ResponseDto?> SendAsync(RequestDto apiRequest, bool withBearer = true);
    }
}
