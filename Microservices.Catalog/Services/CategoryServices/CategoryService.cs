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

        public async Task<Response<CreateCategoryDto>> CreateCategoryAsync(CreateCategoryDto createCategory)
        {
           var value = _mapper.Map<Category>(createCategory);
            await _categoryCollection.InsertOneAsync(value);
            return Response<CreateCategoryDto>.Success(_mapper.Map<CreateCategoryDto>(value),200);

        }

        public Task<Response<NoContent>> DeleteCategoryAsync(string id)
        {
            throw new NotImplementedException();
        }

        public Task<Response<ResultCategoryDto>> GetCategoryByIdAsync(string id)
        {
            throw new NotImplementedException();
        }

        public async Task<Response<List<ResultCategoryDto>>> GetCategoryListAsync()
        {
            var values = await _categoryCollection.Find(x => true).ToListAsync();
            return  Response<List<ResultCategoryDto>>.Success(_mapper.Map<List<ResultCategoryDto>>(values) ,200);
        }


        public Task<Response<UpdateCategoryDto>> UpdateCategoryAsync(UpdateCategoryDto updateCategory)
        {
            throw new NotImplementedException();
        }
    }
}
