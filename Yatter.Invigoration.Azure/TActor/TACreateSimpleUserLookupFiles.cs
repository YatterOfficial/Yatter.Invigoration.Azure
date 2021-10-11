using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Azure.TResponse;
using Yatter.Invigoration.Exceptions;
using Yatter.Invigoration.Extensions;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TActor
{
    /// <summary>
    /// Creates Simple User Lookup Files, invigorates a TOCreateSimpleUserLookupFilesSettings, and responds with a TRFilesCreated or TRFatalResponse
    /// </summary>
    public class TACreateSimpleUserLookupFiles : Yatter.Invigoration.ActionBase
    {
        public TACreateSimpleUserLookupFiles()
        {
            TActorType = GetType().ToString();
        }

        public TOCreateSimpleUserLookupFilesSettings TOCreateSimpleUserLookupFilesSettings { get { return (TOCreateSimpleUserLookupFilesSettings)base.Object; } }

        public void AddTObject(TOCreateSimpleUserLookupFilesSettings @object)
        {
            base.Object = @object;
        }

        public async override Task ActionAsync()
        {
            try
            {
                var connectionString = System.Environment.GetEnvironmentVariable("YATTER_STORAGE_CONNECTIONSTRING");
                var containerName = "contranslation";

                var tObjects = new List<TOWriteTextToBlobSettings>();
                tObjects.Add(new TOWriteTextToBlobSettings()
                            .AddBlobPath(TOCreateSimpleUserLookupFilesSettings.UserFilePath)
                            .AddConnectionString(connectionString)
                            .AddContainerName(containerName)
                            .AddContent(JsonConvert.SerializeObject(TOCreateSimpleUserLookupFilesSettings.SimpleUserNameFileContent, Formatting.Indented)));
                tObjects.Add(new TOWriteTextToBlobSettings()
                            .AddBlobPath(TOCreateSimpleUserLookupFilesSettings.UserGuidFilePath)
                            .AddConnectionString(connectionString)
                            .AddContainerName(containerName)
                            .AddContent(JsonConvert.SerializeObject(TOCreateSimpleUserLookupFilesSettings.SimpleUserGuidFileContent, Formatting.Indented)));

                foreach(var tObject in tObjects)
                {
                    IActor acted = await tObject.InvigorateAsync<TAWriteTextToBlob>();

                    IsSuccess = acted.IsSuccess;
                    Message = acted.Message;

                    if (!IsSuccess)
                    {
                        base.Response = new TRCreateSimpleUserLookupFilesResponse { IsSuccess = IsSuccess, Message = $"{GetType().ToString()} reports that it failed to write the file {tObject.BlobPath} to the {containerName} Container, with TAWriteTextToBlob reporting IsSuccess={IsSuccess} with the Message [{Message}] and has a Response type of {typeof(TRCreateSimpleUserLookupFilesResponse)}." };

                        return;
                    }

                }

                Message = $"{GetType().ToString()} reports that it has succesfully created the Simple User Lookup Files ({TOCreateSimpleUserLookupFilesSettings.UserFilePath} and {TOCreateSimpleUserLookupFilesSettings.UserGuidFilePath}) for UserName {TOCreateSimpleUserLookupFilesSettings.UserName} with UserGuid {TOCreateSimpleUserLookupFilesSettings.UserGuid} and UserRootGuid {TOCreateSimpleUserLookupFilesSettings.UserRootGuid}, reporting IsSuccess={IsSuccess} with the Message [{Message}] and has a Response type of {typeof(TRCreateSimpleUserLookupFilesResponse)}";

                base.Response = new TRCreateSimpleUserLookupFilesResponse { IsSuccess = IsSuccess, Message = Message, UserName = TOCreateSimpleUserLookupFilesSettings.UserName, UserGuid = TOCreateSimpleUserLookupFilesSettings.UserGuid, UserRootGuid = TOCreateSimpleUserLookupFilesSettings.UserRootGuid };
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = $"{GetType().ToString()} failed with the Exception: [{ex.Message}] and has a Response type of {typeof(TRFatalResponse)}";
                base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
            }
        }

        public override void Dispose()
        {
            ((IDisposable)base.Object).Dispose();
        }
    }
}

