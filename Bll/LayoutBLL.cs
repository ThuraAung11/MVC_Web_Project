using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class LayoutBLL
    {
        CategoryDAO dao = new CategoryDAO();
        SocialMediaDAO sdao = new SocialMediaDAO();
        FavDAO favdao = new FavDAO();
        MetaDAO metadao = new MetaDAO();
        AddressDAO addressdao = new AddressDAO();
        PostDAO postdao = new PostDAO();
        public HomeLayoutDTO GetLayoutData()
        {
            HomeLayoutDTO dto = new HomeLayoutDTO();
            dto.CategoryList = dao.GetCategory();
            List<SocialMediaDTO> socialMediaList = new List<SocialMediaDTO>();
            socialMediaList = sdao.GetSocialMedias(); 
             dto.Facebook = socialMediaList.First(x => x.Link.Contains("facebook"));
            dto.Twitter = socialMediaList.First(x => x.Link.Contains("twitter"));
            dto.Instragram = socialMediaList.First(x => x.Link.Contains("instragram"));
            dto.Linkedin = socialMediaList.First(x => x.Link.Contains("linkedin"));
            dto.Googleplus = socialMediaList.First(x => x.Link.Contains("googleplus"));
            dto.Youtube = socialMediaList.First(x => x.Link.Contains("youtube"));

            dto.Fav = favdao.GetFav();
            dto.MetaList = metadao.GetMetaData();

            List<AddressDTO> addresslist = new List<AddressDTO>();
            addresslist= addressdao.GetAddress();
            dto.Address = addresslist.First();

            dto.HotNews = postdao.GetHotNews();
            return dto;
        }


    }
}
