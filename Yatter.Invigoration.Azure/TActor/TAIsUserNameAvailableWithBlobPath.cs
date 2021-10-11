using System;
using System.Threading.Tasks;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Azure.TObject.TObject;
using Yatter.Invigoration.Azure.TResponse;
using Yatter.Invigoration.Extensions;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TActor
{
    /// <summary>
    /// Checks to see if a UserName is available, invigorates a TOUsernameContainerPathFormatter, responds with a TRUserNameAvailability or TRFatalResponse
    /// </summary>
    public class TAIsUserNameAvailableWithBlobPath : Yatter.Invigoration.ActionBase
    {
        public TAIsUserNameAvailableWithBlobPath()
        {
            TActorType = GetType().ToString();
        }

        public TOUsernameContainerPathFormatter TOUsernameContainerPathFormatter { get { return (TOUsernameContainerPathFormatter)base.Object; } }


        public void AddTObject(TOUsernameContainerPathFormatter username)
        {
            base.Object = username;
        }

        public async override Task ActionAsync()
        {
            try
            {
                IActor acted =
                    await new TOBlobDescriptor()
                    .AddConnectionString(System.Environment.GetEnvironmentVariable("YATTER_STORAGE_CONNECTIONSTRING"))
                    .AddContainerName(TOUsernameContainerPathFormatter.ContainerName)
                    .AddBlobPath(string.Format(TOUsernameContainerPathFormatter.PathFormatter, TOUsernameContainerPathFormatter.UserName))
                    .InvigorateAsync<TACheckBlobExists>();

                AddChildToNestedResponse(acted.Result);

                if (acted.IsSuccess)
                {
                    base.IsSuccess = true;

                    var existsResponse = (TRBlobExists)acted.Response;

                    if (existsResponse.Exists)
                    {
                        Message = $"TAIsUserNameAvailableWithBlobPath reports that UserName {TOUsernameContainerPathFormatter.UserName} is not available and has a Response type of {typeof(TRUserNameAvailability)}";
                        base.Response = new TRUserNameAvailability { IsSuccess = IsSuccess, IsAvailable = false, UserName = TOUsernameContainerPathFormatter.UserName, Message = Message };
                    }
                    else
                    {
                        Message = $"TAIsUserNameAvailableWithBlobPath reports that UserName {TOUsernameContainerPathFormatter.UserName} is available and has a Response type of {typeof(TRUserNameAvailability)}";
                        base.Response = new TRUserNameAvailability { IsSuccess = IsSuccess, IsAvailable = IsSuccess, UserName = TOUsernameContainerPathFormatter.UserName, Message = Message };
                    }
                }
                else
                {
                    IsSuccess = false;
                    Message = $"TAIsUserNameAvailableWithBlobPath failed with the following Message: [{acted.Message}] and has a Response type of {typeof(TRFatalResponse)}";
                    base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
                }
            }
            catch(Exception ex)
            {
                IsSuccess = false;
                Message = $"TAIsUserNameAvailableWithBlobPath failed with the Exception: [{ex.Message}] and has a Response type of {typeof(TRFatalResponse)}";
                base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
            }
        }

        public override void Dispose()
        {
            ((IDisposable)base.Object).Dispose();
        }
    }


}

