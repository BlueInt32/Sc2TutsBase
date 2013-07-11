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
		public static MvcHtmlString JquerySelectableListFor<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TEnum>>> expression, object htmlAttributes)
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
			sb.AppendFormat("<li class=\"ui-widget-content header\"><a>{0}</a></li>", metadata.DisplayName);
			foreach (TEnum value in values)
			{
				var memInfo = type.GetMember(value.ToString());
				var attributes = memInfo[0].GetCustomAttributes(typeof(TokenAttribute),false);
				var token = ((TokenAttribute)attributes[0]).Token;
				sb.AppendFormat("<li class=\"ui-widget-content actualitem {1}\" data-token='{2}'>{0}</li>", value, (metadata.Model != null && ((List<TEnum>)metadata.Model).Contains(value)) ? "ui-selected" : string.Empty, token);
			}
			sb.Append("</ol></div>");

			return new MvcHtmlString(sb.ToString());
		}

		public static MvcHtmlString JqueryCodeForList<TModel, TEnum>(this HtmlHelper<TModel> htmlHelper, Expression<Func<TModel, List<TEnum>>> expression, string jsCallbackSelectedFunctionName)
		{
			ModelMetadata metadata = ModelMetadata.FromLambdaExpression(expression, htmlHelper.ViewData);
			return new MvcHtmlString(string.Format("$('#{0}').bind( \"mousedown\", function ( e ) {{e.metaKey = true;}} ).selectable({{filter: \".actualitem\", stop:{1} }});\n", metadata.PropertyName, jsCallbackSelectedFunctionName));
		}

	}
}