using AutoMapper;
using FreeCourse.Services.Catolog.Dtos;
using FreeCourse.Services.Catolog.Models;
using FreeCourse.Services.Catolog.Settings;
using FreeCourse.Shared.Dtos;
using MongoDB.Driver;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FreeCourse.Services.Catolog.Services
{
    public class CategoryService:ICategoryService
    {
        private readonly IMongoCollection<Category> _CategoryCollection;
        private readonly IMapper _mapper;

        public CategoryService(IMapper mapper,IDatabaseSettings databaseSettings)
        {
            var clinet = new MongoClient(databaseSettings.ConnectionString);
            var database = clinet.GetDatabase(databaseSettings.DatabaseName);

            _CategoryCollection = database.GetCollection<Category>(databaseSettings.CategoryCollectionName);
            _mapper = mapper; 
        }

        public async Task<Response<List<CategoryDto>>>GetAllAsync()
        {
            var categories = await _CategoryCollection.Find(category => true).ToListAsync();
            return Response<List<CategoryDto>>.Success(_mapper.Map<List<CategoryDto>>(categories), 200);
        }
        public async Task<Response<CategoryDto>> CreateAsync(Category category)
        {
            await _CategoryCollection.InsertOneAsync(category);
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category), 200);
        }
        public async Task<Response<CategoryDto>>GetByIdAsync(string id)
        {
            var category = await _CategoryCollection.Find<Category>(x => x.Id == id).FirstOrDefaultAsync();
            if(category==null)
            {
                return Response<CategoryDto>.Fail("Category not found", 404);
            }
            return Response<CategoryDto>.Success(_mapper.Map<CategoryDto>(category),200);
        }

    }
}
