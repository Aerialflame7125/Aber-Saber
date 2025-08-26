using System;

namespace Oculus.Platform.Models
{
	public class RoomInviteNotification
	{
		public readonly ulong ID;

		public readonly ulong RoomID;

		public readonly ulong SenderID;

		public readonly DateTime SentTime;

		public RoomInviteNotification(IntPtr o)
		{
			ID = CAPI.ovr_RoomInviteNotification_GetID(o);
			RoomID = CAPI.ovr_RoomInviteNotification_GetRoomID(o);
			SenderID = CAPI.ovr_RoomInviteNotification_GetSenderID(o);
			SentTime = CAPI.ovr_RoomInviteNotification_GetSentTime(o);
		}
	}
}
