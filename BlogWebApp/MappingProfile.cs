using AutoMapper;
using BlogWebApp.BLL.Models.Entities;
using Microsoft.Extensions.Hosting;
using System.Net;

namespace BlogWebApp
{
    /// <summary>
    /// Настройки маппинга всех сущностей приложения
    /// </summary>
    public class MappingProfile : Profile
    {
/// <summary>
/// В конструкторе настроим соответствие сущностей при маппинге
/// </summary>
        public MappingProfile()
{
     //здесь можно подключить маппинг автомаппера, но пока сопоставляем модели с вьюмоделями руками

        }
    }
}
