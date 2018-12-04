using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CircularGravityForce;

public class WalkInPlanetAddon : MonoBehaviour
{
    /* Fix body rotation
     * Necesito alinearlo con el eje de gravedad que lo atrae al objeto.
     * El parámetro está dentro del CGF
     */
    void FixRotation(Transform body, CGF cgf)
    {
        // Parte 2
        var point = cgf.FindClosestPoints(transform.position, cgf._forcePositionProperties.ClosestColliders); // LA CLAVEEEEEE
        Transform planet = cgf.transform;
        Debug.Log("Planet name: " + planet.name);

        Vector3 gravityUp = (body.position - planet.transform.position).normalized;
        Vector3 bodyUp = body.up;

        //Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;
        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, body.position - point) * body.rotation;


        body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 7 * Time.deltaTime);
        //body.rotation = targetRotation;

        Debug.DrawLine(transform.position, body.position - point, Color.blue);
    }

    void AlignToSurfaceNormal(Transform body, CGF cgf)
    {
        RaycastHit hit;

        Vector3 castPos = new Vector3(this.transform.position.x, this.transform.position.y - .25f, this.transform.position.z);
        if (Physics.Raycast(castPos, -transform.up, out hit))
        {
            //Debug.DrawLine(body.position, hit.normal, Color.blue);
            var point = cgf.FindClosestPoints(transform.position, cgf._forcePositionProperties.ClosestColliders); // LA CLAVEEEEEE

            //Debug.DrawLine(transform.position, point, Color.blue);
            //Debug.DrawLine(cgf.transform.position + ((cgf.transform.rotation * Vector3.down) * cgf.BoxSize.y), transform.position, Color.blue);

            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            //Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
        }
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
        if (rigid.name == this.name && rigid.tag == this.tag && rigid.gameObject == gameObject)
        {
            Transform body = rigid.GetComponent<Transform>(); // Body transform
            FixRotation(body, cgf); // Modify body transform rotation
            //AlignToSurfaceNormal(body, cgf);
            Debug.Log("Body name: " + body.name + "." + " " + "Tag: " + body.tag);
        }
    }
}