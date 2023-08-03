using Microservices.Catalog.Dtos.CategoryDtos;
using MicroServices.Shared.Dtos;

namespace Microservices.Catalog.Services.CategoryServices
{
    public interface ICategoryService
    {
        Task<Response<List<ResultCategoryDto>>> GetCategoryListAsync();
        Task<Response<ResultCategoryDto>> GetCategoryByIdAsync(string id);
        Task<Response<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategory);
        Task<Response<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto updateCategory);
        Task<Response<NoContent>> DeleteCategoryAsync(string id);

    }
}
