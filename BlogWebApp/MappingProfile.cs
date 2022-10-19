// This is an independent project of an individual developer. Dear PVS-Studio, please check it.
// PVS-Studio Static Code Analyzer for C, C++, C#, and Java: https://pvs-studio.com
using AutoMapper;

namespace BlogWebApp
{
    /// <summary>
    /// Настройки маппинга всех сущностей приложения.
    /// </summary>
    public class MappingProfile : Profile
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MappingProfile"/> class.
        /// В конструкторе настроим соответствие сущностей при маппинге.
        /// </summary>
        public MappingProfile()
        {
            // здесь можно подключить маппинг автомаппера, но пока сопоставляем модели с вьюмоделями руками.
        }
    }
}
