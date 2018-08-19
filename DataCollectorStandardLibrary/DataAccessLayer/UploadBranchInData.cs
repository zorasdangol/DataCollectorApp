using DataCollectorStandardLibrary.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace DataCollectorStandardLibrary.DataAccessLayer
{
    public class UploadBranchInData
    {
        public static async Task<FunctionResponse<List<BranchInMaster>>> PostBranchInMasterList(List<BranchInMaster> BranchInMasterList)
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.SaveBranchInSyncDataURL);
                //String url = Helpers.Data.Constants.SaveBranchOutSyncDataURL;
                var JsonObject = JsonConvert.SerializeObject(BranchInMasterList);
                string ContentType = "application/json"; // or application/xml

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();
                    var functionresponse = JsonConvert.DeserializeObject<FunctionResponse<List<BranchInMaster>>>(json);
                    return functionresponse;
                }
            }
            catch (Exception ex)
            {
                return new FunctionResponse<List<BranchInMaster>>() { status = "error", Message = ex.Message };
            }
        }

        public static async Task<FunctionResponse<List<BranchInItem>>> GetSendItemList(BranchInDetail BranchInDetail)
        {
            try
            {
                String url = Helpers.Data.Constants.SetApiURL(Helpers.Data.Constants.getTransferItem);
                //String url = Helpers.Data.Constants.getTransferItem;
                var JsonObject = JsonConvert.SerializeObject(BranchInDetail);
                string ContentType = "application/json"; // or application/xml

                using (HttpClient client = new HttpClient())
                {
                    var response = await client.PostAsync(url, new StringContent(JsonObject.ToString(), Encoding.UTF8, ContentType));

                    var json = await response.Content.ReadAsStringAsync();
                    var functionresponse = JsonConvert.DeserializeObject<FunctionResponse<List<BranchInItem>>>(json);
                    return functionresponse;
                }
            }
            catch (Exception ex)
            {
                return new FunctionResponse<List<BranchInItem>>() { status = "error", Message = ex.Message };
            }
        }


    }
}
