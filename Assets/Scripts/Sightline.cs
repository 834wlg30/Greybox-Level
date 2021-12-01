using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sightline : MonoBehaviour
{
    public Transform eyePoint;
    public string targetTag = "Player";
    public float fov = 30f;
    public bool targetInSight { get; set; } = false;
    public Vector3 lastKnownPos { get; set; } = Vector3.zero;

    private SphereCollider collider;

    private void Awake()
    {
        collider = GetComponent<SphereCollider>();
        lastKnownPos = transform.position;
    }

    private void OnTriggerStay(Collider other)
    {
        
        if (other.CompareTag(targetTag))
        {
            
            updateSight(other.transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag(targetTag))
        {
            targetInSight = false;
        }
    }

    private bool canSeeTarget(Transform tgt)
    {
        RaycastHit info;
        Vector3 dirToTgt = (tgt.position - eyePoint.position).normalized;

        if(Physics.Raycast(eyePoint.position, dirToTgt, out info, collider.radius))
        {
            if (info.transform.CompareTag(targetTag))
            {
                return true;
            }
        }
        
        return false;
    }

    private bool targetInFOV(Transform tgt)
    {
        Debug.Log("Checking angle");
        Vector3 dirToTgt = tgt.position - eyePoint.position;
        float angle = Vector3.Angle(eyePoint.forward, dirToTgt);

        if(angle <= fov)
        {
            Debug.Log("Target in FOV");
            return true;
        }

        return false;
    }

    private void updateSight(Transform tgt)
    {
        Debug.Log("Player in range");
        targetInSight = canSeeTarget(tgt) && targetInFOV(tgt);

        if (targetInSight)
        {
            Debug.Log("Player in sight");
            lastKnownPos = tgt.position;
        }
    }
}
