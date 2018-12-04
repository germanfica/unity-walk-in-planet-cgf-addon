using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CircularGravityForce;

public class InPlan : MonoBehaviour
{
    public string playerName; // Player name
    public string playerTag; // Player tag
    public float gravity = -10;
    public GameObject planet;
    //public GameObject capsule;

    /* Fix body rotation
     * Necesito alinearlo con el eje de gravedad que lo atrae al objeto.
     * El parámetro está dentro del CGF
     */
    public void FixRotation(Transform body, CGF cgf)
    {
        //Transform planetPos = cgf._forcePositionProperties.ClosestColliders[0].GetComponent<Transform>(); // Planet position

        //Vector3 gravityUp = (body.position - transform.position).normalized;
        //Vector3 gravityUp = (body.position - capsule.transform.position).normalized;
        //Vector3 gravityUp = (body.position - planetPos.transform.position).normalized;
        Vector3 gravityUp = (body.position - planet.transform.position).normalized;
        Vector3 bodyUp = body.up;
        //body.GetComponent<Rigidbody>().AddForce(gravityUp * gravity);
        //Vector3 gvP = cgf._memoryProperties.RaycastHitBuffer;

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;

        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 5 * Time.deltaTime);
    }

    void Awake()
    {
        CGF.OnApplyCGFEvent += CGF_OnApplyCGFEvent;
    }

    void OnDestroy()
    {
        CGF.OnApplyCGFEvent -= CGF_OnApplyCGFEvent;
    }

    private void CGF_OnApplyCGFEvent(CGF cgf, Rigidbody rigid, Collider coll)
    {
        if (rigid.name == "Player") {
            Transform body = rigid.GetComponent<Transform>(); // Body transform
            FixRotation(body, cgf); // Modify body transform rotation
            Debug.Log("Body name: " + body.name + "." + " " + "Tag: " + body.tag);
        }
    }
}