using Common.Models.Storage;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Http;

namespace HikeService.Controllers
{
    public class UserController : ResourceController<UserDataEntity>
    {
        public string Get(string userName)
        {
            UserDataEntity entity = new UserDataEntity(userName);
            var result = _storageService.GetEntity(entity);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (result.Result != null)
            {
                return ((UserDataEntity)result.Result).ZipCode;
            }

            return "Add zip code";
        }

        public bool Post(string userName, [FromBody] InputData data)
        {
            UserDataEntity entity = new UserDataEntity(userName, data.Value);
            HttpContext.Current.Response.AddHeader("Access-Control-Allow-Origin", "*");

            if (IsValidZipCode(data.Value))
            {
                return _storageService.UpdateEntity(entity);
            }

            return false;
        }

        private bool IsValidZipCode(string zipCode)
        {
            string _usZipRegEx = @"^\d{5}(?:[-\s]\d{4})?$";
            return Regex.Match(zipCode, _usZipRegEx).Success;
        }
    }
}