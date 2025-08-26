using System.IO;

namespace System.Web.Mail;

internal interface IAttachmentEncoder
{
	void EncodeStream(Stream ins, Stream outs);
}
