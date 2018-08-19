using DataCollectorStandardLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.DataAccessLayer
{
    public class UploadGrnData
    {
        public static async Task<FunctionResponse<List<GrnMaster>>> PostGrnMasterList(List<GrnMaster> GrnMasterList)
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.SaveGrnSyncDataURL);
                //String url = Helpers.Data.Constants.SaveGrnSyncDataURL;
                var JsonObject = JsonConvert.SerializeObject(GrnMasterList);
                string ContentType = "application/json"; // or application/xml
                
                using (HttpClient client = new HttpClient())
                {          
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();
                    var functionresponse = JsonConvert.DeserializeObject<FunctionResponse<List<GrnMaster>>>(json);
                    return functionresponse;                   
                }
            }
            catch (Exception ex)
            {
                return new FunctionResponse<List<GrnMaster>>() { status = "error", Message = ex.Message };
            }
        }
    }
}
