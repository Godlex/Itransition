#pragma checksum "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "acb9b2869694c758d314954b8ae2ffe032ba54e9"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Home_GetTable), @"mvc.1.0.view", @"/Views/Home/GetTable.cshtml")]
namespace AspNetCore
{
    #line hidden
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;
    using Microsoft.AspNetCore.Mvc;
    using Microsoft.AspNetCore.Mvc.Rendering;
    using Microsoft.AspNetCore.Mvc.ViewFeatures;
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"acb9b2869694c758d314954b8ae2ffe032ba54e9", @"/Views/Home/GetTable.cshtml")]
    public class Views_Home_GetTable : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<IEnumerable<UserAuthentication.WEB.Controllers.UserView>>
    {
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("\r\n<table class=\"table\">\r\n    <tr>\r\n        <th>\r\n            ");
#nullable restore
#line 6 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
       Write(Html.DisplayNameFor(model => model.LoginProvider));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th>\r\n            ");
#nullable restore
#line 9 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
       Write(Html.DisplayNameFor(model => model.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th>\r\n            ");
#nullable restore
#line 12 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
       Write(Html.DisplayNameFor(model => model.Active));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th>\r\n            ");
#nullable restore
#line 15 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
       Write(Html.DisplayNameFor(model => model.FirstLogin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th>\r\n            ");
#nullable restore
#line 18 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
       Write(Html.DisplayNameFor(model => model.LastLogin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n        </th>\r\n        <th></th>\r\n    </tr>\r\n\r\n\r\n");
#nullable restore
#line 24 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
     foreach (var item in Model)
    {

#line default
#line hidden
#nullable disable
            WriteLiteral("        <tr>\r\n            <td>\r\n                ");
#nullable restore
#line 28 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
           Write(Html.DisplayFor(modelItem => item.LoginProvider));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 31 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
           Write(Html.DisplayFor(modelItem => item.Email));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 34 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
           Write(Html.DisplayFor(modelItem => item.Active));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 37 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
           Write(Html.DisplayFor(modelItem => item.FirstLogin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n            <td>\r\n                ");
#nullable restore
#line 40 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
           Write(Html.DisplayFor(modelItem => item.LastLogin));

#line default
#line hidden
#nullable disable
            WriteLiteral("\r\n            </td>\r\n        </tr>\r\n");
#nullable restore
#line 43 "D:\Itransition\Task4_UserAuthentication\UserAuthentication.WEB\Views\Home\GetTable.cshtml"
    }

#line default
#line hidden
#nullable disable
            WriteLiteral("</table>");
        }
        #pragma warning restore 1998
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.ViewFeatures.IModelExpressionProvider ModelExpressionProvider { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IUrlHelper Url { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.IViewComponentHelper Component { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IJsonHelper Json { get; private set; }
        [global::Microsoft.AspNetCore.Mvc.Razor.Internal.RazorInjectAttribute]
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<IEnumerable<UserAuthentication.WEB.Controllers.UserView>> Html { get; private set; }
    }
}
#pragma warning restore 1591
