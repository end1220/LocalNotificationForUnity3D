package com.mjj.local_notify;
 
import java.util.Calendar;
 
import android.app.Activity;
import android.app.AlarmManager;
import android.app.Notification;
import android.app.NotificationManager;
import android.app.PendingIntent;
import android.content.BroadcastReceiver;
import android.content.Context;
import android.content.Intent;
import android.content.pm.ApplicationInfo;
import android.content.pm.PackageManager;
import android.os.Bundle;
 
import com.unity3d.player.UnityPlayer;
 
 
/**
 * 用于生成 / 清除本地通知推送的插件
 * 仅在安卓平台有效
 *
 * @author Weiren
 *
 */
public class AndroidNotificator extends BroadcastReceiver {
 
    private static int m_nLastID = 0;
         
     
    /**
     * 显示数秒后的通知
     *
     * @param pAppName 应用名
     * @param pTitle 通知标题
     * @param pContent 通知内容
     * @param pDelaySecond 延迟时间
     * @param pIsDailyLoop 是否每日自动推送
     * @throws IllegalArgumentException
     */
    public static void ShowNotification(String pAppName, String pTitle, String pContent, int pDelaySecond, boolean pIsDailyLoop) throws IllegalArgumentException { 
         
        if(pDelaySecond < 0)
        {
            throw new IllegalArgumentException("The param: pDelaySecond < 0");
        }
         
        Activity curActivity = UnityPlayer.currentActivity;
         
        Intent intent = new Intent("UNITY_NOTIFICATOR");
        intent.putExtra("appname", pAppName);
        intent.putExtra("title", pTitle);
        intent.putExtra("content", pContent);
        PendingIntent pi =  PendingIntent.getBroadcast(curActivity, 0, intent, 0);
         
        AlarmManager am = (AlarmManager)curActivity.getSystemService(Context.ALARM_SERVICE);
        Calendar calendar = Calendar.getInstance(); 
        calendar.add(Calendar.SECOND, pDelaySecond);
        long alarmTime = calendar.getTimeInMillis(); 
         
        if (pIsDailyLoop){
            am.setRepeating(
                    AlarmManager.RTC_WAKEUP,
                    alarmTime,
                    86400, // 24 hours
                    pi);
        } else {
            am.set(
                    AlarmManager.RTC_WAKEUP,
                    alarmTime,
                    pi);  
        }
    }  
     
     
    /**
     * 清除所有通知，包括日常通知
     */
    public static void ClearNotification() {
         
        Activity act = UnityPlayer.currentActivity;
        NotificationManager nManager = (NotificationManager)act.getSystemService(Context.NOTIFICATION_SERVICE);
         
        for(int i = m_nLastID; i >= 0; i--) {
            nManager.cancel(i);
        }
         
        m_nLastID = 0;
    }
     
     
    @SuppressWarnings("deprecation")
    public void onReceive(Context pContext, Intent pIntent) {
         
        Class<?> unityActivity = null;
        try {
            unityActivity = pContext.getClassLoader().loadClass("com.unity3d.player.UnityPlayerProxyActivity");
        } catch (Exception ex) {   
            ex.printStackTrace(); 
            return;     
        }     
         
        ApplicationInfo applicationInfo = null;
        PackageManager pm = pContext.getPackageManager(); 
         
        try {             
            applicationInfo = pm.getApplicationInfo(pContext.getPackageName(), PackageManager.GET_META_DATA); 
        } catch (Exception ex) {
            ex.printStackTrace();
            return;    
        }    
         
        Bundle bundle = pIntent.getExtras();
        
        /*Notification notification = new Notification.Builder(pContext)    
                .setAutoCancel(true)    
                .setContentTitle("title")    
                .setContentText("describe")    
                .setContentIntent(pendingIntent)    
                .setSmallIcon(R.drawable.ic_launcher)    
                .setWhen(System.currentTimeMillis())    
                .build();
        
        Notification notification = new Notification(
                applicationInfo.icon,
                (String)bundle.get("appname"),
                System.currentTimeMillis());    
         
        PendingIntent contentIntent = PendingIntent.getActivity(
                pContext,
                m_nLastID,
                new Intent(pContext, unityActivity),
                0);  
        notification.setLatestEventInfo(
                pContext,
                (String)bundle.get("title"),
                (String)bundle.get("content"),
                contentIntent);
         
        NotificationManager nm = (NotificationManager)pContext.getSystemService(Context.NOTIFICATION_SERVICE); 
        nm.notify(m_nLastID, notification); */
         
        m_nLastID++;
    }
}