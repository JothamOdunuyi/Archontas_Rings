using System.IO;
using UnityEditor;
using UnityEngine;
using static UnityEngine.GraphicsBuffer;



public class StrafeAroundPlayer : MonoBehaviour
{
    public Transform player; // Reference to the player object
    public float strafeSpeed = 2f; // The speed at which to strafe
    public float strafeDistance = 3f; // The distance from the player to maintain while strafing
    private Vector3 strafeDirection; // The direction to strafe in

    float lerpAddition;
    

    void Start()
    {
        // Calculate the initial strafe direction
        strafeDirection = transform.right;
        player = GameObject.FindGameObjectWithTag("MainCamera").transform;
    }

    void Update()
    { 
        transform.LookAt(-player.position);

        //transform.RotateAround(player.position, Vector3.up, 20 * Time.deltaTime);
        /*Vector3 targetDir = player.position - transform.position;
        Vector3 strafeDir = Vector3.Cross(targetDir, transform.up);

        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(strafeDir), Time.deltaTime * lerpAddition);
        lerpAddition += strafeSpeed;*/
    }
}
