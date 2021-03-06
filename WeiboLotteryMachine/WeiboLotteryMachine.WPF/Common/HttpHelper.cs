﻿using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;

namespace WeiboLotteryMachine.WPF.Common
{
    class HttpHelper
    {
        /// <summary>
        /// 创建GET方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="myCookieContainer">随同HTTP请求发送的Cookie信息</param>
        /// <returns>返回字符串</returns>
        public static string Get(string url, CookieContainer myCookieContainer, bool autoRedirect)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.CookieContainer = myCookieContainer;
                request.AllowAutoRedirect = autoRedirect;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8);
                string retStr = sr.ReadToEnd();
                sr.Close();
                return retStr;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        /// <summary>
        /// 创建GET方式的HTTP请求
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <returns>返回字符串</returns>
        public static string Get(string url, bool isAutoRedirect = false)
        {
            try
            {

                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                request.AllowAutoRedirect = isAutoRedirect;
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                string retStr = "";
                using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
                {
                    retStr = sr.ReadToEnd();
                    sr.Close();
                }
                return retStr;
            }
            catch (Exception ex)
            {
                return "";
            }
        }

        ///<summary>
        /// 获得网页图片
        ///</summary>
        /// <param name="url">请求的URL</param>
        /// <returns>返回图像</returns>
        public static Image GetImage(string url)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.Method = "GET";
                WebResponse response = request.GetResponse();
                return Image.FromStream(response.GetResponseStream());
            }
            catch
            {
                return null;
            }
        }

        /// <summary>
        /// 创建POST方式的HTTP请求 
        /// </summary>
        /// <param name="url">请求的URL</param>
        /// <param name="postDataStr">发送的数据</param>
        /// <returns>返回字符串</returns>
        public static string Post(string url, string postDataStr)
        {
            CookieContainer cookie = new CookieContainer();

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.Method = "POST";
            request.ContentType = "application/x-www-form-urlencoded"; //必须要的

            request.CookieContainer = cookie;

            //request.ContentLength = postDataStr.Length;
            StreamWriter writer = new StreamWriter(request.GetRequestStream());
            writer.Write(postDataStr);
            writer.Flush();
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            string encoding = response.ContentEncoding;
            StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.GetEncoding("UTF-8"));
            string retStr = sr.ReadToEnd();
            sr.Close();
            return retStr;
        }

        /// <summary>
        /// 创建Post方式的HTTP请求
        /// </summary>
        /// <param name="Url">请求的url</param>
        /// <param name="cookie">请求用的cookie</param>
        /// <param name="postDataStr">发送的数据</param>
        /// <returns></returns>
        public static string SendDataByPost(string Url, CookieContainer cookie, string postDataStr)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(Url);

                request.Method = "post";

                request.Referer = "https://weibo.com";

                request.Host = "weibo.com";
                //request.Connection = "keep-alive";
                request.UserAgent = "Mozilla/5.0 (Windows NT 10.0; WOW64) AppleWebKit/537.36 (KHTML, like Gecko) Chrome/68.0.3440.75 Safari/537.36";

                request.Headers.Set("Origin", "https://weibo.com");

                request.ContentType = "application/x-www-form-urlencoded";

                request.CookieContainer = cookie;

                var b = Encoding.UTF8.GetBytes(postDataStr);
                using (var r = request.GetRequestStream())
                {
                    r.Write(b, 0, b.Length);
                }

                HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                Stream myResponseStream = response.GetResponseStream();
                StreamReader myStreamReader = new StreamReader(myResponseStream, Encoding.GetEncoding("utf-8"));
                string retString = myStreamReader.ReadToEnd();
                myStreamReader.Close();
                myResponseStream.Close();

                return retString;
            }
            catch (Exception ex)
            {
                return "";

            }

        }
    }
}
