using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : Observer
{

    public List<string> notificationObjects = new List<string>();
    public List<Item> itemsList = new List<Item>();

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        foreach (var poi in FindObjectsOfType<PointOfInterest>())
        {
            poi.RegisterObserver(this);
        }
    }

    public override void OnNotify(object value, NotificationType notivicationType, ref Text talkingBoubble, NotificationType notificationNPCText)
    {

        if (notivicationType == NotificationType.AchievementUnlocked)
        {
            if (notificationObjects[0] == value.ToString())
            {
                talkingBoubble.text = "Proszę, o to " + itemsList[0].name;
                notificationObjects.Remove(value.ToString());
                itemsList.RemoveAt(0);
               
                return;
            }
            else
            {
                talkingBoubble.text = "Idź po " + itemsList[0].name;
                return;
            }

        }
    }
}
public enum NotificationType
{
    AchievementUnlocked,
    AchievementNPCText
}
