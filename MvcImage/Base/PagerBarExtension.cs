using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.Mvc;

namespace System.Web.Mvc
{
    public enum BarStyle
    {
        yahoo, digg, meneame, flickr, sabrosus, scott, quotes, black, black2, grayr, yellow, jogger, starcraft2, tres, megas512, technorati, youtube, msdn, badoo, viciao, yahoo2, green_black
    }
    public static class PagerBarExtension
    {

        public static string RenderPagerBar(this HtmlHelper html, int page, int total)
        {
            return RenderPagerBar(html, page, total, BarStyle.technorati);
        }

        public static string RenderPagerBar(this HtmlHelper html, int page, int total, BarStyle style)
        {
            return RenderPagerBar(html, page, total, style, total);
        }

        public static string RenderPagerBar(this HtmlHelper html, int page, int total, BarStyle style, int show)
        {
            if (total == 1)
            {
                return "";
            }
            else
            {
                StringBuilder sb = new StringBuilder();
                string _path = html.ViewContext.HttpContext.Request.Path;
                sb.Append("<div class=\"");
                sb.Append(style.ToString());
                sb.Append("\" >");

                string queryString = html.ViewContext.HttpContext.Request.QueryString.ToString();
                if (queryString.IndexOf("page=") < 0)
                {
                    queryString += "&page=" + page;
                }
                Regex re = new Regex(@"page=\d+", RegexOptions.IgnoreCase);
                string result = re.Replace(queryString, "page={0}");

                if (page != 1)
                {
                    sb.AppendFormat("<span><a href=\"{0}\" title=\"第一页\">{1}</a></span>", _path + "?" + string.Format(result, 1), "<<");
                    sb.AppendFormat("<span><a href=\"{0}\" title=\"上一页\">{1}</a></span>", _path + "?" + string.Format(result, page - 1), "<");
                }
                if (page > (show + 1))
                {
                    sb.AppendFormat("<span><a href=\"{0}\" title=\"前" + (show + 1) + "页\">{1}</a></span>", _path + "?" + string.Format(result, page - (show + 1)), "..");

                }
                for (int i = page - show; i <= page + show; i++)
                {
                    if (i == page)
                    {
                        sb.AppendFormat("<span class=\"current\">{0}</span>", i);
                    }
                    else
                    {
                        if (i > 0 & i <= total)
                        {
                            sb.AppendFormat("<span><a href=\"{0}\">{1}</a></span>", _path + "?" + string.Format(result, i), i);
                        }
                    }
                }
                if (page < (total - (show)))
                {
                    sb.AppendFormat("<span><a href=\"{0}\" title=\"后" + (show + 1) + "页\">{1}</a></span>", _path + "?" + string.Format(result, page + (show + 1)), "..");

                }
                if (page < total)
                {
                    sb.AppendFormat("<span><a href=\"{0}\" title=\"下一页\">{1}</a></span>", _path + "?" + string.Format(result, page + 1), ">");
                    sb.AppendFormat("<span><a href=\"{0}\" title=\"最后一页\">{1}</a></span>", _path + "?" + string.Format(result, total), ">>");

                }
                sb.AppendFormat("<span class=\"current\">共{1}页</span>", page, total);
                sb.Append("</div>");
                return sb.ToString();
            }
        }


        //

        public static string CreatePage(this HtmlHelper html, string urlFormat, int currentPage, int pageSize, int recordCount, string jsFun, int displayPageCount = 10)
        {
            int pageCount = (recordCount + pageSize - 1) / pageSize;

            if (pageCount < 2)
            {
                return string.Empty;
            }

            StringBuilder sb = new StringBuilder();
            if (currentPage == 1)
            {
                sb.Append("<span class=\"pageNo\">首 页</span><span class=\"pageNo\">上一页</span>");
            }
            else
            {
                sb.AppendFormat("<a class=\"pagePre\" onclick=\"showPage(1)\">首 页</a><a class=\"pagePre\" onclick=\"{0}\">上一页</a>",
                    string.Format(jsFun, currentPage - 1));
            }

            var pageNumber = new List<int>();

            pageNumber.Add(1);
            pageNumber.Add(currentPage);
            pageNumber.Add(pageCount);

            int flag = 0;
            while (flag <= displayPageCount)
            {
                flag++;
                int prev = currentPage - flag;
                if (prev > 0)
                {
                    if (!pageNumber.Contains(prev))
                    {
                        pageNumber.Add(prev);
                    }

                    if (pageNumber.Count >= displayPageCount)
                    {
                        break;
                    }
                }

                int next = currentPage + flag;
                if (next < (pageCount + 1))
                {
                    if (!pageNumber.Contains(next))
                    {
                        pageNumber.Add(next);
                    }

                    if (pageNumber.Count >= displayPageCount)
                    {
                        break;
                    }
                }
            }

            pageNumber = pageNumber.Distinct().ToList();

            pageNumber.Sort();

            foreach (var num in pageNumber)
            {
                if (num == currentPage)
                {
                    sb.AppendFormat("<b class=\"pageCurrent\">{0}</b>", num);
                }
                else
                {
                    sb.AppendFormat("<a onclick=\"{0}\">{1}</a>",
                        string.Format(jsFun, num), num);
                }

                if (num == 1)
                {
                    if (pageNumber[1] != 2 && pageCount != 1)
                    {
                        sb.Append("<b>…</b>");
                    }
                }

                if (num == pageNumber[pageNumber.Count - 2])
                {
                    if (num + 1 != pageNumber[pageNumber.Count - 1])
                    {
                        sb.Append("<b>…</b>");
                    }
                }
            }

            if (currentPage == pageCount)
            {
                sb.Append("<span class=\"pageNo\">下一页</span><span class=\"pageNo\">尾 页</span>");
            }
            else
            {
                sb.AppendFormat("<a class=\"pageNext\" onclick=\"{0}\">下一页</a><a class=\"pageNext\" onclick=\"showPage(" + pageCount + ")\">尾 页</a>",
                    string.Format(jsFun, currentPage + 1));
            }

            return sb.ToString();
        }
    }
}