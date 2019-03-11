using UnityEngine;
using System.Collections;

namespace  PC2D
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target;
        float cameraDistance = 12f;
        float newY;
        // Update is called once per frame
        void Update()
        {
            Vector3 pos = transform.position;
            pos.x = target.position.x;
            pos.y = target.position.y;

            // transform.position = pos;   Orig

            //*************** Phil version
            newY = pos.y / 5;
            transform.position = new Vector3(pos.x, newY, -10);
            

            // another vesrion
            //transform.position = new Vector3(pos.x + 3, target.position.y + 6, target.position.z - cameraDistance);

        }


    }
}
