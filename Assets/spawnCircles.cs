using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//  !!!!! PROBLEM: more spheres are created that destroyed, but the timer for the spawn and for the destruction is the same (5 secs)  ??? !!SOLVED, (PROBLEM WITH TIMER) destroy(objct, timer) => destroy(objct, 5)
// also, the animations are all blended together, when a new set of spheres is created, it follows the same animation as the previous set of spheres ???
public class spawnCircles : MonoBehaviour
{
    public int numObjects = 10;
    [SerializeField] GameObject prefab;   // spherewith no animation, used to clone new spheres.
    [SerializeField] GameObject animated_rond;   // spehere with anim, used only to retrieve getComponent<Animation> / !!!!!!!!! doesn't work 
    private float timer = 5;    
    private GameObject cloned;

    // public Animation anim;
    // Start is called before the first frame update
    void Start() {
         Vector3 center = transform.position;
         for (int i = 0; i < numObjects; i++){
             Vector3 pos = RandomCircle(center, 5.0f);
             Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
             GameObject cloned = Instantiate(prefab, pos, rot);
             cloned.transform.parent = transform;   // used to instantiate the new clone as child of the empty gameObject, so that the spheres are not all regrouped at the same spot (for the animation)
             // cloned.getComponent<Animation> = animated_rond.getComponent<Animation>;
             Destroy (cloned, timer);


         }
     }


    Vector3 RandomCircle ( Vector3 center ,   float radius  ){     //coordinates of a circle, we take a random pos on this circle
        float ang = Random.value * 360;
        Vector3 pos;
        pos.x = center.x + radius * Mathf.Sin(ang * Mathf.Deg2Rad);
        pos.y = center.y + radius * Mathf.Cos(ang * Mathf.Deg2Rad);
        pos.z = center.z;
        return pos;
    }
    
    // Update is called once per frame
    void Update()
    {   Vector3 center = transform.position;
        if (timer < Time.time) { //This checks wether real time has caught up to the timer

          for (int i = 0; i < numObjects; i++){
             Vector3 pos = RandomCircle(center, 5.0f);
             Quaternion rot = Quaternion.FromToRotation(Vector3.forward, center-pos);
             GameObject cloned = Instantiate(prefab, pos, rot);
             cloned.transform.parent = transform;
             // cloned.getComponent<Animation> = animated_rond.getComponent<Animation>;  /// no working :()
             Destroy (cloned, 5);
         }
        timer = Time.time + 5 ; //This sets the timer 5 seconds into the future 
     }
    }
}