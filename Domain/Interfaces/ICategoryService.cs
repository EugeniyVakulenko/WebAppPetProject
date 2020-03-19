using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Domain.DTO;

namespace Domain.Interfaces
{
    public interface ICategoryService
    {
        CategoryDTO GetCategoryById(int id);
        IEnumerable<CategoryDTO> GetAllCategories();
        Task<CategoryDTO> CreateCategory(CategoryDTO category);
        void UpdateCategory(CategoryDTO category);
        void DeleteCategory(int id);
      
    }
}
