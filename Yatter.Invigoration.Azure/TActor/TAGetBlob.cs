using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yatter.Invigoration;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Azure.TResponse;
using Yatter.Invigoration.Exceptions;
using Yatter.Invigoration.TObject;
using Yatter.Invigoration.TResponse;
using Yatter.Storage.Azure;

namespace InvigorationTActors.TActor
{
    /// <summary>
    /// Gets a blob, invigorates a TOBlobDescriptor and responds with a TRBlobContent or TRFatalResponse
    /// </summary>
    public class TAGetBlob : Yatter.Invigoration.ActionBase
    {
        public TOBlobDescriptor TOBlobDescriptor { get { return (TOBlobDescriptor)base.Object; } }

        public TAGetBlob()
        {
            TActorType = GetType().ToString();
        }

        public void AddTObject(TODefault tObject)
        {
            base.Object = tObject;
        }

        public async override Task ActionAsync()
        {
           try
            {

		        var input = TOBlobDescriptor;

                var responsiveBlobManager = new Yatter.Storage.Azure.ResponsiveBlobManager();

                var blobRequest = new BlobRequest();
                blobRequest.SetConnectionString(TOBlobDescriptor.ConnectionString);
                blobRequest.SetContainerName(TOBlobDescriptor.ContainerName);
                blobRequest.SetBlobPath(TOBlobDescriptor.BlobPath);

                var response = await responsiveBlobManager.GetBlobAsync<BlobResponse, BlobRequest>(blobRequest);

                IsSuccess = true;

		        if(IsSuccess)
		        {
			        Message = $"{GetType().ToString()} reports that it succesfully acquired the content of {blobRequest.BlobPath} in container {blobRequest.ContainerName} and has a Response type of {typeof(TRBlobContent)}.";
		        }
		        else
		        {
			        Message = $"{GetType().ToString()} reports that it failed with the Message [{response.Message}] and has a Response type of {typeof(TRBlobContent)}";
		        }

                base.Response = new TRBlobContent { IsSuccess = IsSuccess, Message = Message, Content = response.Content };
	        }
            catch(Exception ex)
            {
                IsSuccess = false;
                Message = $"{GetType().ToString()}'s ActAsync failed with the following Exception: [{ex.Message}] and has a Response type of {typeof(TRFatalResponse)}";
                base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
            }
        }
    }
}

