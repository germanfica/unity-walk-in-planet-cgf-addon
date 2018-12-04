using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AlignToSurfaceNormal : MonoBehaviour {
    
    // align to surface normal 
    void Update()
    {
        RaycastHit hit;
        
        Vector3 castPos = new Vector3(this.transform.position.x, this.transform.position.y - .25f, this.transform.position.z);
        if (Physics.Raycast(castPos, -transform.up, out hit))
        {
            transform.rotation = Quaternion.FromToRotation(Vector3.up, hit.normal);

            //Quaternion targetRotation = Quaternion.FromToRotation(Vector3.up, hit.normal);
            //transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 7 * Time.deltaTime);
        }
    }
}
