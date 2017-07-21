using UnityEngine;
using UnitySampleAssets.CrossPlatformInput;
 

namespace CompleteProject
{
    public class PlayerSelection : MonoBehaviour
    {
        public Renderer rend;
        public bool IsSelected = false;



        void Start()
        {

        }

        void Update()
        {
            if (IsSelected)
            {
                rend.enabled = true;
            }
            else
            {
                rend.enabled = false;
            }
        }
    }
}