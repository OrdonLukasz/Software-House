using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Interactable : MonoBehaviour
{
    public GameObject NpcMessage;
    public GameObject PrefabNpcMessage;

    public bool isInteracting;
    
    [SerializeField]
    private GameObject interactWith;
    [SerializeField]
    private float messageOffsetY = 3.0f;
    [SerializeField]
    private float messageOffsetX;
    [SerializeField]
    private float messageRotationOffset = -180f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NpcMessage = Instantiate(PrefabNpcMessage, transform);
            NpcMessage.transform.position = transform.position + new Vector3(messageOffsetX, messageOffsetY, 0);
            Vector3 newRotation = NpcMessage.transform.eulerAngles;
            newRotation = new Vector3(0, messageRotationOffset, 0);
            NpcMessage.transform.eulerAngles = newRotation;
            NpcMessage.SetActive(true);
            isInteracting = true;
            interactWith = other.gameObject;
            NpcMessage.transform.GetChild(0).GetComponent<Text>().text = "Press E to interact";
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            isInteracting = false;
            interactWith = null;
            Destroy(NpcMessage);
        }
    }
}
