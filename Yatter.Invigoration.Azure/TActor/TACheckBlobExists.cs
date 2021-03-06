using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Azure.TResponse;
using Yatter.Invigoration.TResponse;
using Yatter.Storage.Azure;

namespace Yatter.Invigoration.Azure.TActor
{
    /// <summary>
    /// Checks to see if a blob exists, invigorates a TOBlobDescriptor and responds with a TRBlobExists or TRFatalResponse
    /// </summary>
    public class TACheckBlobExists : Yatter.Invigoration.ActionBase
    {
        public TOBlobDescriptor TOBlobDescriptor { get { return (TOBlobDescriptor)base.Object; } }

        public TACheckBlobExists()
        {
            TActorType = GetType().ToString();
        }

        public void AddTObject(TOBlobDescriptor toBlobDescriptor)
        {
            base.Object = toBlobDescriptor;
        }

        public async override Task ActionAsync()
        {
            try
            {
                var responsiveBlobManager = new Yatter.Storage.Azure.ResponsiveBlobManager();

                var blobRequest = new BlobRequest();
                blobRequest.SetConnectionString(TOBlobDescriptor.ConnectionString);
                blobRequest.SetContainerName(TOBlobDescriptor.ContainerName);
                blobRequest.SetBlobPath(TOBlobDescriptor.BlobPath);

                var response = await responsiveBlobManager.ExistsBlobAsync<BlobResponse, BlobRequest>(blobRequest);
                
                if (response.IsSuccess)
                {
                    IsSuccess = true;

                    var existsResponse = JsonConvert.DeserializeObject<ExistsResponse>(response.Message);

                    if (existsResponse.Exists)
                    {
                        Message = $"TACheckBlobExists reports that the Blob exists in the Container '{TOBlobDescriptor.ContainerName}' with the path '{TOBlobDescriptor.BlobPath}' and has a Response type of {typeof(TRBlobExists)}";
                        base.Response = new TRBlobExists { IsSuccess = IsSuccess, Exists = true, Container = TOBlobDescriptor.ContainerName, Path = TOBlobDescriptor.BlobPath, Message = Message  };
                    }
                    else
                    {
                        Message = $"TACheckBlobExists reports that the Blob does not exist in the Container '{TOBlobDescriptor.ContainerName}' with the path '{TOBlobDescriptor.BlobPath}' and has a Response type of {typeof(TRBlobExists)}";
                        base.Response = new TRBlobExists { IsSuccess = IsSuccess, Exists = false, Container = TOBlobDescriptor.ContainerName, Path = TOBlobDescriptor.BlobPath, Message = Message };
                    }
                }
                else
                {
                    IsSuccess = false;
                    Message = $"TACheckBlobExists failed with the following Message: [{response.Message}] and has a Response type of {typeof(TRFatalResponse)}";
                    base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
                }
            }
            catch(Exception ex)
            {
                IsSuccess = false;
                Message = $"TACheckBlobExists failed with the following Exception: [{ex.Message}] and has a Response type of {typeof(TRFatalResponse)}";
                base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
            }
        }
    }
}

