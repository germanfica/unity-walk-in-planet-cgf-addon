using UnityEngine;
using System.Collections;

namespace xyz.germanfica.unity.planet.gravity
{
    public class PlayerController : MonoBehaviour
    {
        public Rigidbody rig;
        RaycastHit hit;
        public bool freezeRotation = true;

        public int forceConst = 4;
        private bool canJump;

        private Animator anim;
        private bool MOVING;
        private bool ROTATING;

        public bool vThirdPersonController = false;

        void Start()
        {
            Ini();
        }

        void Update()
        {
            // Raycast (doesn't affect gameplay)
            if (Physics.Raycast(transform.position, -transform.up, out hit))
            {
                Debug.DrawLine(transform.position, hit.point, Color.cyan);
            }
            // Jump Action
            if (Input.GetKeyUp(KeyCode.Space))
            {
                canJump = true;
            }
        }

        void FixedUpdate()
        {
            Move();
            Jump();
        }

        /* Some initializations
         */
        private void Ini()
        {
            rig.useGravity = false; // Disables Gravity
            if (freezeRotation)
            {
                rig.constraints = RigidbodyConstraints.FreezeRotation;
            }
            else
            {
                rig.constraints = RigidbodyConstraints.None;
            }

            if (vThirdPersonController)
            {
                try
                {
                    anim = GetComponent<Animator>();
                }
                catch
                {
                    Debug.LogError("Animator component is missing.");
                }
            }
        }

        /* Character jump
         */
        private void Jump()
        {
            if (canJump)
            {
                canJump = false;
                // AddForce (useless)
                //rig.AddForce(0, forceConst, 0, ForceMode.Impulse);
                // AddForceAtPosition (useless too)
                //rig.AddForceAtPosition(new Vector3(0, 0, forceConst), rig.transform.position, ForceMode.Impulse);
                // AddRelativeForce (successful)

                // Jump animation
                // trigger jump animations
                if (vThirdPersonController)
                {
                    Vector2 input = new Vector2(anim.GetFloat(0), anim.GetFloat(1));
                    if (input.sqrMagnitude < 0.1f)
                    {
                        anim.CrossFadeInFixedTime("Jump", 0.1f);
                    }
                    else
                    {
                        anim.CrossFadeInFixedTime("JumpMove", .2f);
                    }
                }

                rig.AddRelativeForce(0, forceConst, 0, ForceMode.Impulse);
            }
        }

        /* Character movement
         */
        private void Move()
        {
            var x = Input.GetAxis("Horizontal") * Time.deltaTime * 150.0f;
            var z = Input.GetAxis("Vertical") * Time.deltaTime * 3.0f;

            //Debug.Log("x value: " + z);

            // condition ? consequence : alternative
            MOVING = z > 0 ? MOVING = true : MOVING = false;
            ROTATING = x > 0 ? ROTATING = true : ROTATING = false;

            //Debug.Log("Is moving? " + MOVING);
            if (vThirdPersonController)
            {
                // Movement Animation
                if (MOVING)
                {
                    // Move animation
                    anim.SetFloat("InputMagnitude", 1, 0.25f, Time.deltaTime); // 1 si el personaje se mueve
                }
                else
                {
                    // Unmove animation
                    anim.SetFloat("InputMagnitude", 0, 0.25f, Time.deltaTime); // 0 si el personaje no se mueve
                }
            }

            transform.Rotate(0, x, 0);
            transform.Translate(0, 0, z);
        }
    }
}