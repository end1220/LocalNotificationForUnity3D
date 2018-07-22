

namespace LocalNotification
{
	public class NotificationManager
	{
		private INotification notification;

		public NotificationManager()
		{
#if UNITY_IOS && !UNITY_EDITOR
			notification = new IOSNotification();
#elif UNITY_ANDROID && !UNITY_EDITOR
			notification = new AndroidNotification();
#endif
		}

		public void RegisterNotification(NotificationData data)
		{
			if (notification != null)
				notification.Register(data);
		}

		public void ClearAllNotifications()
		{
			if (notification != null)
				notification.ClearAll();
		}

	}

}