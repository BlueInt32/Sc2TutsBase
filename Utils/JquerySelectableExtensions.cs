using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sc2TutsBase.Utils
{
	public static class JquerySelectableExtensions
	{
		public static MvcHtmlString JquerySelectableListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression, object htmlAttributes)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();
			// 

			IEnumerable<SelectListItem> items =
				values.Select(value => new SelectListItem
				{
					Text = value.ToString(),
					Value = value.ToString(),
					Selected = value.Equals(metadata.Model)
				});

			StringBuilder sb = new StringBuilder();
			sb.AppendFormat("<div class='filteritem'>");
			//sb.AppendFormat("<h5>{0}</h5>", metadata.DisplayName);
			sb.AppendFormat("<ol class=\"selectable\" id=\"{0}\">", metadata.PropertyName);
			sb.AppendFormat("<li class=\"ui-widget-content header\">{0}</li>", metadata.DisplayName);
			foreach (TEnum value in values)
			{
				sb.AppendFormat("<li class=\"ui-widget-content actualitem\">{0}</li>", value);
			}
			sb.Append("</ol></div>");

			return new MvcHtmlString(sb.ToString());
		}

		public static MvcHtmlString JqueryCodeForList<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, TEnum>> expression)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			return new MvcHtmlString(string.Format("<script>$('#{0}').selectable({{filter: \".actualitem\"}});</script>", metadata.PropertyName));
		}

	}
}