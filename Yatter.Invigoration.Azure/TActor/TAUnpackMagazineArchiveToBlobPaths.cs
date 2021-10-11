using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using InvigorationTActors.TActor;
using Newtonsoft.Json;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Azure.TResponse;
using Yatter.Invigoration.Exceptions;
using Yatter.Invigoration.Extensions;
using Yatter.Invigoration.TResponse;
using Yatter.UI.ListBuilder.Serialization.Archives;

namespace Yatter.Invigoration.Azure.TActor
{
    /// <summary>
    /// Retrieves a blob that deserializes from Type Yatter.UI.ListBuilder.Serialization.Archives.Magazine and then unpacks individual documents of Type Yatter.UI.ListBuilder.Serialization.Archives.Document and saves them to specified magazine paths, invigorates a TOMagazineArchiveUnpackingSettings and responds with a TRMagazineArchiveUnpacked or TRFatalResponse
    /// </summary>
    public class TAUnpackMagazineArchiveToBlobPaths : Yatter.Invigoration.ActionBase
    {
        public TAUnpackMagazineArchiveToBlobPaths()
        {
            TActorType = GetType().ToString();
        }

        public TOMagazineArchiveUnpackingSettings TOMagazineArchiveUnpackingSettings { get { return (TOMagazineArchiveUnpackingSettings)base.Object; } }

        public void AddTObject(TOMagazineArchiveUnpackingSettings @object)
        {
            base.Object = @object;
        }

        public async override Task ActionAsync()
        {
            try
            {
                IActor acted =
                    await new TOBlobDescriptor()
                    .AddConnectionString(System.Environment.GetEnvironmentVariable("YATTER_STORAGE_CONNECTIONSTRING"))
                    .AddContainerName(TOMagazineArchiveUnpackingSettings.ArchiveContainer)
                    .AddBlobPath(TOMagazineArchiveUnpackingSettings.ArchivePath)
                    .InvigorateAsync<TAGetBlob>();

                IsSuccess = acted.IsSuccess;
                Message = acted.Message;

                AddChildToNestedResponse(acted.Result);

                if(acted.IsSuccess)
                {
                    IsSuccess = true;

                    var documentMessages = new List<string>(); // Unpacking multiple documents creates multiple unpacking messages.

                    if (acted.Result.IsSuccess) // it got the content
                    {
                        var actedResponse = acted.Result.Response as TRBlobContent;

                        IsSuccess = true;
                        Message = $"TAUnpackMagazineArchiveToBlobPaths reports that it succesfully retrieved the blob with the path '{TOMagazineArchiveUnpackingSettings.ArchivePath}' from the container '{TOMagazineArchiveUnpackingSettings.ArchiveContainer}' and has a Response type of {typeof(TAUnpackMagazineArchiveToBlobPaths)}";
                        base.Response = new TAUnpackMagazineArchiveToBlobPaths { IsSuccess = IsSuccess, Message = Message };

                        // Undertake all the search and replace substitutions at Archive level
                        foreach(var substitution in TOMagazineArchiveUnpackingSettings.Substitutions)
                        {
                            actedResponse.Content = actedResponse.Content.Replace(substitution.Moniker, substitution.Substitution);
                        }

                        var magazineArchive = JsonConvert.DeserializeObject<Magazine>(actedResponse.Content);

                        var toWriteTextToBlobSettingsList = new List<TOWriteTextToBlobSettings>();

                        foreach (var document in magazineArchive.Documents)
                        {
                            var decodedContent = Yatter.Invigoration.Azure.Helpers.StringHelpers.Base64Decode(document.Base64Content);

                            // Undertake all the search and replace substitutions at decoded Document level
                            foreach (var substitution in TOMagazineArchiveUnpackingSettings.Substitutions)
                            {
                                decodedContent = decodedContent.Replace(substitution.Moniker, substitution.Substitution);
                            }

                            var paths = new string[] { magazineArchive.PathRoot, document.Path };
                            var path = Path.Combine(paths);

                            toWriteTextToBlobSettingsList.Add(new TOWriteTextToBlobSettings()
                                        .AddBlobPath(path)
                                        .AddConnectionString(TOMagazineArchiveUnpackingSettings.UnpackingConnectionString)
                                        .AddContainerName(TOMagazineArchiveUnpackingSettings.UnpackingContainer)
                                        .AddContent(decodedContent));
                        }

                        var accumulatedMessages = new List<string>();

                        foreach (var tObject in toWriteTextToBlobSettingsList)
                        {
                            IActor acted_WriteTextToBlob = await tObject.InvigorateAsync<TAWriteTextToBlob>();

                            AddChildToNestedResponse(acted_WriteTextToBlob.Result);

                            IsSuccess = acted_WriteTextToBlob.IsSuccess;
                            Message = acted_WriteTextToBlob.Message; // this will be written over, later - either by the accumulated documentMessages, or the try-catch Exception Message

                            if (!IsSuccess)
                            {
                                base.Response = new TRMagazineArchiveUnpacked { IsSuccess = IsSuccess, Message = $"{GetType().ToString()} reports that it failed to write the file {tObject.BlobPath} to the {TOMagazineArchiveUnpackingSettings.UnpackingContainer} Container, with TAWriteTextToBlob reporting IsSuccess={IsSuccess} with the Message [{Message}] and has a Response type of {typeof(TRMagazineArchiveUnpacked)}." };

                                return;
                            }

                            documentMessages.Add($"{GetType().ToString()} reports that it wrote content to the path [{tObject.BlobPath}] in the {TOMagazineArchiveUnpackingSettings.UnpackingContainer} Container, with TAWriteTextToBlob reporting IsSuccess={IsSuccess} with the Message [{Message}].");
                        }
                    }
                    else // it did not get the content
                    {
                        IsSuccess = false;
                        Message = $"TAUnpackMagazineArchiveToBlobPaths reports that it failed to retrieve the blob with the path '{TOMagazineArchiveUnpackingSettings.ArchivePath}' from the container '{TOMagazineArchiveUnpackingSettings.ArchiveContainer}' and has a Response type of {typeof(TAUnpackMagazineArchiveToBlobPaths)}";
                        base.Response = new TAUnpackMagazineArchiveToBlobPaths { IsSuccess = IsSuccess, Message = Message };
                    }

                    var message = $"{GetType()} reports that it has unpacked multiple documents ({documentMessages.Count}), each which are reported as follows: ";

                    int count = 0;
                    foreach(var msg in documentMessages)
                    {
                        count++;

                        message += $" [{count}. {msg}]";
                    }

                    Message = message; // accumulated Messages

                    base.Response = new TRMagazineArchiveUnpacked { IsSuccess = IsSuccess, Message = $"{message} (end of accumulated messages) and has a Response type of {typeof(TRMagazineArchiveUnpacked)}." };
                }
                else
                {
                    IsSuccess = false;
                    Message = $"TAGetBlob failed with the following Message: [{acted.Message}] and has a Response type of {typeof(TRFatalResponse)}";
                    base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
                }
            }
            catch (Exception ex)
            {
                IsSuccess = false;
                Message = $"{GetType().ToString()} failed with the Exception: [{ex.Message}]";
                base.Response = new TRFatalResponse { IsSuccess = IsSuccess, Message = Message };
            }
        }

        public override void Dispose()
        {
            ((IDisposable)base.Object).Dispose();
        }
    }
}

