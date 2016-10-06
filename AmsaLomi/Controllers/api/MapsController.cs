using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using AmsaLomi.Models;
using System.Web.Http.OData;
using System.Web.Http.OData.Query;
using Newtonsoft.Json;
using System.IO;
using System.Web;

namespace AmsaLomi.Controllers.Api
{
    public class MapsController : ApiController
    {
        // GET: 
        public IHttpActionResult GetMap(int level, string parentname)
        {
            // Read map file
            Map map = null;
            if (level == 1)
                map = LoadMap(HttpContext.Current.Server.MapPath(@"~\Content\maps\ethiopia-map.json"));
            else if (level == 2)
                map = LoadMap(HttpContext.Current.Server.MapPath(@"~\Content\maps\ethiopia2-map.json"));
            else if (level == 3)
                map = LoadMap(HttpContext.Current.Server.MapPath(@"~\Content\maps\ethiopia3-map.json"));
            else
                return null;

            // Filter values
            if (!String.IsNullOrEmpty(parentname))
                map.mapItems = map.mapItems.Where(x => x.parentname == parentname).ToList();

            // return
            return Json(map);
        }

        private Map LoadMap(string filePath)
        {
            Map map = null;
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                map = JsonConvert.DeserializeObject<Map>(json);
            }
            return map;
        }

        private class Map
        {
            public string id;
            public string name;
            public string type;
            public int level;
            public List<MapItem> mapItems;
        }
        private class MapItem
        {
            public string id;
            public string parentname;
            public string name;
            public string type;
            public string path;
        }
    }
}