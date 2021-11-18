using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class GrabbableInTrigger : MonoBehaviour
{
    /// <summary>
    /// All grabbables in a trigger that are valid
    /// </summary>
    public Dictionary<Collider, XRGrabInteractable> nearbyGrabbables;

    /// <summary>
    /// all grabbables that are valid (within range)
    /// </summary>
    public Dictionary<Collider, XRGrabInteractable> validGrabbables;

    /// <summary>
    /// The closest grabbable object
    /// </summary>
    public XRGrabInteractable closestGrabbable;

    /// <summary>
    /// all the grabbales in the trigger that are considered valid
    /// </summary>
    public Dictionary<Collider, XRGrabInteractable> validRemoteGrabbables;


    // cache variables
    private XRGrabInteractable _closets;
    private float _lastDistance;
    private float _thisDistance;
    private Dictionary<Collider, XRGrabInteractable> _valids;
    private Dictionary<Collider, XRGrabInteractable> _filtered;


    // Start is called before the first frame update
    void Start()
    {
        nearbyGrabbables = new Dictionary<Collider, XRGrabInteractable>();
        validGrabbables = new Dictionary<Collider, XRGrabInteractable>(); 
        validRemoteGrabbables = new Dictionary<Collider, XRGrabInteractable>();
    }

    public virtual XRGrabInteractable GetClosestGrabbable(Dictionary<Collider, XRGrabInteractable> grabbables, bool remoteOnly = false)
    {
        _closets = null;
        _lastDistance = 9999f;

        if(grabbables == null)
        {
            return null;
        }

        foreach (var KeyValuePair in grabbables)
        {
            if(KeyValuePair.Value == null || !KeyValuePair.Value.enabled)
            {
                continue;
            }

            // using collider transform as a position
            _thisDistance = Vector3.Distance(KeyValuePair.Value.transform.position, transform.position);
            if(_thisDistance < _lastDistance && KeyValuePair.Value.isActiveAndEnabled)
            {
                // this will not be a valie option
                if(remoteOnly && !KeyValuePair.Value.selectingInteractor)
                {
                    continue;
                }

                // we aren't within a remote grab range
                if(remoteOnly && _thisDistance > KeyValuePair.Value.tightenPosition)
                {
                    continue;
                }


                // this will be our closest grabbable
                _lastDistance = _thisDistance;
                _closets = KeyValuePair.Value;
            }
        }

        return _closets;
    }

    /// <summary>
    /// Remove any grabbables that have been destoried or deactivated
    /// </summary>
    /// <param name="grabs"></param>
    /// <returns></returns>
    public virtual Dictionary<Collider, XRGrabInteractable> CleanedGrabbables(Dictionary<Collider, XRGrabInteractable> grabs)
    {
        _filtered = new Dictionary<Collider, XRGrabInteractable>();
        if(grabs == null)
        {
            return _filtered;
        }

        foreach(var grab in grabs)
        {
            if(grab.Key!=null && grab.Key.enabled && grab.Value.isActiveAndEnabled)
            {
                if (grab.Value.tightenPosition > 0 && Vector3.Distance(grab.Key.transform.position, transform.position) > grab.Value.tightenPosition)
                {
                    continue;
                }

                _filtered.Add(grab.Key, grab.Value);
            }
        }
        return _filtered;
    }

    protected virtual bool isValidGrabbale(Collider collider, XRGrabInteractable grab)
    {
        // if the obejct has been deactivated, remove it
        if(collider == null || grab == null || !grab.isActiveAndEnabled || !collider.enabled)
        {
            return false;
        }

        // not considered grabbable any more (may have been picked up)
        else if (!grab.enabled)
        {
            return false;
        }
        // dock zone without an item isn't a valid grab, skip unless something else is inside
        //else if()
        //{
        //    return false;
        //}

        else if (grab == closestGrabbable)
        {
            if (grab.tightenPosition > 0 && Vector3.Distance(grab.transform.position, transform.position) > grab.tightenPosition) ;
            return false;
        }

        return true;
    }

    public virtual void AddNearybyGrabbable(Collider collider, XRGrabInteractable grabbableObject)
    {
        if(nearbyGrabbables == null)
        {
            nearbyGrabbables = new Dictionary<Collider, XRGrabInteractable>();
        }
        if(grabbableObject != null && !nearbyGrabbables.ContainsKey(collider))
        {
            nearbyGrabbables.Add(collider, grabbableObject);
        }
    }

    public virtual void RemoveNearbyGrabbable(Collider collider, XRGrabInteractable grabbableObject)
    {
        if(grabbableObject != null && nearbyGrabbables != null && nearbyGrabbables.ContainsKey(collider))
        {
            nearbyGrabbables.Remove(collider);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if(grab != null)
        {
            AddNearybyGrabbable(other, grab);
            return;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        XRGrabInteractable grab = other.GetComponent<XRGrabInteractable>();
        if (grab != null)
        {
            RemoveNearbyGrabbable(other, grab);
        }
    }
}
