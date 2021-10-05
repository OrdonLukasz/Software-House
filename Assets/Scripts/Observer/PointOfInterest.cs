using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UI;
public class PointOfInterest : Subject
{
    //  [SerializeField]
    public string achivementName;

    private void Update()
    {
        if (Keyboard.current.eKey.wasPressedThisFrame && transform.parent.GetComponent<Interactable>().isInteracting)
        {
            Text boubbleText = transform.parent.GetComponent<Interactable>().NpcMessage.transform.GetChild(0).GetComponent<Text>();
            Notify(achivementName, NotificationType.AchievementUnlocked, ref boubbleText, NotificationType.AchievementNPCText);
        }
    }
}


