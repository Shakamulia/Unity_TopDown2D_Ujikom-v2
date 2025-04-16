using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TriggerZone : MonoBehaviour
{
    public bool OneShot = false;
    private bool AlreadyEntered = false; // To check if the player has already entered the trigger zone
    private bool AlreadyExited = false; // To check if the player has already exited the trigger zone
    public GameObject InteractionHint; // UI 'E' hint

    public string collisionTag;

    public UnityEvent OnTriggerEnter;
    public UnityEvent OnTriggerExit;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (AlreadyEntered)
        {
            return;
            Debug.Log("Berhasil");
        }

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
        {
            return;
            Debug.Log("Error");
        }

        OnTriggerEnter?.Invoke();

        Debug.Log("Triggered!");

        if(OneShot)
            AlreadyEntered = true;
            
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (AlreadyExited)
            return;

        if (!string.IsNullOrEmpty(collisionTag) && !collision.CompareTag(collisionTag))
        {
            return;
        }

        OnTriggerExit?.Invoke();

        if (OneShot)
            AlreadyExited = true;
    }
    

}

