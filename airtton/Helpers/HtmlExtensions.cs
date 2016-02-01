using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using System.Web.Mvc.Html;
using System.Web.WebPages;


using System.Linq.Expressions;
using System.Web.Routing;
using System.Text;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace airtton.Helpers
{
    public static class HtmlExtensions
    {
        public static MvcHtmlString File(this HtmlHelper html, string name)
        {
            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);
            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }

        public static MvcHtmlString ToJson(this HtmlHelper html, object obj)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            return MvcHtmlString.Create(serializer.Serialize(obj));
        }

        public static MvcHtmlString ToJson(this HtmlHelper html, object obj, int recursionDepth)
        {
            JavaScriptSerializer serializer = new JavaScriptSerializer();
            serializer.RecursionLimit = recursionDepth;
            return MvcHtmlString.Create(serializer.Serialize(obj));
        }

        public static MvcHtmlString File(this HtmlHelper html, string name, object htmlAttributes)
        {
            var t = new RouteValueDictionary(htmlAttributes);

            var dd = htmlAttributes as string;

            var tb = new TagBuilder("input");
            tb.Attributes.Add("type", "file");
            tb.Attributes.Add("name", name);

            foreach (var param in t)
            {
                tb.Attributes.Add(param.Key, param.Value.ToString());
            }

            tb.GenerateId(name);
            return MvcHtmlString.Create(tb.ToString(TagRenderMode.SelfClosing));
        }


        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression)
        {
            string name = GetFullPropertyName(expression);
            return html.File(name);
        }

        public static MvcHtmlString FileFor<TModel, TProperty>(this HtmlHelper<TModel> html, Expression<Func<TModel, TProperty>> expression, object htmlAttributes)
        {
            string name = GetFullPropertyName(expression);
            return html.File(name, htmlAttributes);
        }



        public static string Label(this HtmlHelper helper, string target, string text, string hint)
        {
            return String.Format("<label for='{0}'>{1}</label>", target, text);

        }

        //        public static MvcHtmlString CustomEditorFor(this HtmlHelper html, Expression<Func> expr)
        //  {

        //     return new MvcHtmlString(
        //  @"<div class=\"control-group\">" + 
        //    html.LabelFor(
        //        expr, 
        //        new { @class = "col-md-2 control-label" }
        //     ).ToHtmlString() +
        //   @"</div>
        //     <div class=\"col-md-10\">" + 
        //    html.EditorFor(expr) + 
        //    "</div>");

        //  }

        public static MvcHtmlString DexaHintLabel(this HtmlHelper html, string name, string hint, object htmlAttributes)
        {
            string labelText = name;

            //if (String.IsNullOrEmpty(labelText))
            //    return MvcHtmlString.Empty;

            var label = new TagBuilder("label");
            label.Attributes.Add("for", name);

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(htmlAttributes))
            {
                // By adding the 'true' as the third parameter, you can overwrite whatever default attribute you have set earlier.
                label.MergeAttribute(prop.Name.Replace('_', '-'), prop.GetValue(htmlAttributes).ToString(), true);
            }



            var i = new TagBuilder("i");
            i.Attributes.Add("class", "fa fa-info-circle tooltips");
            i.Attributes.Add("data-container", "body");
            i.Attributes.Add("data-placement", "left");
            i.Attributes.Add("data-original-title", hint);

            var span = new TagBuilder("span");
            span.InnerHtml = i.ToString();

            if (labelText == null)
            {
                label.InnerHtml = i.ToString();
            }
            else
            {
                label.InnerHtml = string.Format("{0}: ", labelText) + i.ToString();
            }
            

            return MvcHtmlString.Create(label.ToString());

            //     <label class="control-label col-md-2 col-lg-2">
            //    Description: <span><i class="fa fa-info-circle tooltips" data-container="body" data-placement="top" data-original-title="Tooltip in top"></i></span>
            //</label>

        }


        public static MvcHtmlString DexaHintLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, string hint, object htmlAttributes)
        {

            var metaData = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);

            string htmlFieldName = ExpressionHelper.GetExpressionText(expression);
            string labelText = metaData.DisplayName ?? metaData.PropertyName ?? htmlFieldName.Split('.').Last();

            if (String.IsNullOrEmpty(labelText))
                return MvcHtmlString.Empty;

            var label = new TagBuilder("label");
            label.Attributes.Add("for", helper.ViewContext.ViewData.TemplateInfo.GetFullHtmlFieldId(htmlFieldName));

            foreach (PropertyDescriptor prop in TypeDescriptor.GetProperties(htmlAttributes))
            {
                // By adding the 'true' as the third parameter, you can overwrite whatever default attribute you have set earlier.
                label.MergeAttribute(prop.Name.Replace('_', '-'), prop.GetValue(htmlAttributes).ToString(), true);
            }
            
            

            var i = new TagBuilder("i");
            i.Attributes.Add("class", "fa fa-info-circle tooltips");
            i.Attributes.Add("data-container", "body");
            i.Attributes.Add("data-placement", "left");
            i.Attributes.Add("data-original-title", hint);

            var span = new TagBuilder("span");
            span.InnerHtml = i.ToString();

            label.InnerHtml = string.Format("{0}: ", labelText) + i.ToString();

            return MvcHtmlString.Create(label.ToString());

            //     <label class="control-label col-md-2 col-lg-2">
            //    Description: <span><i class="fa fa-info-circle tooltips" data-container="body" data-placement="top" data-original-title="Tooltip in top"></i></span>
            //</label>

        }

        //public static MvcHtmlString SmartLabelFor<TModel, TValue>(this HtmlHelper<TModel> helper, Expression<Func<TModel, TValue>> expression, bool displayHint = true, object htmlAttributes = null)
        //{
        //    var result = new StringBuilder();
        //    var metadata = ModelMetadata.FromLambdaExpression(expression, helper.ViewData);
        //    string labelText = null;
        //    string hint = null;

        //    SmartResourceDisplayName resourceDisplayName;
        //    object value = null;

        //    if (metadata.AdditionalValues.TryGetValue("SmartResourceDisplayName", out value))
        //    {
        //        resourceDisplayName = value as SmartResourceDisplayName;
        //        if (resourceDisplayName != null)
        //        {
        //            // resolve label display name
        //            labelText = resourceDisplayName.DisplayName.NullEmpty();
        //            if (labelText == null)
        //            {
        //                // take reskey as absolute fallback
        //                labelText = resourceDisplayName.ResourceKey;
        //            }

        //            // resolve hint
        //            if (displayHint)
        //            {
        //                var langId = EngineContext.Current.Resolve<IWorkContext>().WorkingLanguage.Id;
        //                hint = EngineContext.Current.Resolve<ILocalizationService>().GetResource(resourceDisplayName.ResourceKey + ".Hint", langId, false, "", true);
        //            }
        //        }
        //    }

        //    if (labelText == null)
        //    {
        //        labelText = metadata.PropertyName.SplitPascalCase();
        //    }

        //    var label = helper.LabelFor(expression, labelText, htmlAttributes);

        //    if (displayHint)
        //    {
        //        result.Append("<div class='ctl-label'>");
        //        {
        //            result.Append(label);
        //            if (hint.HasValue())
        //            {
        //                result.Append(helper.Hint(hint).ToHtmlString());
        //            }
        //        }
        //        result.Append("</div>");
        //    }
        //    else
        //    {
        //        result.Append(label);
        //    }

        //    return MvcHtmlString.Create(result.ToString());
        //}


        #region Helpers

        static string GetFullPropertyName<T, TProperty>(Expression<Func<T, TProperty>> exp)
        {
            MemberExpression memberExp;

            if (!TryFindMemberExpression(exp.Body, out memberExp))
                return string.Empty;

            var memberNames = new Stack<string>();

            do
            {
                memberNames.Push(memberExp.Member.Name);
            }
            while (TryFindMemberExpression(memberExp.Expression, out memberExp));

            return string.Join(".", memberNames.ToArray());
        }

        static bool TryFindMemberExpression(Expression exp, out MemberExpression memberExp)
        {
            memberExp = exp as MemberExpression;

            if (memberExp != null)
                return true;

            if (IsConversion(exp) && exp is UnaryExpression)
            {
                memberExp = ((UnaryExpression)exp).Operand as MemberExpression;

                if (memberExp != null)
                    return true;
            }

            return false;
        }

        static bool IsConversion(Expression exp)
        {
            return (exp.NodeType == ExpressionType.Convert || exp.NodeType == ExpressionType.ConvertChecked);
        }

        #endregion
    }
}