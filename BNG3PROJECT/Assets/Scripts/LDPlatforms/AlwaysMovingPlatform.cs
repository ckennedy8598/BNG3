using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//adapted from PlatformMoving to make platforms that always move regardless of player detection. doesnt need any other script to do its job really but ParentPlatform
// should always be involved to logically
namespace Platformer
{
    public class AlwaysMovingPlatform : MonoBehaviour
    {
      

        [SerializeField] float speed;
        [SerializeField] int startPoint;
        [SerializeField] Transform[] points;

        int i;
        bool reverse;


        // Start is called before the first frame update
        void Start()
        {
            transform.position = points[startPoint].position;
            i = startPoint;


        }

        // Update is called once per frame
       
        void FixedUpdate()
        {
            //checks if platform hit destination
            if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
            {
            
                //handles reversing or moving to next point
                if (i == points.Length - 1)
                {
                    reverse = true;
                    i--;
                    return;

                }
                else if (i == 0)
                {
                    reverse = false;
                    i++;
                    return;
                }

                if (reverse)
                {
                    i--;
                }
                else
                {
                    i++;
                }
            }
      
            
            transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
            

        }
    }
}
