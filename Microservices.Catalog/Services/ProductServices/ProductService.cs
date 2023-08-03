using AutoMapper;
using Microservices.Catalog.Dtos.CategoryDtos;
using Microservices.Catalog.Dtos.ProductDto;
using Microservices.Catalog.Models;
using Microservices.Catalog.Settings.Abstract;
using MicroServices.Shared.Dtos;
using MongoDB.Driver;

namespace Microservices.Catalog.Services.ProductServices
{
    public class ProductService : IProductService
    {
        private readonly IMongoCollection<Products> _productCollection;
        private readonly IMongoCollection<Category> _categoryCollection;
        private readonly IMapper _mapper;

        public ProductService(IMapper mapper, IDataBaseSettings _dataBaseSettings)
        {
            _mapper = mapper;
            var client = new MongoClient(_dataBaseSettings.ConnectionString);
            var database = client.GetDatabase(_dataBaseSettings.DatabaseName);
            _productCollection = database.GetCollection<Products>(_dataBaseSettings.ProductCollectionName);
            _categoryCollection = database.GetCollection<Category>(_dataBaseSettings.CategoryCollectionName);
        }

        public async Task<Response<CreateProductDto>> CreateProductAsync(CreateProductDto createProductDto)
        {
            var value = _mapper.Map<Products>(createProductDto);
            await _productCollection.InsertOneAsync(value);
            return Response<CreateProductDto>.Success(_mapper.Map<CreateProductDto>(value),200);
        }

        public  async Task<Response<NoContent>> DeleteProductAsync(string id)
        {
            var value = await _productCollection.DeleteOneAsync(id);
            if (value.DeletedCount > 0)
            {
                return Response<NoContent>.Success(204);
            }
            else
            {
                return Response<NoContent>.Fail("silinicek data yok ", 400);
            }
        }

        public async Task<Response<ResultProductDto>> GetProductByIdAsync(string id)
        {
            var value = await _productCollection.Find<Products>(x => x.ProductID == id).FirstOrDefaultAsync();
            if (value == null)
            {
                return Response<ResultProductDto>.Fail("böyle ıd bulunmadı", 404);
            }
            else
            {
                return Response<ResultProductDto>.Success(_mapper.Map<ResultProductDto>(value), 200);
            }
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListAsync()
        {
            var value = await _productCollection.Find(x => true).ToListAsync();
            return Response<List<ResultProductDto>>.Success(_mapper.Map<List<ResultProductDto>>(value), 200);
        }

        public async Task<Response<List<ResultProductDto>>> GetProductListCategoryAsync()
        {
            var value = await _productCollection.Find(x => true).ToListAsync();
            if (value.Any()) 
            {
                foreach (var item in value)
                { 
                    item.Category=await _categoryCollection.Find<Category> (x=> x.CategoryID== item.CategoryID).FirstOrDefaultAsync();
                }
            }
            else 
            {
                value = new List<Products> ();
            }
            return Response<List<ResultProductDto>>.Success
                (_mapper.Map<List<ResultProductDto>>(value), 200);
        }

        public async Task<Response<UpdateProductDto>> UpdateProductAsync(UpdateProductDto updateProductDto)
        {
            var values = _mapper.Map<Products>(updateProductDto);
            var result = await _productCollection.FindOneAndReplaceAsync(x => x.ProductID == updateProductDto.ProductID, values);
            if (result == null)
            {
                return Response<UpdateProductDto>.Fail("Güncellenecek veri bulunumadı", 400);
            }
            else
            {
                return Response<UpdateProductDto>.Success(204);
            }
        }
    }
}
