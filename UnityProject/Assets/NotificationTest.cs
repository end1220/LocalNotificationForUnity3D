using UnityEngine;
using System;
using System.Collections;
using LocalNotification;

public class NotificationTest : MonoBehaviour
{
	private NotificationManager ntf = new NotificationManager();

	void Awake()
    {
		ntf.ClearAllNotifications();
	}

	private void OnApplicationPause(bool pause)
	{
		if (pause)
		{
			string body = "sixsixsixxxxxxxxxxsss";
			ntf.RegisterNotification(new LocalNotification.NotificationData("111", body, DateTime.Now.AddSeconds(5), false));

			ntf.RegisterNotification(new LocalNotification.NotificationData("repeat", body, DateTime.Now.AddSeconds(5), true));
		}
		else
			ntf.ClearAllNotifications();
	}

	private void OnGUI()
	{
		float sx = 10;
		float sy = 10;
		float w = 100;
		float h = 20;
		int index = 3;

		if (GUI.Button(new Rect(sx, sy + index * h, w, h), "OneTime"))
		{
			DateTime fireDateTime = DateTime.Now.AddSeconds(5);
			string title = "2333";
			string body = "sixsixsixxxxxxxxxxsss";
			var notiItem = new LocalNotification.NotificationData(title, body, fireDateTime, false);
			ntf.RegisterNotification(notiItem);
		}
		index++;

	}

	/*public void OneTime()
    {
        LocalNotification.SendNotification(1, 5000, "Title", "Long message text", new Color32(0xff, 0x44, 0x44, 255));
    }

    public void OneTimeBigIcon()
    {
        LocalNotification.SendNotification(1, 5000, "Title", "Long message text with big icon", new Color32(0xff, 0x44, 0x44, 255), true, true, true, "app_icon");
    }

    public void OneTimeWithActions()
    {
        LocalNotification.Action action1 = new LocalNotification.Action("background", "In Background", this);
        action1.Foreground = false;
        LocalNotification.Action action2 = new LocalNotification.Action("foreground", "In Foreground", this);
        LocalNotification.SendNotification(1, 5000, "Title", "Long message text with actions", new Color32(0xff, 0x44, 0x44, 255), true, true, true, null, "boing", "default", action1, action2);
    }

    public void Repeating()
    {
        LocalNotification.SendRepeatingNotification(1, 5000, 60000, "Title", "Long message text", new Color32(0xff, 0x44, 0x44, 255));
    }

    public void Stop()
    {
        LocalNotification.CancelNotification(1);
    }

    public void OnAction(string identifier)
    {
        Debug.Log("Got action " + identifier);
    }*/
}
