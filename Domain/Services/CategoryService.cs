using System;
using System.Collections.Generic;
using System.Text;
using Domain.Interfaces;
using Infrastructure.Interfaces;
using AutoMapper;
using Domain.DTO;
using System.Threading.Tasks;
using Infrastructure.Entities;

namespace Domain.Services
{
    public class CategoryService : ICategoryService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<CategoryDTO> CreateCategory(CategoryDTO categorydto)
        {
            if (categorydto == null) throw new ArgumentNullException(nameof(categorydto),"Failed to upload to database");
            var category = _mapper.Map<Category>(categorydto);
            _unitOfWork.Categories.Add(category);
            await _unitOfWork.SaveAsync();
            var categoryEntity = _unitOfWork.Categories.Find(c => c.CategoryName == category.CategoryName);
            if (categoryEntity == null) throw new ArgumentNullException(nameof(categoryEntity), "Failed to load from database");
            return _mapper.Map<CategoryDTO>(categoryEntity);
        }

        public void DeleteCategory(int id)
        {
            var categoryEntity = _unitOfWork.Categories.GetById(id);
            if (categoryEntity == null) throw new ArgumentNullException(nameof(categoryEntity), "Category not found");
            _unitOfWork.Categories.Delete(categoryEntity);
            _unitOfWork.Save();
        }

        public IEnumerable<CategoryDTO> GetAllCategories()
        {
            var categories = _unitOfWork.Categories.GetAllWithProducts();
            return _mapper.Map<IEnumerable<CategoryDTO>>(categories);
        }

        public CategoryDTO GetCategoryById(int id)
        {
            var category = _unitOfWork.Categories.GetByIdWithProducts(id);
            if (category == null) throw new ArgumentNullException(nameof(category), "Category not found");
            return _mapper.Map<CategoryDTO>(category);
        }

        public void UpdateCategory(CategoryDTO category)
        {
            var categoryEntity = _mapper.Map<Category>(category);
            if (categoryEntity == null) throw new ArgumentNullException(nameof(categoryEntity));
            _unitOfWork.Categories.Update(categoryEntity);
            _unitOfWork.Save();
        }
    }
}
