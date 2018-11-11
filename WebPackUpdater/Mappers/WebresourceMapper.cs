﻿using System.Collections.Generic;
using WebPackUpdater.Enums;

namespace WebPackUpdater.Mappers
{
    internal class WebresourceMapper
    {
        private static WebresourceMapper instance;

        private WebresourceMapper()
        {
            Items.Add(new Map("htm", WebResourceType.WebPage, 1, "Webpage"));
            Items.Add(new Map("html", WebResourceType.WebPage, 1, "Webpage"));
            Items.Add(new Map("css", WebResourceType.Css, 2, "Style"));
            Items.Add(new Map("js", WebResourceType.Script, 3, "Script"));
            Items.Add(new Map("ts", WebResourceType.Script, 3, "Script"));
            Items.Add(new Map("map", WebResourceType.Script, 3, "Script"));
            Items.Add(new Map("xml", WebResourceType.Data, 4, "Data"));
            Items.Add(new Map("png", WebResourceType.Png, 5, "PNG"));
            Items.Add(new Map("jpg", WebResourceType.Jpg, 6, "JPG"));
            Items.Add(new Map("jpeg", WebResourceType.Jpg, 6, "JPG"));
            Items.Add(new Map("gif", WebResourceType.Gif, 7, "GIF"));
            Items.Add(new Map("xap", WebResourceType.Silverlight, 8, "Silverlight"));
            Items.Add(new Map("xsl", WebResourceType.Xsl, 9, "XSL"));
            Items.Add(new Map("xslt", WebResourceType.Xsl, 9, "XSL"));
            Items.Add(new Map("ico", WebResourceType.Ico, 10, "ICO"));
            Items.Add(new Map("svg", WebResourceType.Vector, 11, "Vector"));
            Items.Add(new Map("resx", WebResourceType.Resx, 12, "Resource"));
        }

        public static WebresourceMapper Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new WebresourceMapper();
                }

                return instance;
            }
        }

        public List<Map> Items { get; } = new List<Map>();
    }

    internal class Map
    {
        public Map(string extension, WebResourceType type, int crmValue, string label)
        {
            Extension = extension;
            Type = type;
            CrmValue = crmValue;
            Label = label;
        }

        public string Extension { get; }
        public WebResourceType Type { get; }
        public int CrmValue { get; }
        public string Label { get; }
    }
}
