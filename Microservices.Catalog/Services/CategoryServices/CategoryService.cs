using AutoMapper;
using Microservices.Catalog.Dtos.CategoryDtos;
using Microservices.Catalog.Models;
using Microservices.Catalog.Settings.Abstract;
using MicroServices.Shared.Dtos;
using MongoDB.Driver;

namespace Microservices.Catalog.Services.CategoryServices
{
    public class CategoryService : ICategoryService
    {
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper, IDataBaseSettings _dataBaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(_dataBaseSettings.DatabaseName);
            _categoryCollection = database.GetCollection<Category>(_dataBaseSettings.CategoryCollectionName);
        }

        public Task<Response<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategory)
        {
            throw new NotImplementedException();
        }

        public Task<Response<NoContent>> DeleteCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ResultCategoryDto>> GetCategoryByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<List<ResultCategoryDto>>> GetCategoryListAsync()
        {
            throw new NotImplementedException();
        }

        public Task<Response<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto updateCategory)
        {
            throw new NotImplementedException();
        }
    }
}
