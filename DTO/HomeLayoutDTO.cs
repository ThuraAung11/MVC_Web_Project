using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DTO
{
    public class HomeLayoutDTO
    {
        public List<CategoryDTO> CategoryList { get; set; }
        public SocialMediaDTO Facebook { get; set; }
        public SocialMediaDTO Twitter { get; set; }
        public SocialMediaDTO Youtube { get; set; }
        public SocialMediaDTO Googleplus { get; set; }
        public SocialMediaDTO Linkedin { get; set; }
        public SocialMediaDTO Instragram { get; set; }
        public FavDTO Fav { get; set; }
        public List<MetaDTO> MetaList { get; set; }
        public AddressDTO Address { get; set; }
        public List<PostDTO> HotNews{ get; set; }

    }
}
