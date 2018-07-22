using System;
using System.Collections.Generic;


namespace LocalNotification
{
	public class NotificationData
	{
		public string Title { get; private set; }

		public string Body { get; private set; }

		public DateTime FireDate { get; private set; }

		public uint FireDelaySeconds { get; private set; }

		public bool Repeat { get; private set; }

		public GameCalendarUnit RepeatInterval { get; private set; }

		public GameCalendarIdentifier RepeatCalendar { get; private set; }


		public NotificationData(string title, string body, uint delaySeconds, bool repeat)
		{
			Title = title;
			Body = body;
			FireDelaySeconds = delaySeconds;
			FireDate = DateTime.Now.AddSeconds(delaySeconds);
			Repeat = repeat;
			RepeatInterval = GameCalendarUnit.Day;
			RepeatCalendar = GameCalendarIdentifier.ChineseCalendar;
		}

		public NotificationData(string title, string body, DateTime fireDate, bool repeat)
		{
			Title = title;
			Body = body;
			TimeSpan span = fireDate.Subtract(DateTime.Now);
			FireDelaySeconds = (uint)span.TotalSeconds;// (uint)(fireDate - DateTime.Now).TotalSeconds;
			FireDate = fireDate;
			Repeat = repeat;
			RepeatInterval = GameCalendarUnit.Day;
			RepeatCalendar = GameCalendarIdentifier.ChineseCalendar;
		}

		public const string Notification_Spacer_Block_Item = "{Notification}";//多条数据中区分数据项的间隔符
		public const string Notification_Spacer_Block_Data = "{,}";//每条数据中区分不同变量的间隔符
		public const string Notification_Wildcard_Repeat = "{Noti_repeat}";//推送 字符串构成 重复
		public const string Notification_Wildcard_Once = "{Noti_once}";//推送 字符串构成 不重复
		public const uint Notification_String_Count = 6;//Title+Body+Date+IsRepeat+CalenderIdentifier+CalenderUnit

		public override string ToString()
		{
			string formatString = "";
			formatString += Notification_Spacer_Block_Item;
			formatString += Title;
			formatString += Notification_Spacer_Block_Data;
			formatString += Body;
			formatString += Notification_Spacer_Block_Data;
			formatString += FireDate.ToString();
			formatString += Notification_Spacer_Block_Data;
			if (Repeat)
			{
				formatString += Notification_Wildcard_Repeat;
			}
			else
			{
				formatString += Notification_Wildcard_Once;
			}
			formatString += Notification_Spacer_Block_Data;
			formatString += GameCalendarIdentifier.ChineseCalendar.ToString();
			formatString += Notification_Spacer_Block_Data;
			formatString += GameCalendarUnit.Day.ToString();

			return formatString;
		}

		public static List<NotificationData> ListFromString(string playerPrefsString)
		{
			List<NotificationData> itemList = new List<NotificationData>();
			if (!string.IsNullOrEmpty(playerPrefsString))
			{
				string[] stringList = playerPrefsString.Split(new string[] { Notification_Spacer_Block_Item }, StringSplitOptions.RemoveEmptyEntries);
				foreach (string playerSingleString in stringList)
				{
					NotificationData newItem = FromString(playerSingleString);
					if (newItem != null)
						itemList.Add(newItem);
				}
			}
			return itemList;
		}

		public static NotificationData FromString(string stringItem)
		{
			if (!string.IsNullOrEmpty(stringItem))
			{
				string[] stringList = stringItem.Split(new string[] { Notification_Spacer_Block_Data }, StringSplitOptions.RemoveEmptyEntries);
				if (stringList != null && stringList.Length == Notification_String_Count)
				{
					string title = stringList[0];
					string body = stringList[1];
					DateTime fireDate = Convert.ToDateTime(stringList[2]);
					bool repeat = false;
					if (stringList[3] == Notification_Wildcard_Repeat)
					{
						repeat = true;
						//realNotification.RepeatCalendar = (GameCalendarIdentifier)Enum.Parse(typeof(GameCalendarIdentifier), stringList[4]);
						//realNotification.RepeatInterval = (GameCalendarUnit)Enum.Parse(typeof(GameCalendarUnit), stringList[5]);
					}
					else
					{
						repeat = false;
					}
					NotificationData realNotification = new NotificationData(title, body, fireDate, repeat);
					return realNotification;
				}
			}
			return null;
		}

	}


	public enum GameCalendarUnit
	{
		Era = 2,
		Year = 4,
		Month = 8,
		Day = 16,
		Hour = 32,
		Minute = 64,
		Second = 128,
		Week = 256,
		Weekday = 512,
		WeekdayOrdinal = 1024,
		Quarter = 2048,
	}

	public enum GameCalendarIdentifier
	{
		// 摘要: 
		//     Identifies the Gregorian calendar.
		GregorianCalendar = 0,
		//
		// 摘要: 
		//     Identifies the Buddhist calendar.
		BuddhistCalendar = 1,
		//
		// 摘要: 
		//     Identifies the Chinese calendar.
		ChineseCalendar = 2,
		//
		// 摘要: 
		//     Identifies the Hebrew calendar.
		HebrewCalendar = 3,
		//
		// 摘要: 
		//     Identifies the Islamic calendar.
		IslamicCalendar = 4,
		//
		// 摘要: 
		//     Identifies the Islamic civil calendar.
		IslamicCivilCalendar = 5,
		//
		// 摘要: 
		//     Identifies the Japanese calendar.
		JapaneseCalendar = 6,
		//
		// 摘要: 
		//     Identifies the Republic of China (Taiwan) calendar.
		RepublicOfChinaCalendar = 7,
		//
		// 摘要: 
		//     Identifies the Persian calendar.
		PersianCalendar = 8,
		//
		// 摘要: 
		//     Identifies the Indian calendar.
		IndianCalendar = 9,
		//
		// 摘要: 
		//     Identifies the ISO8601.
		ISO8601Calendar = 10,
	}

}
