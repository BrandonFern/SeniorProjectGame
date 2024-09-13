using System.Threading;
using UnityEditor;
using UnityEngine;

public class EnemyDetection : MonoBehaviour
{
    public string playerTag = "Player";
    public float raycastLength =6.5f;
    public float raycastAngle = 16f; // Angle
    //private bool D = false;
    public float EraycastAngle = 28f;
    public float EraycastLength = 12f;
    public float NraycastLength =6.5f;
    public float NraycastAngle = 16f; 

    public bool chasing = false;
    private float timeLeft = 2.5f;
    public float enemytime = 2.5f;

    //public int numAdditionalRaycasts = 8;    

    void Update()
    {
        DetectPlayer();
        if (chasing){
            Countdown();
        }
        else{
            timeLeft = enemytime;
        }
    }

void DetectPlayer()
{
    Vector3 forwardDirection = transform.forward;
    Vector3 leftDirection = Quaternion.AngleAxis(-raycastAngle, transform.up) * forwardDirection;
    Vector3 rightDirection = Quaternion.AngleAxis(raycastAngle, transform.up) * forwardDirection;

    // Additional ray directions
    Vector3 leftForwardDirection = Quaternion.AngleAxis(-raycastAngle / 2f, transform.up) * forwardDirection;
    Vector3 rightForwardDirection = Quaternion.AngleAxis(raycastAngle / 2f, transform.up) * forwardDirection;
    Vector3 leftBackDirection = Quaternion.AngleAxis(-raycastAngle * 1.5f, transform.up) * forwardDirection;
    Vector3 rightBackDirection = Quaternion.AngleAxis(raycastAngle * 1.5f, transform.up) * forwardDirection;

    // Cast rays for the original directions
    RaycastAndDetect(leftDirection);
    RaycastAndDetect(forwardDirection);
    RaycastAndDetect(rightDirection);

    // Cast rays for additional directions
    RaycastAndDetect(leftForwardDirection);
    RaycastAndDetect(rightForwardDirection);
    RaycastAndDetect(leftBackDirection);
    RaycastAndDetect(rightBackDirection);
}



    void RaycastAndDetect(Vector3 direction)
    {
        RaycastHit hit;

        if (Physics.Raycast(transform.position, direction, out hit, raycastLength) && !chasing)
        {
            
            if (hit.collider.CompareTag(playerTag) && hit.collider.CompareTag("Wall"))
            {
                //transform.GetComponentInChildren<Cone>().SetNormalMaterial();

                raycastAngle = NraycastAngle;
                raycastLength = NraycastLength;
                
                transform.GetComponent<EnemyPatrol>().NDETECTED();
            }
            
            else if(hit.collider.CompareTag(playerTag)){
                Debug.Log("Player detected!");
                //SoundFXManager.instance.PlaySoundFXClip(alert, transform, 1f);
                
                //transform.GetComponentInChildren<Cone>().SetAlertMaterial();
                raycastLength = EraycastLength;
                raycastAngle = EraycastAngle;
                transform.GetComponent<EnemyPatrol>().DETECTED();
                Countdown();
                //D = true;
                //DETECTED(d);
            }
            
            else if(!hit.collider.CompareTag(playerTag))
            {
                //transform.GetComponentInChildren<Cone>().SetNormalMaterial();

                raycastAngle = NraycastAngle;
                raycastLength = NraycastLength;

                
                transform.GetComponent<EnemyPatrol>().NDETECTED();
                //D = false;
                //DETECTED(d);
                
            }
        }

        Debug.DrawRay(transform.position, direction * raycastLength, Color.green);
    }

    private bool Countdown(){

    timeLeft -= Time.deltaTime;

    if (timeLeft > 0)
    {
        chasing = true;
        //transform.GetComponentInChildren<Cone>().SetAlertMaterial();
        raycastLength = EraycastLength;
        raycastAngle = EraycastAngle;
        transform.GetComponent<EnemyPatrol>().DETECTED();
        Debug.Log("Countdown: " + timeLeft); // Debug log the countdown value
        return true; // Indicate that countdown is still ongoing
    }
    else
    {
        chasing = false;
        Debug.Log("Countdown finished!");
        return false; // Indicate that countdown has finished
    }

    }



    
}