// ==UserScript==
// @name        unlimited-wldx wldx.sgcc.com.cn
// @namespace   Violentmonkey Scripts
// @match       *://wldx.sgcc.com.cn/*
// @grant       unsafeWindow
// @grant       GM_xmlhttpRequest
// @version     1.0
// @author      shaoziei zyson_wong
// @description 6/22/2023, 4:39:33 PM
// @require     http://127.0.0.1:1995/vendor/jquery/jquery.min.js
// ==/UserScript==

/*
1. 页面内容可以复制。
2. 通过API提交题目、选项，自动匹配相关答案。

请自行在Firefox/Chrome/Edge/Brave等浏览器中安装“ViolentMonkey”、“TamperMonkey”或“GreaseMoney”相关插件。
选一个就行，推荐使用开源的ViolentMonkey + Firefox
*/

// (function() {
//     'use strict';
//      $("pre,code").css("user-select","auto");
// })();

function allowSelect() {
    'use strict';
    $("pre,code").css("user-select","auto");
}

$(document).ready(function() {
    allowSelect();
    
});