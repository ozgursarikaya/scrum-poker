using Microsoft.AspNetCore.Mvc;
using ScrumPoker.Common.Models;
using ScrumPoker.CustomException;
using ScrumPoker.Web.Extensions;
using System.Text;

namespace ScrumPoker.Web.Controllers
{
    public class BaseController : Controller
    {
        public void SetException(Exception ex)
        {
            StringBuilder stringBuilder = new();
            Type type = ex.GetType();

            if (type == typeof(ValidationException))
            {
                var error = (ValidationException)ex;
                foreach (var item in error.Errors)
                {
                    stringBuilder.Append(item + "<br/>");
                }
            }
            else if (type == typeof(BusinessException))
            {
                stringBuilder.Append(ex.Message);
            }
            else
            {
                stringBuilder.Append(ex.Message);
            }

            SetUiMessage(false, stringBuilder.ToString());
        }

        public void SetUiMessage(bool isOk, string message)
        {
            if (isOk)
                TempData.Put("UiMessage", new UiMessageModel(message, "success"));
            else
                TempData.Put("UiMessage", new UiMessageModel(message, "error"));
        }

        public void SetUiMessage(bool isOk)
        {
            if (isOk)
                TempData.Put("UiMessage", new UiMessageModel("İşleminiz başarılı bir şekilde gerçekleştirildi.", "success"));
            else
                TempData.Put("UiMessage", new UiMessageModel("İşleminiz gerçekleştirilirken hata oluştu", "error"));
        }

        public AjaxResponseModel SetAjaxException(Exception ex)
        {
            StringBuilder text = new();
            Type type = ex.GetType();

            if (type == typeof(ValidationException))
            {
                var error = (ValidationException)ex;
                foreach (var item in error.Errors)
                    text.Append(item + "<br/>");
            }
            else if (type == typeof(BusinessException))
                text.Append(ex.Message);
            else
                text.Append(ex.Message);

            return new AjaxResponseModel()
            {
                IsSuccess = false,
                Error = text.ToString(),
                Data = text.ToString(),
            };
        }
    }
}
