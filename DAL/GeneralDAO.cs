using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class GeneralDAO : PostContext
    {
        public List<PostDTO> GetSliderPost()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Slider == true && x.isDeleted == false)
                       join c in db.Categories on p.CategoryID equals c.ID
                       select new
                       {
                           postID=p.ID,
                           title=p.Title,
                           categoryName=c.CategoryName,
                           seolink=p.SeoLink,
                           addDate=p.AddDate,
                           Viewcount=p.ViewCount,

                       }).OrderByDescending(x=>x.addDate).Take(8).ToList();

            foreach (var item in list)
            {
                PostDTO dto = new PostDTO()
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    SeoLink = item.seolink,
                    AddDate = item.addDate,
                    ViewCount = item.Viewcount,
                };
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x=>x.isApproved==true && x.isDeleted==false && x.PostID==item.postID).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<VideoDTO> GetAllVideo()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            List<Video> list = db.Videos.Where(x => x.isDeleted == false).OrderByDescending(x => x.AddDate).ToList();
            foreach (var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.AddDate = item.AddDate;

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetCategoryPostList(int categoryID)
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.CategoryID == categoryID && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            addDate = p.AddDate,
                            Viewcount = p.ViewCount,

                        }).OrderByDescending(x => x.addDate).ToList();

            foreach (var item in list)
            {
                PostDTO dto = new PostDTO()
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    SeoLink = item.seolink,
                    AddDate = item.addDate,
                    ViewCount = item.Viewcount,
                };
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isApproved == true && x.isDeleted == false && x.PostID == item.postID).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetSearchPosts(string searchText)
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => (x.Title.Contains(searchText))|| (x.PostContent.Contains(searchText)) && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            addDate = p.AddDate,
                            Viewcount = p.ViewCount,

                        }).OrderByDescending(x => x.addDate).ToList();

            foreach (var item in list)
            {
                PostDTO dto = new PostDTO()
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    SeoLink = item.seolink,
                    AddDate = item.addDate,
                    ViewCount = item.Viewcount,
                };
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isApproved == true && x.isDeleted == false && x.PostID == item.postID).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public PostDTO GetPostDetailItemWithID(int ID)
        {
            Post post = db.Posts.First(x=>x.ID==ID);
            post.ViewCount++;
            db.SaveChanges();
            PostDTO dto = new PostDTO();
            dto.ID = post.ID;
            dto.Title = post.Title;
            dto.ShortContent = post.ShortContent;
            dto.PostContent = post.PostContent;
            dto.SeoLink = post.SeoLink;
            dto.CategoryID = post.CategoryID;
            dto.Language = post.LanguageName;
            dto.CategoryName = (db.Categories.First(x=>x.ID==dto.CategoryID)).CategoryName;
            List<PostImage> image = db.PostImages.Where(x=>x.PostID==ID).ToList();
            List<PostImageDTO> imglist = new List<PostImageDTO>();
            foreach (var item in image)
            {
                PostImageDTO img = new PostImageDTO() 
                {
                    ID=item.ID,
                    ImagePath=item.ImagePath,
                    PostID=item.PostID
                };
                imglist.Add(img);

            }
            dto.PostImages = imglist;
            dto.CommentCount = db.Comments.Where(x=>x.isDeleted==false && x.isApproved==true && x.PostID==ID).Count();
            List<Comment> commentList = db.Comments.Where(x=>x.isDeleted==false && x.isApproved==true && x.PostID==ID).ToList();
            List<CommentDTO> commentls = new List<CommentDTO>();
            foreach (var item in commentList)
            {
                CommentDTO com = new CommentDTO()
                {
                    ID = item.ID,
                    CommentContent = item.CommentContent,
                    Name = item.NameSurname,
                    Email=item.Email,
                    AddDate=item.AddDate
                };
                commentls.Add(com);
            }
            dto.CommentList = commentls;

            List<PostTag> tag = db.PostTags.Where(x=>x.isDeleted==false && x.PostID==ID).ToList();
            List<TagDTO> tagList = new List<TagDTO>();
            foreach (var item in tag)
            {
                TagDTO tdto = new TagDTO() 
                {
                    ID=item.ID,
                    PostID=item.PostID,
                    TagContent=item.TagContent
                };
                tagList.Add(tdto);
            }
            dto.TagList = tagList;
            return dto;
        }

        public List<VideoDTO> GetVideos()
        {
            List<VideoDTO> dtolist = new List<VideoDTO>();
            List<Video> list = db.Videos.Where(x=>x.isDeleted==false).OrderByDescending(x=>x.AddDate).Take(3).ToList();
            foreach (var item in list)
            {
                VideoDTO dto = new VideoDTO();
                dto.ID = item.ID;
                dto.Title = item.Title;
                dto.VideoPath = item.VideoPath;
                dto.OriginalVideoPath = item.OriginalVideoPath;
                dto.AddDate = item.AddDate;

                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetMostViewPost()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            addDate = p.AddDate,
                            Viewcount = p.ViewCount,

                        }).OrderByDescending(x => x.Viewcount).Take(5).ToList();

            foreach (var item in list)
            {
                PostDTO dto = new PostDTO()
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    SeoLink = item.seolink,
                    AddDate = item.addDate,
                    ViewCount = item.Viewcount,
                };
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isApproved == true && x.isDeleted == false && x.PostID == item.postID).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetPopularPost()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Area2==true && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            addDate = p.AddDate,
                            Viewcount = p.ViewCount,

                        }).OrderByDescending(x => x.addDate).Take(8).ToList();

            foreach (var item in list)
            {
                PostDTO dto = new PostDTO()
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    SeoLink = item.seolink,
                    AddDate = item.addDate,
                    ViewCount = item.Viewcount,
                };
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isApproved == true && x.isDeleted == false && x.PostID == item.postID).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }

        public List<PostDTO> GetBreakingPost()
        {
            List<PostDTO> dtolist = new List<PostDTO>();
            var list = (from p in db.Posts.Where(x => x.Slider == false && x.isDeleted == false)
                        join c in db.Categories on p.CategoryID equals c.ID
                        select new
                        {
                            postID = p.ID,
                            title = p.Title,
                            categoryName = c.CategoryName,
                            seolink = p.SeoLink,
                            addDate = p.AddDate,
                            Viewcount = p.ViewCount,

                        }).OrderByDescending(x => x.addDate).Take(5).ToList();

            foreach (var item in list)
            {
                PostDTO dto = new PostDTO()
                {
                    ID = item.postID,
                    Title = item.title,
                    CategoryName = item.categoryName,
                    SeoLink = item.seolink,
                    AddDate = item.addDate,
                    ViewCount = item.Viewcount,
                };
                PostImage image = db.PostImages.First(x => x.isDeleted == false && x.PostID == item.postID);
                dto.ImagePath = image.ImagePath;
                dto.CommentCount = db.Comments.Where(x => x.isApproved == true && x.isDeleted == false && x.PostID == item.postID).Count();
                dtolist.Add(dto);
            }
            return dtolist;
        }
    }
}
