using DataCollectorStandardLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.DataAccessLayer
{
    public class UploadBranchOutData
    {
        public static async Task<FunctionResponse<List<BranchOutMaster>>> PostBranchOutMasterList(List<BranchOutMaster> BranchOutMasterList)
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.SaveBranchOutSyncDataURL);
                //String url = Helpers.Data.Constants.SaveBranchOutSyncDataURL;
                var JsonObject = JsonConvert.SerializeObject(BranchOutMasterList);
                string ContentType = "application/json"; // or application/xml

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();
                    var functionresponse = JsonConvert.DeserializeObject<FunctionResponse<List<BranchOutMaster>>>(json);
                    return functionresponse;
                }
            }
            catch (Exception ex)
            {
                return new FunctionResponse<List<BranchOutMaster>>() { status = "error", Message = ex.Message };
            }
        }
    }
}
