using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web;

namespace GowWebSite.Controllers
{
    public class UploadController : ApiController
    {
        // GET: api/Upload
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Upload/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Upload
        public string Post()
        {
            HttpResponseMessage result = null;
            var httpRequest = HttpContext.Current.Request;

            if (!httpRequest.Headers.AllKeys.Contains("From") || httpRequest.Headers["From"] != "GowScript")
            {
                return "Failed";
            }

            int cityID = -1;
            if (httpRequest.Headers.AllKeys.Contains("CityID"))
            {
                try
                {
                    cityID = Int16.Parse(httpRequest.Headers["CityID"]);
                }
                catch
                {
                    cityID = -1;
                }
            }
            try
            {
                if (httpRequest.Files.Count > 0)
                {
                    var docfiles = new List<string>();
                    foreach (string file in httpRequest.Files)
                    {
                        var postedFile = httpRequest.Files[file];
                        string filePath;
                        if (cityID > 0)
                        {
                            string dir =  HttpContext.Current.Server.MapPath("/Images/" + cityID + "/");
                            if (!System.IO.Directory.Exists(dir))
                            {
                                System.IO.Directory.CreateDirectory(dir);
                            }
                            filePath = dir + postedFile.FileName;
                        }
                        else
                        {
                            filePath = HttpContext.Current.Server.MapPath("/Images/" + postedFile.FileName);
                        }
                        postedFile.SaveAs(filePath);

                        docfiles.Add(filePath);
                    }
                    result = Request.CreateResponse(HttpStatusCode.Created, docfiles);
                }
                else
                {
                    result = Request.CreateResponse(HttpStatusCode.BadRequest);
                }
                return "Success";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        // PUT: api/Upload/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Upload/5
        public void Delete(int id)
        {
        }
    }
}
