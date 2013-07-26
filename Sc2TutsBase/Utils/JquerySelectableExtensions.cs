using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Web;
using System.Web.Mvc;

namespace Sc2TutsBase.Utils
{
	public static class JquerySelectableExtensions
	{
		public static MvcHtmlString JquerySelectableListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TEnum>>> expression, object htmlAttributes) where TEnum : IConvertible
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			IEnumerable<TEnum> values = Enum.GetValues(typeof(TEnum)).Cast<TEnum>();

			var type = typeof(TEnum);
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
			sb.AppendFormat("<li class=\"ui-widget-content header\"><a class='staticLink'>{0}</a></li>", metadata.DisplayName);
			foreach (TEnum value in values)
			{
                string token = Utils.EnumHelper.GetToken<TEnum>(value);
				sb.AppendFormat("<li class=\"ui-widget-content actualitem {1}\" data-token='{2}'>{0}</li>", value, (metadata.Model != null && ((List<TEnum>)metadata.Model).Contains(value)) ? "ui-selected" : string.Empty, token);
			}
			sb.Append("</ol></div>");

			return new MvcHtmlString(sb.ToString());
		}

		public static MvcHtmlString JqueryCodeForList<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TEnum>>> expression, string jsCallbackSelectedFunctionName)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			return new MvcHtmlString(string.Format("$('#{0}').selectable({{filter: \".actualitem\", stop:{1} }});\n", metadata.PropertyName, jsCallbackSelectedFunctionName));
		}

	}
}