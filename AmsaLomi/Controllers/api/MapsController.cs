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
using GeoJSON.Net.Feature;

namespace AmsaLomi.Controllers.Api
{
    public class MapsController : ApiController
    {
        // GET: 
        public IHttpActionResult GetMap(string name1, string name2, string name3)
        {
            // Read map file
            FeatureCollection map = LoadMap(name1, name2, name3);

            // return
            return Json(map);
        }

        private FeatureCollection LoadMap(string name1, string name2, string name3)
        {
            string filePath = null, filter = null, parameter = null;
            if (!String.IsNullOrEmpty(name3) && !name3.Equals("undefined"))
            {
                filePath = HttpContext.Current.Server.MapPath(@"~\Content\maps\Adm3.geojson");
                filter = name3;
                parameter = "NAME_2";
            }
            else if (!String.IsNullOrEmpty(name2) && !name2.Equals("undefined"))
            {
                filePath = HttpContext.Current.Server.MapPath(@"~\Content\maps\Adm2.geojson");
                filter = name2;
                parameter = "NAME_1";
            }
            else if (!String.IsNullOrEmpty(name1) && !name1.Equals("undefined"))
            {
                filePath = HttpContext.Current.Server.MapPath(@"~\Content\maps\Adm1.geojson");
                filter = name1;
                parameter = "NAME_0";
            }
            else
                return null;

            FeatureCollection map = new FeatureCollection();
            using (StreamReader r = new StreamReader(filePath))
            {
                string json = r.ReadToEnd();
                var feature = JsonConvert.DeserializeObject<FeatureCollection>(json, new GeoJSON.Net.Converters.MultiPolygonConverter());
                // Filter out selected values
                if (!String.IsNullOrEmpty(filter))
                    map.Features.AddRange(feature.Features.Where(x => x.Properties[parameter].ToString().Equals(filter, StringComparison.CurrentCultureIgnoreCase)));
            }
            
            return map;
        }
    }
}