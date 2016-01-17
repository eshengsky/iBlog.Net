using System.Web;
using System.Web.Optimization;

namespace iBlog.WebUI
{
    public class BundleConfig
    {
        // 有关绑定的详细信息，请访问 http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            //********************基本（前台）********************
            bundles.Add(new ScriptBundle("~/bundles/js").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/site.css",
                          "~/Content/share.css",
                          "~/Content/font-awesome.css",
                          "~/Content/animate-custom.css"));

            //********************首页********************
            bundles.Add(new StyleBundle("~/Content/loading").Include(
            "~/Content/home-loading.css"));

            //********************登录********************
            bundles.Add(new ScriptBundle("~/bundles/account").Include(
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/supersized.3.2.7.js",
            "~/Scripts/supersized.shutter.js",
            "~/Scripts/account.js"));

            bundles.Add(new StyleBundle("~/Content/account").Include(
            "~/Content/sweetalert.css",
            "~/Content/supersized.css",
            "~/Content/supersized.shutter.css",
            "~/Content/animate.css",
            "~/Content/account.css"));

            //********************博客首页********************
            bundles.Add(new ScriptBundle("~/bundles/blog").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/selectlist.js",
            "~/ueditor/ueditor.parse.js",
            "~/Scripts/jquery.mCustomScrollbar.js",
            "~/Scripts/jquery.mousewheel.js",
            "~/Scripts/category.js",
            "~/Scripts/jquery.qrcode-0.12.0.js"));

            bundles.Add(new StyleBundle("~/Content/blog").Include(
          "~/Content/selectlist.css",
          "~/Content/jquery.mCustomScrollbar.css"));

            //********************查看文章********************
            bundles.Add(new ScriptBundle("~/bundles/item").Include(
            "~/ueditor/ueditor.parse.js",
            "~/Scripts/jquery.qrcode-0.12.0.js",
            "~/Scripts/jquery.scrollNav.js",
            "~/Scripts/lightbox.js",
            "~/Scripts/blog-item.js"));

            bundles.Add(new StyleBundle("~/Content/item").Include(
            "~/Content/animate-custom.css",
            "~/Content/lightbox.css"));

            //********************开源********************
            bundles.Add(new ScriptBundle("~/bundles/project").Include(
            "~/Scripts/lightbox.js"));

            bundles.Add(new StyleBundle("~/Content/project").Include(
            "~/Content/lightbox.css"));

            //********************留言********************
            bundles.Add(new ScriptBundle("~/bundles/guestbook").Include(
            "~/Scripts/guestbook.js"));

            //********************关于********************
            bundles.Add(new ScriptBundle("~/bundles/about").Include(
            "~/Scripts/jquery.cycleText.js",
            "~/Scripts/jquery.qrcode-0.12.0.js",
            "~/Scripts/about.js"));

            bundles.Add(new StyleBundle("~/Content/about2").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/icon-style.css",
                      "~/Content/font-awesome.css",
                      "~/Content/jpreloader.css",
                      "~/Content/animate.css",
                      "~/Content/morphext.css",
                      "~/Content/about.css"));

            //********************正则表达式测试********************
            bundles.Add(new ScriptBundle("~/bundles/regtester").Include(
            "~/Scripts/regexr.js",
            "~/Scripts/regExWorker.template.js",
            "~/Scripts/webfontloader.js"));

            bundles.Add(new StyleBundle("~/Content/regtester").Include(
          "~/Content/regexr.css"));

            //********************JSON编辑********************
            bundles.Add(new ScriptBundle("~/bundles/jsoneditor").Include(
            "~/Scripts/jsoneditor.js",
            "~/Scripts/jsoneditor-main.js",
            "~/Scripts/sweetalert.min.js"));

            bundles.Add(new StyleBundle("~/Content/jsoneditor").Include(
          "~/Content/jsoneditor.css",
          "~/Content/sweetalert.css"));

            //********************前端编辑********************
            bundles.Add(new ScriptBundle("~/bundles/fronteditor").Include(
            "~/Scripts/fronteditor.js"));

            bundles.Add(new StyleBundle("~/Content/fronteditor").Include(
          "~/Content/fronteditor.css"));

            //********************代码拼接********************
            bundles.Add(new ScriptBundle("~/bundles/codejoin").Include(
            "~/Scripts/codejoin.js"));

            bundles.Add(new StyleBundle("~/Content/codejoin").Include(
          "~/Content/codejoin.css"));

            //********************基本（后台）********************
            bundles.Add(new ScriptBundle("~/bundles/admin").Include(
            "~/Scripts/jquery-{version}.js",
            "~/Scripts/bootstrap.js",
            "~/Scripts/jquery.metisMenu.js",
            "~/Scripts/admin.js"));

            bundles.Add(new StyleBundle("~/Content/admin").Include(
                          "~/Content/bootstrap.css",
                          "~/Content/admin.css",
                          "~/Content/font-awesome.css",
                          "~/Content/animate.css"));

            //********************新的文章********************
            bundles.Add(new ScriptBundle("~/bundles/new").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/jquery.validate.js",
            "~/Scripts/jquery.validate.unobtrusive.js",
            "~/Scripts/fuelux.js",
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/md5.js",
            "~/Scripts/blog-new.js"));

            bundles.Add(new StyleBundle("~/Content/new").Include(
          "~/Content/fuelux.css",
          "~/Content/sweetalert.css"));

            //********************修改文章********************
            bundles.Add(new ScriptBundle("~/bundles/edit").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/jquery.validate.js",
            "~/Scripts/jquery.validate.unobtrusive.js",
            "~/Scripts/fuelux.js",
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/md5.js",
            "~/Scripts/blog-edit.js"));

            bundles.Add(new StyleBundle("~/Content/edit").Include(
          "~/Content/fuelux.css",
          "~/Content/sweetalert.css"));

            //********************分类管理********************
            bundles.Add(new ScriptBundle("~/bundles/cate-manage").Include(
            "~/Scripts/fuelux.js",
            "~/Scripts/jquery-sortable.js",
            "~/Scripts/jquery.ui.widget.js",
            "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/cate-manage.js"));

            bundles.Add(new StyleBundle("~/Content/cate-manage").Include(
            "~/Content/fuelux.css",
            "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
            "~/Content/sweetalert.css"));

            //********************文章管理********************
            bundles.Add(new ScriptBundle("~/bundles/manage").Include(
            "~/Scripts/bootstrap-table.js",
            "~/Scripts/bootstrap-table-filter-control.js",
            "~/Scripts/bootstrap-table-zh-CN.js",
            "~/Scripts/date-format.js",
            "~/Scripts/lodash.js",
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/blog-manage.js"));

            bundles.Add(new StyleBundle("~/Content/manage").Include(
            "~/Content/bootstrap-table.css",
            "~/Content/sweetalert.css"));

            //********************正则管理********************
            bundles.Add(new ScriptBundle("~/bundles/regmanage").Include(
            "~/Scripts/bootstrap-table.js",
            "~/Scripts/bootstrap-table-zh-CN.js",
            "~/Scripts/lodash.js",
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/jquery.validate.js",
            "~/Scripts/jquery.validate.unobtrusive.js",
            "~/Scripts/fuelux.js",
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/regmanage.js"));

            bundles.Add(new StyleBundle("~/Content/regmanage").Include(
            "~/Content/bootstrap-table.css",
            "~/Content/fuelux.css",
            "~/Content/sweetalert.css"));

            //********************关于管理********************
            bundles.Add(new ScriptBundle("~/bundles/aboutmanage").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/jquery.ui.widget.js",
            "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/about-manage.js"));

            bundles.Add(new StyleBundle("~/Content/aboutmanage").Include(
            "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
            "~/Content/sweetalert.css"));

            //********************缓存管理********************
            bundles.Add(new ScriptBundle("~/bundles/cachemanage").Include(
            "~/Scripts/fuelux.js",
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/cache-manage.js"));

            bundles.Add(new StyleBundle("~/Content/cachemanage").Include(
            "~/Content/fuelux.css",
            "~/Content/sweetalert.css"));

            //********************系统设置********************
            bundles.Add(new ScriptBundle("~/bundles/settings").Include(
            "~/Scripts/jquery.unobtrusive-ajax.js",
            "~/Scripts/jquery.ui.widget.js",
            "~/Scripts/fuelux.js",
            "~/Scripts/jQuery.FileUpload/jquery.fileupload.js",
            "~/Scripts/sweetalert.min.js",
            "~/Scripts/switchery.js",
            "~/Scripts/settings.js"));

            bundles.Add(new StyleBundle("~/Content/settings").Include(
            "~/Content/fuelux.css",
            "~/Content/jQuery.FileUpload/css/jquery.fileupload.css",
            "~/Content/sweetalert.css",
            "~/Content/switchery.css"));
        }
    }
}
