using DAL;
using DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Bll
{
    public class PostBll
    {
        PostDAO dao = new PostDAO();
        public bool AddPost(PostDTO model)
        {
            Post post = new Post();
            post.Title = model.Title;
            post.PostContent = model.PostContent;
            post.ShortContent = model.ShortContent;
            post.Slider = model.Slider;
            post.Area1 = model.Area1;
            post.Area2 = model.Area2;
            post.Area3 = model.Area3;
            post.Notification = model.Notification;
            post.CategoryID = model.CategoryID;
            post.SeoLink = SeoLink.GenerateUrl(model.Title);
            post.AddDate = DateTime.Now;
            post.LastUpdateDate = DateTime.Now;
            post.LastUpdateUserID = UserStatic.UserID;
            post.AddUserID = UserStatic.UserID;
            post.LanguageName = model.Language;

            int ID = dao.AddPost(post);
            LogDAL.AddLog(General.ProcessType.PostAdd,General.TableName.Post,ID);
            SaveImages(model.PostImages,ID);
            AddTag(model.TagText,ID);

            return true;
        }

        public List<CommentDTO> GetAllComments()
        {
            return dao.GetAllComments();
        }

        public void DeleteComment(int ID)
        {
            dao.DeleteComment(ID);
            LogDAL.AddLog(General.ProcessType.CommentDelete,General.TableName.Comment,ID);
        }

        public void ApprovedComment(int ID)
        {
            dao.ApprovedComment(ID);
            LogDAL.AddLog(General.ProcessType.CommentApprove, General.TableName.Comment,ID) ;
        }

        public List<CommentDTO> GetAllComment()
        {
            return dao.GetCommentList();
        }

        private void AddTag(string tagText, int PostID)
        {
            string[] Tags;
            Tags = tagText.Split(',');
            List<PostTag> tagList = new List<PostTag>();
            foreach (var item in Tags)
            {
                PostTag tag = new PostTag();
                tag.PostID = PostID;
                tag.TagContent = item;
                tag.LastUpdateDate = DateTime.Now;
                tag.LastUpdateUserID = UserStatic.UserID;
                tagList.Add(tag);
            }
            foreach (var item in tagList)
            {
                int ID = dao.AddTag(item);
                LogDAL.AddLog(General.ProcessType.TagAdd,General.TableName.Tag,ID);
            }
        }

        public bool AddComment(GeneralDTO model)
        {
            Comment comment = new Comment();
            comment.PostID = model.PostID;
            comment.NameSurname = model.Name;
            comment.Email = model.Email;
            comment.CommentContent = model.Message;
            comment.AddDate = DateTime.Now;
            dao.AddComment(comment);
            return true;
        }

        public CountDTO GetAllCount()
        {
            return dao.GetAllCount();
        }

        void SaveImages(List<PostImageDTO> list,int PostID) 
        {
            List<PostImage> imageList = new List<PostImage>();
            foreach (var item in list)
            {
                PostImage image = new PostImage ();
                image.PostID = PostID;
                image.ImagePath = item.ImagePath;
                image.AddDate = DateTime.Now;
                image.LastUpdateDate = DateTime.Now;
                image.LastUpdateUserID = UserStatic.UserID;
                
                imageList.Add(image);
            }

            foreach (var item in imageList)
            {
                int imageID = dao.AddImage(item);
                LogDAL.AddLog(General.ProcessType.ImageAdd, General.TableName.Image, imageID);
            }
        }

        public List<PostDTO> GetPost()
        {
            return dao.GetPost();
        }

        public PostDTO GetPostsWithID(int ID)
        {
            PostDTO dto = new PostDTO();
            dto = dao.GetPostsWithID(ID);
            dto.PostImages = dao.GetPostImagesWithID(ID);
            List<PostTag> tagList = dao.GetTagsWithID(ID);
            string tagvalue = "";
            foreach (var item in tagList)
            {
                tagvalue += item.TagContent;
                tagvalue += ",";
            }
            dto.TagText = tagvalue;
            return dto;
        }

        public bool UpdatePost(PostDTO model)
        {
            model.SeoLink = Bll.SeoLink.GenerateUrl(model.Title);
            dao.UpdatePost(model);
            LogDAL.AddLog(General.ProcessType.PostUpdate,General.TableName.Post,model.ID);
            if (model.PostImages!=null)
            {
                SaveImages(model.PostImages,model.ID);
            }
            dao.DeleteTags(model.ID);
            AddTag(model.TagText,model.ID);
            return true;
        }

        public string DeletePostImage(int ID)
        {
            string imgpath = dao.DeletePostImage(ID);
            LogDAL.AddLog(General.ProcessType.ImageDelete,General.TableName.Image,ID);
            return imgpath;
        }

        public List<PostImageDTO> DeletePost(int ID)
        {
            List<PostImageDTO> imglist = dao.DeletePost(ID);
            LogDAL.AddLog(General.ProcessType.PostDelete,General.TableName.Post,ID);
            return imglist;
        }

        public CountDTO GetCount()
        {
            CountDTO dto = new CountDTO();
            dto.CommentCount = dao.GetCommentCount();
            dto.MessageCount = dao.GetMessageCount();
            return dto;
        }
    }
}  
