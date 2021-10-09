using System;
using System.Threading.Tasks;
using Yatter.Invigoration.Azure.TObject;
using Yatter.Invigoration.Exceptions;
using Yatter.Invigoration.TResponse;

namespace Yatter.Invigoration.Azure.TActor
{
    /// <summary>
    /// Unpacks a 
    /// </summary>
    public class TAUnpackFileArchiveToBlobPaths : Yatter.Invigoration.ActionBase
    {
        public TAUnpackFileArchiveToBlobPaths()
        {
            TActorType = GetType().ToString();
        }

        public TOFileArchiveUnpackingSettings TOFileArchiveUnpackingSettings { get { return (TOFileArchiveUnpackingSettings)base.Object; } }

        public void AddTObject(TOFileArchiveUnpackingSettings @object)
        {
            base.Object = @object;
        }

        public async override Task ActionAsync()
        {
            try
            {
                IsSuccess = true;

                Message = $"TAUnpackFileArchiveToBlobPaths reports that it is not yet implemented.";

                base.Response = new TRNotImplemented { IsSuccess = IsSuccess, Message = Message };

                //throw new TActorNotImplementedException(TActorType);
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

