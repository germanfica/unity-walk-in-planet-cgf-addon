using CircularGravityForce;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using xyz.germanfica.unity.planet.gravity;

/**
 * Add to Environment > Align Example >
 * 
 * En este script quería entender las físicas de Unity
 * entonces, este script tiene la función de
 * habilitar la gravedad y deshabilitarla,
 * remover las restricciones de rotación
 * y bloquearlas para que esté siempre a 90 grados con 
 * respecto al eje centrarl de gravedad de Unity.
 */
namespace xyz.germanfica.gravity {
    public class Commands : MonoBehaviour {
        public PlayerController playerController;

        void LateUpdate () {
            if (Input.GetKeyDown(KeyCode.F11))
            {
                if (GetComponent<CGF>().Enable)
                {
                    GetComponent<CGF>().Enable = false;
                    playerController.freezeRotation = true;
                    playerController.rig.constraints = RigidbodyConstraints.FreezeRotation;
                    playerController.transform.rotation = new Quaternion (0, 0, 0, 0);
                }
                else {
                    GetComponent<CGF>().Enable = true;
                    playerController.freezeRotation = false;
                    playerController.rig.constraints = RigidbodyConstraints.None;
                }
            }

            try
            {
                if (Input.GetKeyDown(KeyCode.Keypad8))
                {
                    CGF_AlignToForce cgf_AlignToForce = GetComponent<CGF_AlignToForce>();
                    cgf_AlignToForce._alignDirection = CGF_AlignToForce.AlignDirection.Up;
                }

                if (Input.GetKeyDown(KeyCode.Keypad2))
                {
                    CGF_AlignToForce cgf_AlignToForce = GetComponent<CGF_AlignToForce>();
                    cgf_AlignToForce._alignDirection = CGF_AlignToForce.AlignDirection.Down;
                }

                if (Input.GetKeyDown(KeyCode.Keypad4))
                {
                    CGF_AlignToForce cgf_AlignToForce = GetComponent<CGF_AlignToForce>();
                    cgf_AlignToForce._alignDirection = CGF_AlignToForce.AlignDirection.Left;
                }

                if (Input.GetKeyDown(KeyCode.Keypad6))
                {
                    CGF_AlignToForce cgf_AlignToForce = GetComponent<CGF_AlignToForce>();
                    cgf_AlignToForce._alignDirection = CGF_AlignToForce.AlignDirection.Right;
                }
            }
            catch {
                Debug.Log("Hubo un error");
            }
        }
    }
}
