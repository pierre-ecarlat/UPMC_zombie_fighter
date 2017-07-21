using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;

namespace CompleteProject
{
    public class PlayerMovement : MonoBehaviour
    {
        public float speed = 10f;            // The speed that the player will move at.
        public int maxTempo = 100;

        Vector3 movement;                   // The vector to store the direction of the player's movement.
        Animator anim;                      // Reference to the animator component.
        Rigidbody playerRigidbody;          // Reference to the player's rigidbody.
        int floorMask;                      // A layer mask so that a ray can be cast just at gameobjects on the floor layer.
        float camRayLength = 100f;          // The length of the ray from the camera into the scene.

        static System.Random rnd = new System.Random();

        int currTempo = 20;
        int cpt = 0;
        float h = 0;
        float v = 0;

        void Awake()
        {
            // Create a layer mask for the floor layer.
            floorMask = LayerMask.GetMask("Floor");

            // Set up references.
            anim = GetComponent<Animator>();
            playerRigidbody = GetComponent<Rigidbody>();
        }


        void FixedUpdate()
        {
            // Store the input axes.
            if (cpt == currTempo)
            {
                h = rnd.Next(3) - 1;//Input.GetAxisRaw("Horizontal");
                v = rnd.Next(3) - 1;//Input.GetAxisRaw("Vertical");
                cpt = 0;
                currTempo = rnd.Next(10, maxTempo + 1);
            }
            cpt++;
            
            // Move the player around the scene.
            Move(h, v);

            // Turn the player to face the mouse cursor.
            Turning();

            // Animate the player.
            Animating(h, v);
        }
        
        void Move(float h, float v)
        {
            // Set the movement vector based on the axis input.
            movement.Set(h, 0f, v);

            // Normalise the movement vector and make it proportional to the speed per second.
            movement = movement.normalized * speed * Time.deltaTime;

            // Move the player to it's current position plus the movement.
            playerRigidbody.MovePosition(transform.position + movement);
        }
        
        void Turning()
        {
            /*
            // Create a ray from the mouse cursor on screen in the direction of the camera.
            Ray camRay = Camera.main.ScreenPointToRay(Input.mousePosition);

            // Create a RaycastHit variable to store information about what was hit by the ray.
            RaycastHit floorHit;

            // Perform the raycast and if it hits something on the floor layer...
            if (Physics.Raycast(camRay, out floorHit, camRayLength, floorMask))
            {
                // Create a vector from the player to the point on the floor the raycast from the mouse hit.
                Vector3 playerToMouse = floorHit.point - transform.position;

                // Ensure the vector is entirely along the floor plane.
                playerToMouse.y = 0f;

                // Create a quaternion (rotation) based on looking down the vector from the player to the mouse.
                Quaternion newRotation = Quaternion.LookRotation(playerToMouse);

                // Set the player's rotation to this new rotation.
                playerRigidbody.MoveRotation(newRotation);
            }
             */
        }
        
        void Animating(float h, float v)
        {
            // Create a boolean that is true if either of the input axes is non-zero.
            bool walking = h != 0f || v != 0f;

            // Tell the animator whether or not the player is walking.
            anim.SetBool("IsWalking", walking);
        }
    }
}