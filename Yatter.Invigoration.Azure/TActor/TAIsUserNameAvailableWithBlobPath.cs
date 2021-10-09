using System;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yatter.Invigoration;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Azure.TObject.TObject;
using Yatter.Invigoration.Azure.TResponse;
using Yatter.Storage.Azure;

namespace Yatter.Invigoration.Azure.TActor
{
    public class TAIsUserNameAvailableWithBlobPath : Yatter.Invigoration.ActionBase
    {
        public TAIsUserNameAvailableWithBlobPath()
        {
            TActorType = GetType().ToString();
        }

        public TOUsernameContainerPathFormatter TOUsernameContainerPathFormatter { get { return (TOUsernameContainerPathFormatter)base.Object; } }

        public string StorageConnectionString { get; set; }

        public void AddTObject(TOUsernameContainerPathFormatter username)
        {
            base.Object = username;
        }

        public async override Task ActionAsync()
        {
            try
            {
                /*  BUSINESS RULES
                 *  
                 *  User exists if Blob exists in [CONTAINER_NAME] container with formatted path '...{0}...' e.g. string.Format("users/username/{0}.json", [USERNAME])
                 *  
                 *  INVIGORATION PATTERN
                 *  
                 *  TACheckBlobExists acted = await Invigorator.ActAsync<TOBlobDescriptor, TACheckBlobExists>(inputs);
                 *  
                 *  TOBlobDescriptor has the following properties:
                 *  
                 *  1. ConnectionString
                 *  2. ContainerName
                 *  3. BlobPath
                 *  
                 * */

                var inputs = new TACheckBlobExists();
                inputs.AddTObject(new TOBlobDescriptor
                {
                    ConnectionString = System.Environment.GetEnvironmentVariable("YATTER_STORAGE_CONNECTIONSTRING"),
                    ContainerName = TOUsernameContainerPathFormatter.Container,
                    BlobPath = string.Format(TOUsernameContainerPathFormatter.PathFormatter, TOUsernameContainerPathFormatter.UserName)
                });

                TACheckBlobExists acted = await Invigorator.ActAsync<TOBlobDescriptor, TACheckBlobExists>(inputs);
                AddNestedResult(acted.Result);

                if (acted.IsSuccess)
                {
                    base.IsSuccess = true;

                    var existsResponse = (TRBlobExists)acted.Response;

                    if (existsResponse.Exists)
                    {
                        Message = $"UserName {TOUsernameContainerPathFormatter.UserName} is not available";
                        base.Response = new TRUserNameAvailability { IsAvailable = false, UserName = TOUsernameContainerPathFormatter.UserName, Message = Message };
                    }
                    else
                    {
                        Message = $"UserName {TOUsernameContainerPathFormatter.UserName} is available";
                        base.Response = new TRUserNameAvailability { IsAvailable = true, UserName = TOUsernameContainerPathFormatter.UserName, Message = Message };
                    }
                }
                else
                {
                    IsSuccess = false;
                    Message = $"TACheckUserNameAvailability failed with the following Message: [{acted.Message}]";
                    base.Response = new TRFatalResponse { Message = Message };
                }
            }
            catch(Exception ex)
            {
                IsSuccess = false;
                Message = $"TACheckUserNameAvailability failed with the Exception: [{ex.Message}]";
                base.Response = new TRFatalResponse { Message = Message };
            }

            base.AddToNestedResponse(this);
        }

        public override void Dispose()
        {
            ((IDisposable)base.Object).Dispose();
        }
    }


}

