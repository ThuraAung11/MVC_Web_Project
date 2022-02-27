using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Bll
{
    public class CategoryBll
    {
        CategoryDAO dao = new CategoryDAO();
        public bool AddCategory(CategoryDTO model)
        {

            Category category = new Category();
            category.CategoryName = model.CategoryName;
            category.AddDate = DateTime.Now;
            category.LastUpdateDate = DateTime.Now;
            category.LastUpdateUserID = UserStatic.UserID;

            int ID = dao.AddCategory(category);
            LogDAL.AddLog(General.ProcessType.CategoryAdd,General.TableName.Category,ID);
            return true;
        }

        public static IEnumerable<SelectListItem> GetCategoryForDropDown()
        {
            return CategoryDAO.GetCategoryForDropDown();
        }

        public List<CategoryDTO> GetCategory()
        {
            return dao.GetCategory();
        }

        public CategoryDTO GetCategoryWithID(int ID)
        {
            return dao.GetCategoryWithID(ID);
        }

        public bool UpdateCategory(CategoryDTO model)
        {
            dao.UpdateCategory(model);
            LogDAL.AddLog(General.ProcessType.CategoryUpdate,General.TableName.Category,model.ID);
            return true;
        }

        PostBll pbll = new PostBll();
        public List<PostImageDTO> DeleteCategory(int ID)
        {
            List<Post> pImageList=dao.DeleteCategory(ID);
            LogDAL.AddLog(General.ProcessType.CategoryDelete,General.TableName.Category,ID);
            List<PostImageDTO> imgList = new List<PostImageDTO>();
            foreach (var item in pImageList)
            {
                List<PostImageDTO> dto = pbll.DeletePost(item.ID);
                LogDAL.AddLog(General.ProcessType.PostDelete,General.TableName.Post,item.ID);
                foreach (var item2 in dto)
                {
                    imgList.Add(item2);
                }
            }
            return imgList;

        }
    }
}
