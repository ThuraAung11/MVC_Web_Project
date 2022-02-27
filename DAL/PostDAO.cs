using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL
{
    public class PostDAO
    {
        public int AddPost(Post post)
        {
            try
            {
                using (POSTDATAEntities1 db=new POSTDATAEntities1())
                {
                    db.Posts.Add(post);
                    db.SaveChanges();
                   
                }
                return post.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddImage(PostImage item)
        {
            try
            {
                using (POSTDATAEntities1 db = new POSTDATAEntities1())
                {
                    db.PostImages.Add(item);
                    db.SaveChanges();

                }

                return item.ID;
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public int AddTag(PostTag item)
        {
            try
            {
                using (POSTDATAEntities1 db = new POSTDATAEntities1())
                {
                    db.PostTags.Add(item);
                    db.SaveChanges();
                    
                }
                return item.ID;
            }
            catch (Exception ex)
            {

                throw ex; 
            }
        }

        public List<PostDTO> GetPost()
        {
            List<PostDTO> ls = new List<PostDTO>();
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                var postlist = (from p in db.Posts.Where(x => x.isDeleted == false)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    categoryName = c.CategoryName,
                                    AddDate = p.AddDate,
                                    seolink = p.SeoLink
                                }).OrderByDescending(x => x.AddDate).Take(8).ToList();


                foreach (var item in postlist)
                {
                    PostDTO dto = new PostDTO()
                    {
                        ID = item.ID,
                        Title = item.Title,
                        CategoryName = item.categoryName,
                        AddDate = item.AddDate,
                        SeoLink = item.seolink
                    };
                    ls.Add(dto);
                }
            }
           
            return ls;
        }

        public List<CommentDTO> GetAllComments()
        {
            List<CommentDTO> cList = new List<CommentDTO>();
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                var list = (from c in db.Comments.Where(x => x.isDeleted == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Content = c.CommentContent,
                                AddDate = c.AddDate,
                                Email = c.Email,
                                isApproved = c.isApproved
                            }).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO()
                    {
                        ID = item.ID,
                        Name = item.PostTitle,
                        CommentContent = item.Content,
                        Email = item.Email,
                        AddDate = item.AddDate,
                        isApproved = item.isApproved
                    };
                    cList.Add(dto);
                }
            }
           
          
            return cList;
        }

        public CountDTO GetAllCount()
        {
            CountDTO dto = new CountDTO();
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
               
                dto.CommentCount = db.Comments.Where(x => x.isDeleted == false).Count();
                dto.MessageCount = db.Contacts.Where(x => x.isDeleted == false).Count();
                dto.PostCount = db.Posts.Where(x => x.isDeleted == false).Count();
                dto.ViewCount = db.Posts.Where(x => x.isDeleted == false).Sum(x => x.ViewCount);
            }
            return dto;

        }

      
        public void DeleteComment(int ID)
        {
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                Comment cmt = db.Comments.First(x => x.ID == ID);
                cmt.isDeleted = true;
                cmt.DeletedDate = DateTime.Now;
                cmt.LastUpdateDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
           
        }

        public void ApprovedComment(int ID)
        {
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                Comment cmt = db.Comments.First(x => x.ID == ID);
                cmt.isApproved = true;
                cmt.ApproveDate = DateTime.Now;
                cmt.ApproveUserID = UserStatic.UserID;
                cmt.LastUpdateDate = DateTime.Now;
                cmt.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();
            }
         
        }

        public List<CommentDTO> GetCommentList()
        {
            List<CommentDTO> cList = new List<CommentDTO>();
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                var list = (from c in db.Comments.Where(x => x.isApproved == false && x.isDeleted == false)
                            join p in db.Posts on c.PostID equals p.ID
                            select new
                            {
                                ID = c.ID,
                                PostTitle = p.Title,
                                Content = c.CommentContent,
                                AddDate = c.AddDate,
                                Email = c.Email
                            }).OrderBy(x => x.AddDate).ToList();
                foreach (var item in list)
                {
                    CommentDTO dto = new CommentDTO()
                    {
                        ID = item.ID,
                        Name = item.PostTitle,
                        CommentContent = item.Content,
                        Email = item.Email,
                        AddDate = item.AddDate
                    };
                    cList.Add(dto);
                }
            }
           
            return cList;
        }

        public List<PostDTO> GetHotNews()
        {
            List<PostDTO> ls = new List<PostDTO>();
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                var postlist = (from p in db.Posts.Where(x => x.isDeleted == false && x.Area1 == true)
                                join c in db.Categories on p.CategoryID equals c.ID
                                select new
                                {
                                    ID = p.ID,
                                    Title = p.Title,
                                    SeoLink = p.SeoLink,
                                    categoryName = c.CategoryName,
                                    AddDate = p.AddDate
                                }).OrderByDescending(x => x.AddDate).ToList();


                foreach (var item in postlist)
                {
                    PostDTO dto = new PostDTO()
                    {
                        ID = item.ID,
                        Title = item.Title,
                        CategoryName = item.categoryName,
                        SeoLink = item.SeoLink,
                        AddDate = item.AddDate
                    };
                    ls.Add(dto);
                }
            }
         
            return ls;
        }

        public int GetMessageCount()
        {
            
            using (POSTDATAEntities1 db=new POSTDATAEntities1())
            {
               return db.Contacts.Where(x => x.isDeleted == false && x.isRead == false).Count();
            }
     
        }

        public int GetCommentCount()
        {
           
            using (POSTDATAEntities1 db=new POSTDATAEntities1())
            {
                return  db.Comments.Where(x => x.isDeleted == false && x.isApproved == false).Count();
            }
            
        }

        public void AddComment(Comment comment)
        {
            try
            {
                using (POSTDATAEntities1 db = new POSTDATAEntities1())
                {
                    db.Comments.Add(comment);
                    db.SaveChanges();
                }
             
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public PostDTO GetPostsWithID(int ID)
        {
            PostDTO model = new PostDTO();
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                Post post = db.Posts.First(x => x.ID == ID);
                
                model.ID = post.ID;
                model.PostContent = post.PostContent;
                model.ShortContent = post.ShortContent;
                model.Title = post.Title;
                model.SeoLink = post.SeoLink;
                model.Notification = post.Notification;
                model.Slider = post.Slider;
                model.Area1 = post.Area1;
                model.Area2 = post.Area2;
                model.Area3 = post.Area3;
                model.CategoryID = post.CategoryID;
                model.Language = post.LanguageName;
               
            }
            return model;
        }

        public List<PostImageDTO> GetPostImagesWithID(int PostID)
        {
            List<PostImageDTO> List = new List<PostImageDTO>();
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                List<PostImage> imgList = db.PostImages.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();

                foreach (var item in imgList)
                {
                    PostImageDTO dto = new PostImageDTO()
                    {
                        ID = item.ID,
                        ImagePath = item.ImagePath
                    };
                    List.Add(dto);
                }
            }
           
            return List;
        }

        public List<PostTag> GetTagsWithID(int PostID)
        {
            List<PostTag> ls = new List<PostTag>();
            using (POSTDATAEntities1 db=new POSTDATAEntities1())
            {
                ls= db.PostTags.Where(x => x.isDeleted == false && x.PostID == PostID).ToList();
            }
            return ls;
        }

        public void UpdatePost(PostDTO model)
        {
            try
            {
                using (POSTDATAEntities1 db = new POSTDATAEntities1())
                {
                    Post p = db.Posts.First(x => x.ID == model.ID);
                    p.ID = model.ID;
                    p.Title = model.Title;
                    p.PostContent = model.PostContent;
                    p.ShortContent = model.ShortContent;
                    p.SeoLink = model.SeoLink;
                    p.LanguageName = model.Language;
                    p.Slider = model.Slider;
                    p.Area1 = model.Area1;
                    p.Area2 = model.Area2;
                    p.Area3 = model.Area3;
                    p.CategoryID = model.CategoryID;
                    p.Notification = model.Notification;
                    p.LastUpdateDate = DateTime.Now;
                    p.LastUpdateUserID = UserStatic.UserID;

                    db.SaveChanges();
                }
               
            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> imageList = new List<PostImageDTO>();
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                Post post = db.Posts.First(x => x.ID == ID);
                post.isDeleted = true;
                post.DeletedDate = DateTime.Now;
                post.LastUpdateDate = DateTime.Now;
                post.LastUpdateUserID = UserStatic.UserID;
                db.SaveChanges();

                List<PostImage> pImageList = db.PostImages.Where(x => x.PostID == ID).ToList();

                foreach (var item in pImageList)
                {
                    PostImageDTO pimg = new PostImageDTO();
                    pimg.ImagePath = item.ImagePath;
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdateDate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;

                    imageList.Add(pimg);
                }
                db.SaveChanges();
            }
            
            return imageList;
        }

        public string DeletePostImage(int ID)
        {
            string imgPath = "";
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                PostImage img = db.PostImages.First(x => x.ID == ID);
                imgPath = img.ImagePath;
                img.isDeleted = true;
                img.DeletedDate = DateTime.Now;
                img.LastUpdateDate = DateTime.Now;
                img.LastUpdateUserID = UserStatic.UserID;

                db.SaveChanges();
              
            }
            return imgPath;

        }

        public void DeleteTags(int PostID)
        {
            using (POSTDATAEntities1 db = new POSTDATAEntities1())
            {
                List<PostTag> tagList = db.PostTags.Where(x => x.isDeleted == false && x.ID == PostID).ToList();
                foreach (var item in tagList)
                {
                    item.isDeleted = true;
                    item.DeletedDate = DateTime.Now;
                    item.LastUpdateDate = DateTime.Now;
                    item.LastUpdateUserID = UserStatic.UserID;
                }

                db.SaveChanges();
            }
          
        }
    }
}
