using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web.Mvc;
using iBlog.Domain.Helpers;
using iBlog.Domain.Abstract;
using Newtonsoft.Json;
using iBlog.Domain.Entities;
using System.Threading.Tasks;
using iBlog.WebUI.Models;
using ConfigHelper;
using iBlog.Utility;
using iBlog.Utility.Redis;

namespace iBlog.WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        private readonly IPostRepository _postRepository;
        private readonly ICategoryRepository _categoryRepository;

        public AdminController(IPostRepository postRepository, ICategoryRepository categoryRepository)
        {
            _postRepository = postRepository;
            _categoryRepository = categoryRepository;
        }

        /// <summary>
        /// 网站统计
        /// </summary>
        /// <returns></returns>
        public ActionResult Index()
        {
            return View();
        }

        /// <summary>
        /// 文章分类
        /// </summary>
        /// <returns></returns>
        public ActionResult CategoryManage()
        {
            return View();
        }

        /// <summary>
        /// 获取所有分类
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetCategories()
        {
            var data = await _categoryRepository.GetAll();
            return Json(data);
        }

        /// <summary>
        /// 上传分类图片
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        public string UploadImg()
        {
            var file = Request.Files["file"];
            if (file == null)
            {
                return string.Empty;
            }
            var fileName = StringHelper.GenerateShortGuid();
            var filePath = "/Content/Img/" + fileName + Path.GetExtension(file.FileName);
            var saveFilePath = Server.MapPath(filePath);
            file.SaveAs(saveFilePath);
            return filePath;
        }

        /// <summary>
        /// 保存分类数据
        /// </summary>
        /// <param name="json"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task SaveCategories(string json)
        {
            json = json.Substring(1, json.Length - 2);
            var list = JsonConvert.DeserializeObject<List<Category>>(json);
            foreach (var item in list.Where(item => string.IsNullOrWhiteSpace(item.UniqueId)))
            {
                item.UniqueId = StringHelper.GenerateShortGuid();
            }
            await _categoryRepository.Save(list);
        }

        /// <summary>
        /// 新的文章
        /// </summary>
        /// <returns></returns>
        public ActionResult NewArticle()
        {
            return View();
        }

        /// <summary>
        /// 文章管理
        /// </summary>
        /// <returns></returns>
        public ActionResult ArticleList()
        {
            return View();
        }

        /// <summary>
        /// 编辑文章
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public async Task<ViewResult> UpdateArticle(string id)
        {
            var post = await _postRepository.GetPostById(id);
            return View(post);
        }

        /// <summary>
        /// 更新文章
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<bool> UpdateArticle(Post post)
        {
            return await _postRepository.Update(post);
        }

        /// <summary>
        /// 删除多个文章
        /// </summary>
        /// <param name="ids"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<bool> DeleteArticles(string ids)
        {
            return await _postRepository.Delete(ids);
        }

        /// <summary>
        /// 新增文章
        /// </summary>
        /// <param name="post"></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public async Task<string> AddArticle(Post post)
        {
            var uniqueId = StringHelper.GenerateShortGuid();
            post.UniqueId = uniqueId;
            post.CreateTime = post.ModifyTime = DateTime.Now;
            post.ViewCount = 0;
            post.IsActive = true;
            await _postRepository.Insert(post);
            return uniqueId;
        }

        /// <summary>
        /// 判断给定的文章alias是否唯一
        /// </summary>
        /// <param name="alias"></param>
        /// <param name="uid"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> CheckArticleAlias(string alias, string uid = "")
        {
            var result = await _postRepository.CheckAlias(alias, uid);
            return Json(new { result = result });
        }

        /// <summary>
        /// 获取文章列表
        /// </summary>
        /// <param name="filter"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<JsonResult> GetArticleList(ArticleFilterControls filterControl)
        {
            var filterItem = !string.IsNullOrWhiteSpace(filterControl.filter) ? JsonConvert.DeserializeObject<ArticleFilterItem>(filterControl.filter) : new ArticleFilterItem();
            var postResult = await _postRepository.GetArticles(filterControl.pageNumber, filterControl.pageSize, filterControl.sortName, filterControl.sortOrder, filterControl.searchText, filterItem.CateName, filterItem.UniqueId, filterItem.Title);
            var listTemp = postResult.PostList;
            var list = new List<PostItem>();
            foreach (var item in listTemp)
            {
                list.Add(new PostItem
                {
                    UniqueId = item.UniqueId,
                    Alias = item.Alias,
                    Title = item.Title,
                    CateName = await _categoryRepository.GetNameByAlias(item.CategoryAlias),
                    CreateTime = item.CreateTime,
                    ModifyTime = item.ModifyTime,
                    ViewCount = item.ViewCount,
                    Summary = item.Summary,
                    Source = item.Source,
                    Url = item.Url
                });
            }
            var total = postResult.PostCount;
            return new JsonResult { Data = new { rows = list, total = total } };
        }

        /// <summary>
        /// 文章评论
        /// </summary>
        /// <returns></returns>
        public ActionResult Comments()
        {
            return View();
        }

        /// <summary>
        /// 留言板
        /// </summary>
        /// <returns></returns>
        public ActionResult GuestBook()
        {
            return View();
        }

        /// <summary>
        /// 关于管理
        /// </summary>
        /// <returns></returns>
        public ActionResult AboutManage()
        {
            var configSettings = new ConfigSettings();
            var config = new AboutConfig(configSettings, "About", Server.MapPath("~/Settings"));
            config.Load();
            return View(config);
        }

        /// <summary>
        /// 保存更改
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        [HttpPost]
        [ValidateInput(false)]
        public void SaveAbout(AboutConfig conf)
        {
            var configSettings = new ConfigSettings();
            var config = new AboutConfig(configSettings, "About", Server.MapPath("~/Settings"))
            {
                FirstLine = conf.FirstLine,
                SecondLine = conf.SecondLine,
                PhotoPath = conf.PhotoPath,
                QrcodePath = conf.QrcodePath,
                ThirdLine = conf.ThirdLine,
                Profile = conf.Profile,
                Wechat = conf.Wechat,
                Email = conf.Email
            };
            config.Save();
        }

        /// <summary>
        /// 缓存管理
        /// </summary>
        /// <returns></returns>
        public ActionResult CacheManage()
        {
            return View();
        }

        /// <summary>
        /// 根据key获取缓存数据
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        [HttpPost]
        public string GetCacheData(string key)
        {
            var data = RedisManager.GetItem<string>(key);
            return data;
        }

        /// <summary>
        /// 根据key清除缓存数据
        /// </summary>
        /// <param name="key"></param>
        [HttpPost]
        public void ClearCache(string key)
        {
            RedisManager.RemoveItem(key);
        }

        /// <summary>
        /// 异常管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Exception()
        {
            return View();
        }

        /// <summary>
        /// 参数管理
        /// </summary>
        /// <returns></returns>
        public ActionResult Settings()
        {
            var configSettings = new ConfigSettings();
            var config = new SystemConfig(configSettings, "System", Server.MapPath("~/Settings"));
            config.Load();
            return View(config);
        }

        /// <summary>
        /// 保存站点设置
        /// </summary>
        /// <param name="conf"></param>
        public void SaveSite(SystemConfig conf)
        {
            var configSettings = new ConfigSettings();
            var config = new SystemConfig(configSettings, "System", Server.MapPath("~/Settings"));
            config.Load();
            config.SiteName = conf.SiteName;
            config.SiteDomain = conf.SiteDomain;
            config.RecordNo = conf.RecordNo;
            config.LogoPath = conf.LogoPath;
            config.PageSize = conf.PageSize;
            config.ShowMenu = conf.ShowMenu;
            config.CacheExpired = conf.CacheExpired;
            config.Save();
        }

        /// <summary>
        /// 保存组件设置
        /// </summary>
        /// <param name="conf"></param>
        public void SaveComponent(SystemConfig conf)
        {
            var configSettings = new ConfigSettings();
            var config = new SystemConfig(configSettings, "System", Server.MapPath("~/Settings"));
            config.Load();
            config.TranslateKey = conf.TranslateKey;
            config.EnableStatistics = conf.EnableStatistics;
            config.StatisticsId = conf.StatisticsId;
            config.EnableShare = conf.EnableShare;
            config.JiaThisId = conf.JiaThisId;
            config.ShowComments = conf.ShowComments;
            config.ChangyanId = conf.ChangyanId;
            config.ShowGuestbook = conf.ShowGuestbook;
            config.YouyanId = conf.YouyanId;
            config.Save();
        }
    }
}