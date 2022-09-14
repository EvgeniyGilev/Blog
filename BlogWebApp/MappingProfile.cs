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
