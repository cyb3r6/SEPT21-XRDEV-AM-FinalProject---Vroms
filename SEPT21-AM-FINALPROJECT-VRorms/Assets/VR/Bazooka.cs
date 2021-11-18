using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;


[RequireComponent(typeof(XRGrabInteractable))]
public class Bazooka : MonoBehaviour
{
    public Transform spawnPoint;
    public float shootingForce;
    public GameObject bulletPrefab;

    private XRGrabInteractable interactable_base;

    void Start()
    {
        interactable_base = GetComponent<XRGrabInteractable>();
        interactable_base.activated.AddListener(TriggerPulled);
    }

    private void TriggerPulled(ActivateEventArgs args)
    {
        GameObject shot = Instantiate(bulletPrefab, spawnPoint.position, spawnPoint.rotation);


        shot.GetComponent<Rigidbody>().AddForce(shot.transform.forward * shootingForce);

    }
}
