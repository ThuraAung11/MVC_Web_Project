using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class GeneralBLL
    {
        GeneralDAO dao = new GeneralDAO();
        AdsDAO adsdao = new AdsDAO();
        CategoryDAO categorydao = new CategoryDAO();
        AddressDAO addressdao = new AddressDAO();
        public GeneralDTO GetAllPost()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.SliderPost = dao.GetSliderPost();
            dto.BreakingPost=dao.GetBreakingPost();
            dto.PopularPost = dao.GetPopularPost();
            dto.MostViewedPost = dao.GetMostViewPost();
            dto.Videos = dao.GetVideos();
            dto.Adslist = adsdao.GetAds();
            return dto;
        }

        public GeneralDTO GetPostDetailItemWithID(int ID)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.Adslist = adsdao.GetAds();
            dto.PostDetail = dao.GetPostDetailItemWithID(ID);
            return dto;
        }

        public GeneralDTO GetCategoryPostList(string categoryName)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.Adslist = adsdao.GetAds();
            if (categoryName=="video")
            {
                dto.Videos = dao.GetAllVideo();
                categoryName = "video";
            }
            else
            {
                List<CategoryDTO> cdto = categorydao.GetCategory();
                int categoryID = 0;
                foreach (var item in cdto)
                {
                    if (categoryName==SeoLink.GenerateUrl(item.CategoryName))
                    {
                        categoryID = item.ID;
                        dto.CategoryName = item.CategoryName;
                        break;
                    }
                }
                dto.CategoryPostList = dao.GetCategoryPostList(categoryID);
            }
            return dto;
        }

        public GeneralDTO GetContactItem()
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.Adslist = adsdao.GetAds();
            dto.Address = addressdao.GetAddress().First();
            return dto;
        }

        public GeneralDTO GetSearchPosts(string searchText)
        {
            GeneralDTO dto = new GeneralDTO();
            dto.BreakingPost = dao.GetBreakingPost();
            dto.Adslist = adsdao.GetAds();
            dto.CategoryPostList = dao.GetSearchPosts(searchText);
            dto.SearchText = searchText;
            return dto;
        }
    }
}
