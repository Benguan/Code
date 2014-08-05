using System;
using System.Collections.Generic;
using System.Web.Mvc;
using System.Web.Mvc.Html;
using System.Web.Routing;

namespace NEG.Website.Controls.Common
{
    public static class Helper
    {
        public static MvcHtmlString ActionLinkWithTag(this HtmlHelper html, string tagName, string text,
                                                      string actionName, string controllerName, RouteValueDictionary routeValue = null,
                                                      IDictionary<string, Object> tagHtmlAttributes = null,
                                                      IDictionary<string, Object> htmlAttributes = null)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);


            TagBuilder tagBuilder = new TagBuilder(tagName);
            tagBuilder.SetInnerText(text);


            if (tagHtmlAttributes != null)
            {
                tagBuilder.MergeAttributes(tagHtmlAttributes);
            }

            TagBuilder aBuilder = new TagBuilder("a")
                {
                    InnerHtml = tagBuilder.ToString(TagRenderMode.Normal)
                };

            if (routeValue != null)
            {
                string url = urlHelper.Action(actionName, controllerName, routeValue);
                aBuilder.MergeAttribute("href", url);
            }


            return new MvcHtmlString(aBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkWithImage(this HtmlHelper html, string imgSrc, string actionName)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            string imgUrl = urlHelper.Content(imgSrc);
            TagBuilder imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", imgUrl);
            string img = imgTagBuilder.ToString(TagRenderMode.SelfClosing);

            string url = urlHelper.Action(actionName);

            TagBuilder tagBuilder = new TagBuilder("a")
            {
                InnerHtml = img
            };
            tagBuilder.MergeAttribute("href", url);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionLinkWithImage(this HtmlHelper html, string imgSrc, string actionName, string controllerName, object routeValue = null)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            string imgUrl = urlHelper.Content(imgSrc);
            TagBuilder imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", imgUrl);
            string img = imgTagBuilder.ToString(TagRenderMode.SelfClosing);

            string url = urlHelper.Action(actionName, controllerName, routeValue);

            TagBuilder tagBuilder = new TagBuilder("a")
                {
                    InnerHtml = img
                };
            tagBuilder.MergeAttribute("href", url);

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }

        public static MvcHtmlString ActionHover(this HtmlHelper html, string imgSrc, string actionName, string controllerName, string className, object routeValue = null, string id = null)
        {
            var urlHelper = new UrlHelper(html.ViewContext.RequestContext);

            string imgUrl = urlHelper.Content(imgSrc);
            TagBuilder imgTagBuilder = new TagBuilder("img");
            imgTagBuilder.MergeAttribute("src", imgUrl);
            imgTagBuilder.MergeAttribute("width", "175");
            imgTagBuilder.MergeAttribute("height", "144");
            string img = imgTagBuilder.ToString(TagRenderMode.SelfClosing);

            string url = urlHelper.Action(actionName, controllerName, new {id});

            TagBuilder tagBuilderDiv = new TagBuilder("div")
                {
                    InnerHtml = img + id
                };

            if (!string.IsNullOrWhiteSpace(className))
            {
                tagBuilderDiv.MergeAttribute("class", className);
            }

            TagBuilder tagBuilder = new TagBuilder("a")
                {
                    InnerHtml = tagBuilderDiv.ToString(TagRenderMode.Normal)
                };
            tagBuilder.MergeAttribute("href", url);
            tagBuilder.MergeAttribute("target", "_black");

            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));
        }


        public static MvcHtmlString TabLink(this HtmlHelper html, string linkText, string actionName, string controllerName, string activeClassName = "hover")
        {
            //get controller && action
            var controller = html.ViewContext.RouteData.Values["controller"];
            var action = html.ViewContext.RouteData.Values["action"];

            if (controller == null || action == null)
            {
                return html.ActionLink(linkText, actionName, controllerName);
            }

            //if on current page, hight light tab
            if (controller.ToString() == controllerName && action.ToString() == actionName)
            {
                return html.ActionLink(linkText, actionName, controllerName, new {@class = activeClassName});
            }
            else
            {
                return html.ActionLink(linkText, actionName, controllerName);
            }
        }

        public static MvcHtmlString Span(this HtmlHelper html, string text)
        {
            TagBuilder tagBuilder = new TagBuilder("span")
                {
                    InnerHtml = text
                };


            return new MvcHtmlString(tagBuilder.ToString(TagRenderMode.Normal));

        }

    }
}