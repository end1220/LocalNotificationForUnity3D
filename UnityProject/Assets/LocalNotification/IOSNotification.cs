
#if UNITY_IOS && !UNITY_EDITOR


namespace LocalNotification
{
	public class IOSNotification : INotification
	{
		private const float DELAY_PUSH_SECONDS = 0.5f;

		public IOSNotification()
		{
			// 注册本地推送
			UnityEngine.iOS.NotificationServices.RegisterForNotifications(
			UnityEngine.iOS.NotificationType.Alert |
			UnityEngine.iOS.NotificationType.Badge |
			UnityEngine.iOS.NotificationType.Sound, false);
		}

		public void Register(NotificationData data)
		{
			UnityEngine.iOS.LocalNotification lnf = ConvertToSystemNotification(data);
			//UnityEngine.iOS.NotificationServices.PresentLocalNotificationNow(lnf);
			UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(lnf);
		}

		public void ClearAll()
		{
			// 取消所有的本地通知
			UnityEngine.iOS.NotificationServices.CancelAllLocalNotifications();

			// 清除本地通知
			UnityEngine.iOS.NotificationServices.ClearLocalNotifications();

			// 清除角标
			UnityEngine.iOS.LocalNotification notification = new UnityEngine.iOS.LocalNotification();
			notification.applicationIconBadgeNumber = -1;
			notification.hasAction = false;
			notification.fireDate = System.DateTime.Now.AddSeconds(DELAY_PUSH_SECONDS);
			notification.alertBody = "";
			UnityEngine.iOS.NotificationServices.ScheduleLocalNotification(notification);
		}

		private static string GetDeviceToken()
		{
			byte[] token = UnityEngine.iOS.NotificationServices.deviceToken;
			if (token != null)
			{
				return System.BitConverter.ToString(token).Replace('-', '%');
			}
			return "";
		}

		private static UnityEngine.iOS.LocalNotification ConvertToSystemNotification(NotificationData notification)
		{
			string title = notification.Title;
			string body = notification.Body;

			UnityEngine.iOS.LocalNotification lnf = new UnityEngine.iOS.LocalNotification();
			lnf.alertAction = (string.IsNullOrEmpty(title) ? "" : title);
			lnf.alertBody = (string.IsNullOrEmpty(body) ? "" : body);
			lnf.hasAction = true;
			lnf.fireDate = notification.FireDate.AddSeconds(DELAY_PUSH_SECONDS);
			lnf.applicationIconBadgeNumber = 1;
			lnf.soundName = UnityEngine.iOS.LocalNotification.defaultSoundName;
			if (notification.Repeat)
			{
				lnf.repeatCalendar = (UnityEngine.iOS.CalendarIdentifier)notification.RepeatCalendar;
				lnf.repeatInterval = (UnityEngine.iOS.CalendarUnit)notification.RepeatInterval;
			}

			return lnf;
		}

	}

}

#endif