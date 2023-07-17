using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionController : MonoBehaviour
{
    //Etkileþime girilecek objede rigidbody olmasý gerekiyor ayrýca interpolate seçeneðini interpolate olarak none seçeneðinden deðiþtirmek gerekiyor.(tag = Interactable)
    [Header("Interaction Settings")]
    [SerializeField] float interactionDistance = 2f;
    [SerializeField] float interactionSpeed = 1f;
    [SerializeField] KeyCode interactKey = KeyCode.E;

    public bool isInteracting = false;
    private GameObject interactedObject;
    private Vector3 firstOffset;

    private CharacterController characterController;

    private void Start()
    {
        characterController = GetComponent<CharacterController>();
    }
    private void Update()
    {
        if (Input.GetKeyDown(interactKey))
        {
            if (isInteracting)
            {
                StopInteraction();
            }
            else
            {
                StartInteraction();
            }
        }
        if (isInteracting && characterController.isGrounded)
        {
            MoveObjectWithCharacter();
        }
        else if (!characterController.isGrounded)
        {
            StopInteraction();
        }
    }
    private void StartInteraction()
    {
        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, interactionDistance))
        {
            Rigidbody rb = hit.collider.GetComponent<Rigidbody>();
            if (rb != null)
            {
                interactedObject = hit.collider.gameObject;
                firstOffset = interactedObject.transform.position - transform.position;
                isInteracting = true;
            }
        }
    }
    private void StopInteraction()
    {
        if (interactedObject != null)
        {
            interactedObject = null;
        }
        isInteracting = false;
    }
    private void MoveObjectWithCharacter()
    {
        if (interactedObject != null)
        {
            Vector3 characterMovement = characterController.velocity * Time.deltaTime;

            interactedObject.transform.position = transform.position + firstOffset + characterMovement * interactionSpeed;
        }
    }
}