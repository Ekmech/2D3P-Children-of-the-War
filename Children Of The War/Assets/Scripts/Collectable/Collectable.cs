using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : MonoBehaviour
{
    [SerializeField] private float interactionRange = 3f;

    private GameObject player;
    private bool canInteract = false;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
    void Update()
    {
        Interact();
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, interactionRange);
    }
    private void Interact()
    {
        float distance = Vector3.Distance(transform.position, player.transform.position);
        if (distance <= interactionRange)
        {
            canInteract = true;
            Debug.Log("Press 'F' to collect " + gameObject.tag);
        }
        else
        {
            canInteract = false;
        }
        if (canInteract && Input.GetKeyDown(KeyCode.F))
        {
            CollectItem();
        }
    }
    private void CollectItem()
    {
        Destroy(gameObject);
        Debug.Log("Collected");
    }
}