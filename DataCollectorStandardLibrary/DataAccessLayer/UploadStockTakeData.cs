using DataCollectorStandardLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.DataAccessLayer
{
    public class UploadStockTakeData
    {
        public static async Task<FunctionResponse<string>> PostStockTakeList(List<LoadDataCollect> StockTakeList)
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.SaveDataCollectURL);
                //String url = Helpers.Data.Constants.SaveGrnSyncDataURL;
                var JsonObject = JsonConvert.SerializeObject(StockTakeList);
                string ContentType = "application/json"; // or application/xml

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();
                    var resString = JsonConvert.DeserializeObject<string>(json);
                    var functionresponse = new FunctionResponse<string>() { status = "ok", result = resString};
                    return functionresponse;
                }
            }
            catch (Exception ex)
            {
                return new FunctionResponse<string>() { status = "error", Message = ex.Message };
            }
        }
    }
}
