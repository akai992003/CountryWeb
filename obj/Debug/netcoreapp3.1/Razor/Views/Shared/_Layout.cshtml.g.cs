#pragma checksum "/Users/hsiaochingkao/Source/HN/CountryWeb/Views/Shared/_Layout.cshtml" "{ff1816ec-aa5e-4d10-87f7-6f4963833460}" "7ca9b16ef1364dd1261c37db5afac3e6adfe167c"
// <auto-generated/>
#pragma warning disable 1591
[assembly: global::Microsoft.AspNetCore.Razor.Hosting.RazorCompiledItemAttribute(typeof(AspNetCore.Views_Shared__Layout), @"mvc.1.0.view", @"/Views/Shared/_Layout.cshtml")]
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
#nullable restore
#line 1 "/Users/hsiaochingkao/Source/HN/CountryWeb/Views/_ViewImports.cshtml"
using CountryWeb;

#line default
#line hidden
#nullable disable
#nullable restore
#line 2 "/Users/hsiaochingkao/Source/HN/CountryWeb/Views/_ViewImports.cshtml"
using CountryWeb.Models;

#line default
#line hidden
#nullable disable
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"7ca9b16ef1364dd1261c37db5afac3e6adfe167c", @"/Views/Shared/_Layout.cshtml")]
    [global::Microsoft.AspNetCore.Razor.Hosting.RazorSourceChecksumAttribute(@"SHA1", @"0f1a8749cf0e514129de2084ffe1051157ab22d5", @"/Views/_ViewImports.cshtml")]
    public class Views_Shared__Layout : global::Microsoft.AspNetCore.Mvc.Razor.RazorPage<dynamic>
    {
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_0 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("dropdown-item"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_1 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("title", new global::Microsoft.AspNetCore.Html.HtmlString("重大事記"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_2 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-controller", "Home", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_3 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "importevent", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_4 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-area", "", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_5 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("asp-action", "Privacy", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_6 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/jquery/dist/jquery.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_7 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", new global::Microsoft.AspNetCore.Html.HtmlString("~/lib/bootstrap/dist/js/bootstrap.bundle.min.js"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_8 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("src", "~/js/site.js", global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        private static readonly global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute __tagHelperAttribute_9 = new global::Microsoft.AspNetCore.Razor.TagHelpers.TagHelperAttribute("class", new global::Microsoft.AspNetCore.Html.HtmlString("bg-white"), global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
        #line hidden
        #pragma warning disable 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperExecutionContext __tagHelperExecutionContext;
        #pragma warning restore 0649
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner __tagHelperRunner = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperRunner();
        #pragma warning disable 0169
        private string __tagHelperStringValueBuffer;
        #pragma warning restore 0169
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __backed__tagHelperScopeManager = null;
        private global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager __tagHelperScopeManager
        {
            get
            {
                if (__backed__tagHelperScopeManager == null)
                {
                    __backed__tagHelperScopeManager = new global::Microsoft.AspNetCore.Razor.Runtime.TagHelpers.TagHelperScopeManager(StartTagHelperWritingScope, EndTagHelperWritingScope);
                }
                return __backed__tagHelperScopeManager;
            }
        }
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper;
        private global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper;
        private global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper;
        #pragma warning disable 1998
        public async override global::System.Threading.Tasks.Task ExecuteAsync()
        {
            WriteLiteral("<!DOCTYPE html>\r\n<html lang=\"zh-tw\">\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("head", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ca9b16ef1364dd1261c37db5afac3e6adfe167c6891", async() => {
                WriteLiteral(@"
    <meta charset=""utf-8"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=Edge"">
    <meta http-equiv=""X-UA-Compatible"" content=""IE=9"">

    <meta name=""viewport"" content=""width=device-width, initial-scale=1"">
    <!-- CSRF Token -->
    <meta name=""csrf-token"" content=""B7ZHDTnFLZsv54wORrnPMqEdMysVcJf2mCSIGcbT"">
    <title>宏恩醫療財團法人宏恩綜合醫院</title>
    <meta name=""description""
        content=""本院提供各項醫療保健相關的服務，以及協助社區民眾落實健康的生活型態。對於地方上各種有醫療保健活動，皆主動予以關切，並就專業之立場提供醫療諮詢和意見，並結合社區內各醫療系統，以提昇社區醫療保健水準。"">
    <meta property=""og:url"" content=""http://country.org.tw/"" />
    <meta property=""og:type"" content=""website"" />
    <meta property=""og:title"" content=""宏恩醫療財團法人宏恩綜合醫院"" />
    <meta property=""og:description""
        content=""本院提供各項醫療保健相關的服務，以及協助社區民眾落實健康的生活型態。對於地方上各種有醫療保健活動，皆主動予以關切，並就專業之立場提供醫療諮詢和意見，並結合社區內各醫療系統，以提昇社區醫療保健水準。"" />
    <meta property=""og:image"" content=""/img/og_logo.png"" />
    <meta property=""og:image:height"" content=""300"" />
    <meta property=""og:image:width"" content=""300"" />

    <!--");
                WriteLiteral(@" Favicon -->
    <link rel=""shortcut icon"" href=""/favicon.ico"">
    <!-- Web Font -->
    <link rel=""stylesheet"" href=""https://fonts.googleapis.com/css?family=Noto+Sans+TC:100,400,700,900&display=swap"">
    <!-- Main CSS -->
    <link rel=""stylesheet"" href=""/css/app.min.css"">
    <!-- Fontawesome -->
    <link rel=""stylesheet"" href=""/css/fontawesome/all.min.css"">
    <!--[if IE 9]>
    <link rel=""stylesheet"" href=""https://cdn.jsdelivr.net/npm/bootstrap@3.3.7/dist/css/bootstrap.min.css""
          integrity=""sha384-BVYiiSIFeK1dGmJRAkycuHAHRg32OmUcww7on3RYdg4Va+PmSTsz/K68vbdEjh4u"" crossorigin=""anonymous"">
    <![endif]-->
    <!-- Smart Menu -->
    <link rel=""stylesheet"" href=""/css/menu/sm-core-css.css"">
    <link rel=""stylesheet"" href=""/css/menu/jquery.smartmenus.bootstrap-4.min.css"">
    <!-- Flex Slider -->
    <link rel=""stylesheet"" href=""/css/flexslider.min.css"">
    <!-- Swiper -->
    <link rel=""stylesheet"" href=""/css/swiper.min.css"">
    <!-- Custom Css -->
    <link rel=""stylesheet""");
                WriteLiteral(@" href=""/css/custom.min.css"">
    <!-- Main JS -->
    <script src=""/js/app.min.js""></script>
    <!-- Swipter -->
    <script src=""https://cdnjs.cloudflare.com/ajax/libs/Swiper/3.3.1/js/swiper.min.js""></script>
    <!-- Custom Js -->
    <script async src=""/js/custom.js""></script>
    <!-- Smart Menu -->
    <script defer src=""/js/menu/jquery.smartmenus.min.js""></script>
    <script defer src=""/js/menu/jquery.smartmenus.bootstrap-4.min.js""></script>
    <!-- Flex Slider -->
    <script defer src=""/js/jquery.flexslider-min.js""></script>
    <!-- Zoom Effect Js -->
    <script defer src=""/js/wheelzoom.min.js""></script>

    <!-- Global site tag (gtag.js) - Google Analytics -->
    <script async src=""https://www.googletagmanager.com/gtag/js?id=UA-142686848-1""></script>
    <script>
        window.dataLayer = window.dataLayer || [];
        function gtag() { dataLayer.push(arguments); }
        gtag('js', new Date());

        gtag('config', 'UA-142686848-1');
    </script>
");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.HeadTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_HeadTagHelper);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n\r\n");
            __tagHelperExecutionContext = __tagHelperScopeManager.Begin("body", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ca9b16ef1364dd1261c37db5afac3e6adfe167c11085", async() => {
                WriteLiteral(@"
    <!-- Google Tag Manager (noscript) -->
    <noscript><iframe src=""https://www.googletagmanager.com/ns.html?id=GTM-PNPW8SP"" height=""0"" width=""0""
            style=""display:none;visibility:hidden""></iframe></noscript>
    <!-- End Google Tag Manager (noscript) -->
    <!-- Back to Top -->
    <a id=""back-to-top"" href=""#"" class=""btn btn-primary back-to-top"" role=""button""><i
            class=""fas fa-angle-up fa-lg""></i></a>
    <!-- Header -->
    <header class=""shadow fixed-top"">
        <div class=""bg-white navbar-dark"">
            <div class=""container py-2"">
                <div class=""row align-items-center"">
                    <div class=""col-6"">
                        <a href=""https://www.country.org.tw"" title=""宏恩醫療財團法人宏恩綜合醫院"">
                            <img src=""/Logo.png"" width=""280"" height=""100%"" alt=""宏恩醫療財團法人宏恩綜合醫院"">
                        </a>
                    </div>
                    <div class=""header-links col-6"">
                        <div class=""row float-rig");
                WriteLiteral(@"ht"">
                            <div class=""align-self-center d-none d-md-block"">

                                <a class=""px-2"" href=""https://www.country.org.tw/covid19""
                                    title=""Covid19專區"">Covid19專區</a>

                                <a class=""px-2"" href=""https://www.country.org.tw"" title=""首頁"">首頁</a>
                                <a class=""px-2"" href=""https://www.country.org.tw/網站地圖"" title=""網站地圖"">網站地圖</a>
                                <a id=""back-to-bottom"" class=""px-2"" href=""#"" role=""button"" title=""聯絡我們"">聯絡我們</a>
                                <a class=""badge badge-secondary p-2"" href=""https://www.country.org.tw/old/inter""
                                    title=""國際醫療""><i class=""fas fa-hospital-symbol pr-2""></i>國際醫療</a>
                                <a class=""badge badge-secondary p-2 m-2"" href=""https://www.country.org.tw/set/en""
                                    title=""English""><i class=""fas fa-globe pr-2""></i>English</a>
                     ");
                WriteLiteral(@"       </div>
                            <button class=""navbar-toggler text-primary d-md-none d-sm-block mx-2"" type=""button""
                                data-toggle=""collapse"" data-target=""#navbar"" aria-expanded=""false"">
                                <span class=""navbar-toggler-icon"">
                                    <i class=""fas fa-bars navbar-vertical text-primary""></i>
                                </span>
                            </button>
                        </div>
                    </div>

                </div>
            </div>
        </div>

        <!-- Navbar -->
        <nav class=""navbar navbar-expand-md navbar-dark bg-primary"">
            <div class=""container"">
                <div class=""collapse navbar-collapse"" id=""navbar"">
                    <ul class=""nav navbar-nav col-md-2 col-sm-12 justify-content-center"">
                        <li class=""nav-item dropdown""><a class=""nav-link dropdown-toggle text-white"" href=""#""
                           ");
                WriteLiteral(@"     title=""訊息專區"">
                                <div class=""text-center pr-3"">訊息專區</div>
                            </a>
                            <ul class=""dropdown-menu"">
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/訊息專區/最新消息""
                                        title=""最新消息"">最新消息</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/訊息專區/公告訊息""
                                        title=""公告訊息"">公告訊息</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/訊息專區/得獎訊息""
                                        title=""得獎訊息"">得獎訊息</a></li>
                            </ul>
                        </li>
                    </ul>
                    <div class=""text-white d-none d-md-block"">|</div>
                    <ul class=""nav navbar-nav col-md-2 col-sm-12 justify-content-center"">
                        <li class=""nav-item dropdown""><a class=");
                WriteLiteral("\"nav-link dropdown-toggle text-white\" href=\"#\">\r\n                                <div class=\"text-center pr-3\">認識宏恩</div>\r\n                            </a>\r\n                            <ul class=\"dropdown-menu\">\r\n");
                WriteLiteral("                                <li>\r\n");
                WriteLiteral("                                    <a class=\"dropdown-item\" title=\"tel\" href=\"/Home/tel\">tel</a>\r\n");
                WriteLiteral(@"                                </li>
                                <li>
                                    <a class=""dropdown-item"" href=""https://www.country.org.tw/認識宏恩/管理團隊""
                                        title=""管理團隊"">管理團隊</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/認識宏恩/願景使命""
                                        title=""願景使命"">願景使命</a></li>
                                <li>
");
                WriteLiteral("                                    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ca9b16ef1364dd1261c37db5afac3e6adfe167c16748", async() => {
                    WriteLiteral("\r\n                                        重大事記\r\n                                    ");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_0);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_1);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_3.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_3);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n");
                WriteLiteral(@"                                </li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/認識宏恩/組織架構""
                                        title=""組織架構"">組織架構</a></li>
                            </ul>
                        </li>
                    </ul>
                    <div class=""text-white d-none d-md-block"">|</div>
                    <ul class=""nav navbar-nav col-md-2 col-sm-12 justify-content-center"">
                        <li class=""nav-item dropdown""><a class=""nav-link dropdown-toggle text-white"" href=""#""
                                title=""就醫指南"">
                                <div class=""text-center pr-3"">就醫指南</div>
                            </a>
                            <ul class=""dropdown-menu"">
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/網路掛號""
                                        title=""網路掛號"">網路掛號</a></li>
                                <li><a class=""dropdown-item""");
                WriteLiteral(@" href=""https://www.country.org.tw/就醫指南/掛號查詢""
                                        title=""網路掛號"">掛號查詢</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/看診進度""
                                        title=""看診進度"">看診進度</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/門診時間""
                                        title=""門診時間"">門診時間</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/醫師停診""
                                        title=""醫師停診"">醫師停診</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/看哪一科""
                                        title=""看哪一科"">看哪一科</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/就醫權益與須知""
                                        title=""就醫權益與須知"">就醫權益與須知</a></li>
                               ");
                WriteLiteral(@" <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/住院服務""
                                        title=""住院服務"">住院服務</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/急診服務""
                                        title=""急診服務"">急診服務</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/就醫指南/收費說明""
                                        title=""收費說明"">收費說明</a></li>
                            </ul>
                        </li>
                    </ul>
                    <div class=""text-white d-none d-md-block"">|</div>
                    <ul class=""nav navbar-nav col-md-2 col-sm-12 justify-content-center"">
                        <li class=""nav-item dropdown""><a class=""nav-link dropdown-toggle text-white"" href=""#""
                                title=""醫療中心"">
                                <div class=""text-center pr-3"">醫療中心</div>
                            </a>
            ");
                WriteLiteral(@"                <ul class=""dropdown-menu"">
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/醫療中心/醫療團隊""
                                        title=""醫療團隊"">醫療團隊</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/醫療中心/醫事團隊""
                                        title=""醫事團隊"">醫事團隊</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/醫療中心/特色醫療""
                                        title=""特色醫療"">特色醫療</a></li>
                            </ul>
                        </li>
                    </ul>
                    <div class=""text-white d-none d-md-block"">|</div>
                    <ul class=""nav navbar-nav col-md-2 col-sm-12 justify-content-center"">
                        <li class=""nav-item dropdown""><a class=""nav-link dropdown-toggle text-white"" href=""#""
                                title=""衛教園地"">
                                <div class=""t");
                WriteLiteral(@"ext-center pr-3"">衛教園地</div>
                            </a>
                            <ul class=""dropdown-menu"">
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/衛教園地/衛教單張""
                                        title=""衛教單張"">衛教單張</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/衛教園地/防疫新知""
                                        title=""防疫新知"">防疫新知</a></li>


                            </ul>
                        </li>
                    </ul>
                    <div class=""text-white d-none d-md-block"">|</div>
                    <ul class=""nav navbar-nav col-md-2 col-sm-12 justify-content-center"">
                        <li class=""nav-item dropdown""><a class=""nav-link dropdown-toggle text-white"" href=""#""
                                title=""為民服務"">
                                <div class=""text-center pr-3"">為民服務</div>
                            </a>
                            <ul ");
                WriteLiteral(@"class=""dropdown-menu"">
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/為民服務/就醫資料申請""
                                        title=""就醫資料申請"">就醫資料申請</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/為民服務/社會服務""
                                        title=""社會服務"">社會服務</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/為民服務/病友分享""
                                        title=""病友分享"">病友分享</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/為民服務/滿意度調查""
                                        title=""滿意度調查"">滿意度調查</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/為民服務/到院指南""
                                        title=""到院指南"">到院指南</a></li>
                                <li><a class=""dropdown-item"" href=""https://www.country.org.tw/為民服務/常見問題""
               ");
                WriteLiteral(@"                         title=""常見問題"">常見問題</a></li>
                            </ul>
                        </li>
                    </ul>
                    <ul class=""nav navbar-nav d-md-none d-sm-block justify-content-center"">
                        <li class=""nav-item"">
                            <a class=""nav-link text-white"" href=""https://www.country.org.tw/old/inter"" title=""國際醫療"">
                                <div class=""text-center pr-3""><i class=""fas fa-hospital-symbol pr-2""></i>國際醫療</div>
                            </a>
                        </li>
                    </ul>
                    <ul class=""nav navbar-nav d-md-none d-sm-block justify-content-center"">
                        <li class=""nav-item"">
                            <a class=""nav-link text-white"" href=""https://www.country.org.tw/set/en"" title=""English"">
                                <div class=""text-center pr-3""><i class=""fas fa-globe pr-2""></i>English</div>
                            </a>
         ");
                WriteLiteral("               </li>\r\n                    </ul>\r\n                </div>\r\n            </div>\r\n        </nav>\r\n\r\n\r\n    </header>\r\n    <div class=\"container\">\r\n        <main role=\"main\" class=\"pb-3\">\r\n            ");
#nullable restore
#line 274 "/Users/hsiaochingkao/Source/HN/CountryWeb/Views/Shared/_Layout.cshtml"
       Write(RenderBody());

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n        </main>\r\n    </div>\r\n\r\n    <footer class=\"border-top footer text-muted\">\r\n        <div class=\"container\">\r\n            &copy; 2021 - CountryWeb - ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("a", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ca9b16ef1364dd1261c37db5afac3e6adfe167c26705", async() => {
                    WriteLiteral("Privacy");
                }
                );
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.AnchorTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Area = (string)__tagHelperAttribute_4.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_4);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Controller = (string)__tagHelperAttribute_2.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_2);
                __Microsoft_AspNetCore_Mvc_TagHelpers_AnchorTagHelper.Action = (string)__tagHelperAttribute_5.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_5);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n        </div>\r\n    </footer>\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ca9b16ef1364dd1261c37db5afac3e6adfe167c28367", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_6);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ca9b16ef1364dd1261c37db5afac3e6adfe167c29453", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_7);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
                __tagHelperExecutionContext = __tagHelperScopeManager.Begin("script", global::Microsoft.AspNetCore.Razor.TagHelpers.TagMode.StartTagAndEndTag, "7ca9b16ef1364dd1261c37db5afac3e6adfe167c30539", async() => {
                }
                );
                __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.UrlResolutionTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_UrlResolutionTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.TagHelpers.ScriptTagHelper>();
                __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper);
                __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.Src = (string)__tagHelperAttribute_8.Value;
                __tagHelperExecutionContext.AddTagHelperAttribute(__tagHelperAttribute_8);
#nullable restore
#line 285 "/Users/hsiaochingkao/Source/HN/CountryWeb/Views/Shared/_Layout.cshtml"
__Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion = true;

#line default
#line hidden
#nullable disable
                __tagHelperExecutionContext.AddTagHelperAttribute("asp-append-version", __Microsoft_AspNetCore_Mvc_TagHelpers_ScriptTagHelper.AppendVersion, global::Microsoft.AspNetCore.Razor.TagHelpers.HtmlAttributeValueStyle.DoubleQuotes);
                await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
                if (!__tagHelperExecutionContext.Output.IsContentModified)
                {
                    await __tagHelperExecutionContext.SetOutputContentAsync();
                }
                Write(__tagHelperExecutionContext.Output);
                __tagHelperExecutionContext = __tagHelperScopeManager.End();
                WriteLiteral("\r\n    ");
#nullable restore
#line 286 "/Users/hsiaochingkao/Source/HN/CountryWeb/Views/Shared/_Layout.cshtml"
Write(RenderSection("Scripts", required: false));

#line default
#line hidden
#nullable disable
                WriteLiteral("\r\n");
            }
            );
            __Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper = CreateTagHelper<global::Microsoft.AspNetCore.Mvc.Razor.TagHelpers.BodyTagHelper>();
            __tagHelperExecutionContext.Add(__Microsoft_AspNetCore_Mvc_Razor_TagHelpers_BodyTagHelper);
            __tagHelperExecutionContext.AddHtmlAttribute(__tagHelperAttribute_9);
            await __tagHelperRunner.RunAsync(__tagHelperExecutionContext);
            if (!__tagHelperExecutionContext.Output.IsContentModified)
            {
                await __tagHelperExecutionContext.SetOutputContentAsync();
            }
            Write(__tagHelperExecutionContext.Output);
            __tagHelperExecutionContext = __tagHelperScopeManager.End();
            WriteLiteral("\r\n\r\n</html>\r\n");
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
        public global::Microsoft.AspNetCore.Mvc.Rendering.IHtmlHelper<dynamic> Html { get; private set; }
    }
}
#pragma warning restore 1591
