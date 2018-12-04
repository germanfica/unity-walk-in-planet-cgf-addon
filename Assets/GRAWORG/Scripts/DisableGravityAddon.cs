using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CircularGravityForce;

public class DisableGravityAddon : MonoBehaviour {
    private bool oneTime = false;

    void Awake()
    {
        CGF.OnApplyCGFEvent += CGF_OnApplyCGFEvent;
    }

    void OnDestroy()
    {
        CGF.OnApplyCGFEvent -= CGF_OnApplyCGFEvent;
    }

    /*
     * rigbody.constraints = RigidbodyConstraints.FreezeRotation; // Me sirve
     * rigbody.transform.rotation = new Quaternion(0, 0, 0, 0); // Me sirve
     */
    private void CGF_OnApplyCGFEvent(CGF cgf, Rigidbody rigid, Collider coll)
    {
        // Esto se ejecuta una vez
        if (rigid.name == this.name && rigid.tag == this.tag && rigid.gameObject == gameObject && !oneTime)
        {
            Transform body = rigid.GetComponent<Transform>(); // Body transform
            rigid.constraints = RigidbodyConstraints.None;
            oneTime = true;
            Debug.Log("Body name: " + body.name + "." + " " + "Tag: " + body.tag);
        }

        // ESTO FUNCIONA MAL
        //isTaskComplete or once executed or isExecuted
        // ACA TENGO QUE PREGUNTAR SI SALIO DEL PERSONAJE
        // SI EL OBJETO YA NO ESTA EN LA ORBITA ENTONCES HACER TAL COSA
        //GetComponent<Rigidbody>().constraints = RigidbodyConstraints.FreezeRotation; // Me sirve
        //GetComponent<Rigidbody>().transform.rotation = new Quaternion(0, 0, 0, 0); // Me sirve
        Debug.Log(cgf.Size);

        

    }
}
