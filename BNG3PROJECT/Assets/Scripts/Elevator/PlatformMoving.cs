using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//youtu.be/cW-5JYZLlvQ?si=PSUjY8CERavFwhxJ


//this was here by default
namespace Platformer
{
    public class PlatformMoving : MonoBehaviour
    {
        public bool canMove;

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
        void Update()
        {
            if (Vector3.Distance(transform.position, points[i].position) < 0.01f)
            {
                canMove = false;

                if(i == points.Length - 1)
                {
                    reverse = true;
                    i--;
                    return;
                    
                } else if (i == 0)
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
            if (canMove)
            {
                transform.position = Vector3.MoveTowards(transform.position, points[i].position, speed * Time.deltaTime);
            }
        
        }
    }
}
