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
        Transform planet = cgf.transform;
        Debug.Log("Planet name: " + planet.name);

        Vector3 gravityUp = (body.position - planet.transform.position).normalized;
        Vector3 bodyUp = body.up;

        Quaternion targetRotation = Quaternion.FromToRotation(bodyUp, gravityUp) * body.rotation;

        //body.rotation = Quaternion.Slerp(body.rotation, targetRotation, 7 * Time.deltaTime);
        body.rotation = targetRotation;
    }

    void AlignToSurfaceNormal(Transform body, CGF cgf)
    {
        RaycastHit hit;
        Transform planet = cgf.transform;
        Vector3 gravityUp = body.position+(body.position + planet.transform.position).normalized;
        //Debug.DrawLine(body.position, gravityUp, Color.blue);

        Vector3 castPos = new Vector3(body.transform.position.x, body.transform.position.y - .25f, body.transform.position.z);
        if (Physics.Raycast(castPos, -transform.up, out hit))
        {
            //Debug.DrawLine(body.position, hit.normal, Color.blue);
            var point = cgf.FindClosestPoints(body.position, cgf._forcePositionProperties.ClosestColliders); // LA CLAVEEEEEE

            Debug.DrawLine(body.position, point, Color.blue);

            body.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

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
            //FixRotation(body, cgf); // Modify body transform rotation
            AlignToSurfaceNormal(body, cgf);
            Debug.Log("Body name: " + body.name + "." + " " + "Tag: " + body.tag);
        }
    }
}