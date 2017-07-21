using UnityEngine;
using System.Collections;

namespace CompleteProject
{
    public class CameraMove : MonoBehaviour
    {
        public float smoothing = 5f;        // The speed with which the camera will be following.
        public Camera camera;
        
        
        Vector3 target = Vector3.zero;            // The position that that camera will be following.
        Vector3 offset;                     // The initial offset from the target.


        void Start()
        {
            camera = GetComponent<Camera>();

            // Calculate the initial offset.
            offset = transform.position - target;
        }


        void FixedUpdate()
        {
            // Store the input axes.
            float trans_h = Input.GetAxisRaw("Horizontal");
            float trans_v = Input.GetAxisRaw("Vertical");
            float wheel = Input.GetAxis("Mouse ScrollWheel");
            float rot_h = Input.GetAxis("Mouse X");
            float rot_v = Input.GetAxis("Mouse Y");

            // Move the player around the scene.
            Move(trans_h, trans_v);

            // Move the player around the scene.
            Zoom(wheel);
            

            // Create a postion the camera is aiming for based on the offset from the target.
            Vector3 targetCamPos = target + offset;

            // Smoothly interpolate between the camera's current position and it's target position.
            transform.position = Vector3.Lerp(transform.position, targetCamPos, smoothing * Time.deltaTime);

            if (Input.GetMouseButton(1))
            {
                transform.RotateAround(target, Vector3.up, rot_h * 2);
                transform.RotateAround(target, Vector3.right, rot_v * 2);
            }

            if (Input.GetButtonDown("Fire1"))
            {
                Ray ray = camera.ScreenPointToRay (Input.mousePosition); // ray from camera to mouse
                
                RaycastHit hit = new RaycastHit(); // impact point
                
                if(Physics.Raycast(ray, out hit, 10000))
                {
                    if (hit.collider.tag == "zombie")
                    {
                        PlayerSelection script = hit.collider.GetComponent<PlayerSelection>();
                        if(script.IsSelected)
                            script.IsSelected = false;
                        else
                            script.IsSelected = true;

                        Debug.DrawLine (ray.origin, hit.point); //test pour debug le rayon
                    }
                    

                    //hit.collider.SendMessageUpwards("afficherInfo",info,SendMessageOptions.DontRequireReceiver);//si le ray touche un objet, il test voir si cet objet posséde une fonction affiche info
                    //si oui il active cette fonction dans le script de l'objet en question.
                }
            }
        }

        void Move(float h, float v)
        {
            // Set the movement vector based on the axis input.
            target.x += h;
            target.z += v;
        }

        void Zoom(float w)
        {
            if((transform.position.y > 100 && w > 0) || (transform.position.y < 250 && w < 0))
                target.y -= w*30;
        }

        void Turning(float r)
        {
            if (Input.GetMouseButton(0))
            {
                transform.LookAt(target);
            }
        }
    }
}