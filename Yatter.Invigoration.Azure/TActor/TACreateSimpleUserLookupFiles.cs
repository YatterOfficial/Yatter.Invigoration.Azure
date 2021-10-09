using System;
using System.Threading.Tasks;
using Newtonsoft.Json;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Azure.TResponse;
using Yatter.Invigoration.Exceptions;
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

                #region Write SimpleUserNameFile
                var writeUserNameFile =
                    await WriteBlob(connectionString,
                                    containerName,
                                    TOCreateSimpleUserLookupFilesSettings.UserFilePath,
                                    JsonConvert.SerializeObject(TOCreateSimpleUserLookupFilesSettings.SimpleUserNameFileContent, Formatting.Indented));

                AddChildToNestedResponse(writeUserNameFile.Result);

                IsSuccess = writeUserNameFile.IsSuccess;
                Message = writeUserNameFile.Message;

                if (!IsSuccess)
                {
                    base.Response = new TACreateSimpleUserLookupFilesResponse { IsSuccess = IsSuccess, Message = $"{GetType().ToString()} reports that it failed to write the UserNameFile {TOCreateSimpleUserLookupFilesSettings.UserFilePath} to the {containerName} Container, with TAWriteTextToBlob reporting [IsSuccess={writeUserNameFile.IsSuccess} with the Message {writeUserNameFile.Message}.]" };

                    return;
                }
                #endregion

                #region Write SimpleUserGuidFile
                var writeUserGuidFile =
                    await WriteBlob(connectionString,
                                    containerName,
                                    TOCreateSimpleUserLookupFilesSettings.UserGuidFilePath,
                                    JsonConvert.SerializeObject(TOCreateSimpleUserLookupFilesSettings.SimpleUserGuidFileContent, Formatting.Indented));

                AddChildToNestedResponse(writeUserGuidFile.Result);

                IsSuccess = writeUserGuidFile.IsSuccess;
                Message = writeUserGuidFile.Message;

                if (!IsSuccess)
                {
                    base.Response = new TACreateSimpleUserLookupFilesResponse { IsSuccess = IsSuccess, Message = $"{GetType().ToString()} reports that it failed to write the UserNameFile {TOCreateSimpleUserLookupFilesSettings.UserFilePath} to the {containerName} Container, with TAWriteTextToBlob reporting [IsSuccess={writeUserNameFile.IsSuccess} with the Message {writeUserNameFile.Message}.]" };

                    return;
                }

                #endregion

                Message = $"{GetType().ToString()} reports that it has succesfully created the Simple User Lookup Files ({TOCreateSimpleUserLookupFilesSettings.UserFilePath} and {TOCreateSimpleUserLookupFilesSettings.UserGuidFilePath}) for UserName {TOCreateSimpleUserLookupFilesSettings.UserName}";

                base.Response = new TACreateSimpleUserLookupFilesResponse { IsSuccess = IsSuccess, Message = Message };
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = $"{GetType().ToString()} failed with the Exception: [{ex.Message}]";
                base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
            }
        }

        private static async Task<TAWriteTextToBlob> WriteBlob(string connectionString, string containerName, string blobPath, string content)
        {
            return await Invigorator.ActAsync<TOWriteTextToBlobSettings, TAWriteTextToBlob>(
                                        new TAWriteTextToBlob()
                                        .AddTObjectToTActor(
                                        new TOWriteTextToBlobSettings
                                        {
                                            ConnectionString = connectionString,
                                            ContainerName = containerName,
                                            BlobPath = blobPath,
                                            Content = content
                                        }));
        }

        public override void Dispose()
        {
            ((IDisposable)base.Object).Dispose();
        }
    }
}

