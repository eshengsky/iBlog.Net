using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using Newtonsoft.Json;
using iBlog.Domain.Abstract;
using iBlog.WebUI.Models;
using ConfigHelper;
using iBlog.Utility.Redis;
using iBlog.Domain.Entities;

namespace iBlog.WebUI.Controllers
{
    public class BlogController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public BlogController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        public async Task<ActionResult> Category(string id = "")
        {
            var cateListTemp = RedisManager.GetItem<List<Category>>("categories");
            if (cateListTemp == null)
            {
                cateListTemp = await _categoryRepository.GetAll();
                if (cateListTemp != null && cateListTemp.Any())
                {
                    RedisManager.SetItem("categories", cateListTemp, new TimeSpan(0, Settings.Config.CacheExpired, 0));
                }
            }
            if (id != "" && id != "other" && !cateListTemp.Select(t => t.Alias).Contains(id))
            {
                var error = new Elmah.Error(new Exception("分类alias：" + id + " 不存在！"), System.Web.HttpContext.Current) { StatusCode = 404 };
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(error);
                return View("Error404");
            }
            var cateList = new List<CategoryBar>();
            cateList.Add(new CategoryBar
            {
                Alias = string.Empty,
                CateName = "全部分类",
                Img = "/Content/Img/全部分类.svg"
                //PostCount = await _postRepository.GetPostCountByCate(string.Empty)
            });
            foreach (var category in cateListTemp)
            {
                cateList.Add(new CategoryBar
                {
                    Alias = category.Alias,
                    CateName = category.CateName,
                    Img = category.Img,
                    Link = category.Link
                    //PostCount = await _postRepository.GetPostCountByCate(category.UniqueId)
                });
            }
            cateList.Add(new CategoryBar
            {
                Alias = "other",
                CateName = "未分类",
                Img = "/Content/Img/未分类.svg"
                //PostCount = await _postRepository.GetPostCountByCate("other")
            });
            ViewBag.CateList = cateList;
            ViewBag.CateAlias = id;
            return View("Category");
        }

        public class PostLabel
        {
            public string Text { get; set; }

            public string Value { get; set; }
        }

        [Route("{id}")]
        public async Task<ViewResult> Item(string id)
        {
            if (id.Equals("about", StringComparison.CurrentCultureIgnoreCase))
            {
                var configSettings = new ConfigSettings();
                var config = new AboutConfig(configSettings, "About", Server.MapPath("~/Settings"));
                config.Load();
                return View("About", config);
            }
            if (id.Equals("guestbook", StringComparison.CurrentCultureIgnoreCase))
            {
                return View("Guestbook");
            }
            var cacheKey = "post_" + id;
            var post = RedisManager.GetItem<Post>(cacheKey);
            if (post == null)
            {
                post = await _postRepository.GetPostByAlias(id);
                if (post != null)
                {
                    RedisManager.SetItem(cacheKey, post, new TimeSpan(0, Settings.Config.CacheExpired, 0));
                }
            }
            if (post != null)
            {
                var item = new PostItem
                {
                    UniqueId = post.UniqueId,
                    Title = post.Title,
                    CategoryAlias = post.CategoryAlias,
                    CateName = await _categoryRepository.GetNameByAlias(post.CategoryAlias),
                    CreateTimeStr = post.CreateTime.ToString("yy-MM-dd HH:mm"),
                    ViewCount = post.ViewCount,
                    LabelList = JsonConvert.DeserializeObject<List<LabelShow>>(post.Labels).Select(t => t.Text).ToList(),
                    Summary = post.Summary,
                    Content = post.Content
                };
                return View(item);
            }
            else
            {
                var error = new Elmah.Error(new Exception("文章id：" + id + " 不存在！"), System.Web.HttpContext.Current) { StatusCode = 404 };
                Elmah.ErrorLog.GetDefault(System.Web.HttpContext.Current).Log(error);
                return View("Error404");
            }
        }

        public class LabelShow
        {
            public string Text { get; set; }
            public string Value { get; set; }
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetPostList(Filters filter)
        {
            var cacheKey = "postlist_ca_" + filter.CateAlias + "_ft_" + (int)filter.FiltType + "_kw_" + filter.Keyword + "_pi_" + filter.PageIndex + "_ps_" + filter.PageSize + "_sb_" + (int)filter.SortBy;
            var postResult = RedisManager.GetItem<PostResult>(cacheKey);
            if (postResult == null)
            {
                postResult = await _postRepository.GetPosts(filter.CateAlias, filter.PageIndex, filter.PageSize, filter.SortBy, filter.FiltType, filter.Keyword);
                if (postResult != null)
                {
                    RedisManager.SetItem(cacheKey, postResult, new TimeSpan(0, Settings.Config.CacheExpired, 0));
                }
            }
            var listTemp = postResult.PostList;
            var list = new List<PostItem>();
            foreach (var item in listTemp)
            {
                list.Add(new PostItem
                {
                    UniqueId = item.UniqueId,
                    Alias = item.Alias,
                    Title = item.Title,
                    CategoryAlias = item.CategoryAlias,
                    CateName = await _categoryRepository.GetNameByAlias(item.CategoryAlias),
                    CreateTimeStr = item.CreateTime.ToString("yy-MM-dd HH:mm"),
                    ViewCount = item.ViewCount,
                    Summary = item.Summary,
                    Source = item.Source,
                    Url = item.Url,
                    Host = !string.IsNullOrWhiteSpace(item.Url) ? new Uri(item.Url).Host : string.Empty
                });
            }
            var total = postResult.PostCount;
            var pageCount = total % filter.PageSize != 0 ? total / filter.PageSize + 1 : total / filter.PageSize;
            return new JsonResult { Data = new { pageCount = pageCount, items = list } };
        }

        /// <summary>
        /// 根据Id获取预览内容
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<JsonResult> GetPreviewContent(string id)
        {
            var cacheKey = "post_" + id;
            var post = RedisManager.GetItem<Post>(cacheKey);
            if (post == null)
            {
                post = await _postRepository.GetPostByAlias(id);
                if (post != null)
                {
                    RedisManager.SetItem(cacheKey, post, new TimeSpan(0, Settings.Config.CacheExpired, 0));
                }
            }
            return new JsonResult { Data = new { post.Content, post.Labels } };
        }

        public ViewResult Error404()
        {
            return View("Error404");
        }

        public ViewResult Error500()
        {
            return View("Error500");
        }
    }
}