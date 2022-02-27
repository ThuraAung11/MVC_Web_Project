using DTO;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace DAL
{
    public class CategoryDAO : PostContext
    {
        public int AddCategory(Category category)
        {
            try
            {
                db.Categories.Add(category);
                db.SaveChanges();
                return category.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public List<CategoryDTO> GetCategory()
        {
            List<CategoryDTO> ls = new List<CategoryDTO>();
            List<Category> category = db.Categories.Where(x=>x.isDeleted==false).OrderBy(x=>x.AddDate).ToList();
            foreach (var item in category)
            {
                CategoryDTO dto = new CategoryDTO() 
                {
                    ID=item.ID,
                    CategoryName=item.CategoryName
                };
                ls.Add(dto);
            }
            return ls;
        }

        public static IEnumerable<SelectListItem> GetCategoryForDropDown()
        {
            IEnumerable<SelectListItem> categoryList = db.Categories.Where(x=>x.isDeleted==false).OrderBy(x=>x.AddDate).Select(x=>new SelectListItem() 
            {
                Text=x.CategoryName,
                Value=SqlFunctions.StringConvert((double)x.ID)
            });
            return categoryList;
        }

        public List<Post> DeleteCategory(int ID)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == ID);
                category.isDeleted = true;
                category.LastUpdateDate = DateTime.Now;
                category.DeleteDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
                List<Post> pList = db.Posts.Where(x => x.CategoryID == ID).ToList();
                return pList;
            }
            catch (Exception ex)
            {

                throw ex;
            }
          
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            Category category = db.Categories.First(x=>x.ID==ID);
            CategoryDTO dto = new CategoryDTO() 
            {
                ID = category.ID,
                CategoryName=category.CategoryName
            };
            return dto;
        }

        public void UpdateCategory(CategoryDTO model)
        {
            try
            {
                Category category = db.Categories.First(x => x.ID == model.ID);
                category.ID = model.ID;
                category.CategoryName = model.CategoryName;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
            }
            catch (Exception ex)
            {

                throw ex;
            }
           
        }
    }
}
