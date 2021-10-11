using System;
using System.Threading.Tasks;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Azure.TResponse;
using Yatter.Invigoration.Exceptions;
using Yatter.Invigoration.TResponse;
using Yatter.Storage.Azure;

namespace Yatter.Invigoration.Azure.TActor
{
    public class TAWriteTextToBlob : Yatter.Invigoration.ActionBase
    {
        public TAWriteTextToBlob()
        {
            TActorType = GetType().ToString();
        }

        public TOWriteTextToBlobSettings TOWriteTextToBlobSettings { get { return (TOWriteTextToBlobSettings)base.Object; } }

        public void AddTObject(TOWriteTextToBlobSettings @object)
        {
            base.Object = @object;
        }

        public async override Task ActionAsync()
        {
            try
            {
                var connectionString = TOWriteTextToBlobSettings.ConnectionString;
                var containerName = TOWriteTextToBlobSettings.ContainerName;
                var blobPath = TOWriteTextToBlobSettings.BlobPath;
                var blobContent = TOWriteTextToBlobSettings.Content;

                BlobResponse blobResponse = await WriteBlob(connectionString, containerName, blobPath, blobContent);

                IsSuccess = blobResponse.IsSuccess;

                if (IsSuccess)
                {
                    Message = $"{GetType().ToString()} reports that Yatter.Storage.Azure.ResponsiveBlobManager succesfully uploaded to '{TOWriteTextToBlobSettings.BlobPath}' with the response message message [" + blobResponse.Message + $"] and has a response type of {typeof(TRWriteBlobResponse)}";
                }
                else
                {
                    Message = $"{GetType().ToString()} reports that Yatter.Storage.Azure.ResponsiveBlobManager failed to uploaded to '{TOWriteTextToBlobSettings.BlobPath}' with the response message message [" + blobResponse.Message + $"] and has a response type of {typeof(TRWriteBlobResponse)}";
                }
                base.Response = new TRWriteBlobResponse { IsSuccess = IsSuccess, Message = Message };
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = $"{GetType().ToString()} failed with the Exception: [{ex.Message}]" + $" and has a response type of {typeof(TRFatalResponse)}";
                base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
            }
        }

        private static async Task<BlobResponse> WriteBlob(string connectionString, string containerName, string blobPath, string content)
        {
            var blobRequest = new BlobRequest();
            blobRequest.SetConnectionString(connectionString);
            blobRequest.SetContainerName(containerName);
            blobRequest.SetBlobPath(blobPath);
            blobRequest.BlobContent = content;

            ResponsiveBlobManager responsiveBlobManager = new ResponsiveBlobManager();

            return await responsiveBlobManager.UploadBlobAsync<BlobResponse, BlobRequest>(blobRequest);
        }

        public override void Dispose()
        {
            ((IDisposable)base.Object).Dispose();
        }
    }
}

