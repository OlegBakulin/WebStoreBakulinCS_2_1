using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc.ViewFeatures;
using Microsoft.AspNetCore.Razor.TagHelpers;

namespace WebStoreCoreApplication.TagHelpers
{
    [HtmlTargetElement(Attributes = AttributeName)]
    public class IsActiveRoutTagHelper : TagHelper
    {
        private const string AttributeName = "is-active-rout";
        
        
        [HtmlAttributeName("asp-action")]
        public string Action { get; set; }


        [HtmlAttributeName("asp-controller")]
        public string Controller { get; set; }


        [HtmlAttributeName("asp-all-route-data", DictionaryAttributePrefix = "asp-route-")]
        public Dictionary<string, string> RoutValue { get; set; } = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);


        [ViewContext, HtmlAttributeNotBound]
        public ViewContext ViewContext { get; set; }


        public override void Process(TagHelperContext context, TagHelperOutput output)
        {
            if (IsActive()) MakeActive(output);
            output.Attributes.RemoveAll(AttributeName);
        }

        private void MakeActive(TagHelperOutput output)
        {
            var class_attribut = output.Attributes.FirstOrDefault(att => att.Name == "class");

            if (class_attribut is null)
                output.Attributes.Add("class", "active");
            else
            {
                if(class_attribut.Value.ToString()?.Contains("active") ?? false)
                    return;
                output.Attributes.SetAttribute("class", class_attribut.Value + " active");
            }

        }

        private bool IsActive()
        {
            var route_value = ViewContext.RouteData.Values;

            var current_controller = route_value["Controller"].ToString();
            var current_action = route_value["Action"].ToString();

            const StringComparison str_com = StringComparison.OrdinalIgnoreCase;
            
            if (!string.IsNullOrEmpty(Controller) && !string.Equals(current_controller, Controller, str_com))
                return false;

            if (!string.IsNullOrEmpty(Action) && !string.Equals(current_action, Action, str_com))
                return false;
            foreach (var (key, value) in RoutValue)
                if (!route_value.ContainsKey(key) || route_value[key]?.ToString() != value)
                    return false;

            return true;
        }
    }
}
