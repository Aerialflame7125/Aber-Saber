using System;
using Oculus.Platform.Models;

namespace Oculus.Platform
{
	public class MessageWithAbuseReportRecording : Message<AbuseReportRecording>
	{
		public MessageWithAbuseReportRecording(IntPtr c_message)
			: base(c_message)
		{
		}

		public override AbuseReportRecording GetAbuseReportRecording()
		{
			return base.Data;
		}

		protected override AbuseReportRecording GetDataFromMessage(IntPtr c_message)
		{
			IntPtr obj = CAPI.ovr_Message_GetNativeMessage(c_message);
			IntPtr o = CAPI.ovr_Message_GetAbuseReportRecording(obj);
			return new AbuseReportRecording(o);
		}
	}
}
