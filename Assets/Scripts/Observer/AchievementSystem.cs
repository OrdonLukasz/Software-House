using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AchievementSystem : Observer
{
    public List<string> notificationObjects = new List<string>();
    public List<Item> itemsList = new List<Item>();
    public List<ChildrenHolder> inventoryItemList = new List<ChildrenHolder>();

    public Transform inventoryParrent;
    public InformationManager informationManager;

    private void Start()
    {
        PlayerPrefs.DeleteAll();
        foreach (var poi in FindObjectsOfType<PointOfInterest>())
        {
            poi.RegisterObserver(this);
        }
        foreach (Transform child in inventoryParrent)
        {
            inventoryItemList.Add(child.GetComponent<ChildrenHolder>());
        }
    }

    public override void OnNotify(object value, NotificationType notivicationType, ref Text talkingBoubble, NotificationType notificationNPCText)
    {
        if (notivicationType == NotificationType.AchievementUnlocked)
        {
            if (notificationObjects.Count > 0)
            {
                if (notificationObjects[0] == value.ToString())
                {
                    talkingBoubble.text = "Proszę, o to " + itemsList[0].name;

                    for (int i = 0; i < inventoryItemList.Count; i++)
                    {
                        if (inventoryItemList[i].isEmpty == true)
                        {
                            inventoryItemList[i].itemHolderChildren.sprite = itemsList[0].itemSprite;
                            inventoryItemList[i].isEmpty = false;

                            notificationObjects.Remove(value.ToString());
                            itemsList.RemoveAt(0);
                            if (itemsList.Count == 0)
                            {
                                informationManager.OpenAlert();
                            }
                            return;
                        }
                    }
                }
                else
                {
                    talkingBoubble.text = "Idź po " + itemsList[0].name;
                    return;
                }
                }
            }
        }
    }

    public enum NotificationType
    {
        AchievementUnlocked,
        AchievementNPCText
    }
