using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class Interactable : MonoBehaviour
{
    [SerializeField]
    private GameObject interactWith;
    [SerializeField]
    private bool wasDisplayed;

    public GameObject NpcMessage;
    public GameObject PrefabNpcMessage;

    public bool isInteracting;

    public string communicateAction;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            NpcMessage = Instantiate(PrefabNpcMessage, transform);
            NpcMessage.transform.position = transform.position + new Vector3(0, 3, 0);
            Vector3 newRotation = new Vector3(0, -90, 0);
            NpcMessage.transform.eulerAngles = newRotation;
            wasDisplayed = true;
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
