using UnityEngine;
using System.Collections;

public class grenadeLauncher : MonoBehaviour
{

    public GameObject grenade;
    public GameObject throwArm;
    public GameObject throwArmMax;

    public float maxForce = 1f;
    public float rateForce = 1.3f;
    public float originalForce = 100f;

    private float curForce = 1f;
    private Vector3 throwForce;
    private bool preppingThrow = false;

    private GameObject tempGrenade;
    private Vector3 tempForce;
    private bool fire1ButtonUpChange = false;

    // Use this for initialization
    void Start()
    {
        curForce = originalForce;
    }

    // Update is called once per frame
    void OnPreCull()
    {

        if (Input.GetButton("Fire1"))
        { 
            if (!preppingThrow)
            {
                preppingThrow = true;
                GameObject newGrenade = (GameObject)Instantiate(grenade, throwArm.transform.position, transform.rotation);
                tempGrenade = newGrenade;
                tempGrenade.GetComponent<Rigidbody>().isKinematic = true;
                tempGrenade.transform.parent = Camera.main.transform;
                StartCoroutine(PrepGrenade());
            }

        }

        if (fire1ButtonUpChange)
        {
            StopAllCoroutines();
            preppingThrow = false;
            tempForce = Camera.main.transform.forward; // get camera.main.transform.forward at prerender time
            SpawnGrenade();
            fire1ButtonUpChange = false;
        }
    }

    void Update()
    {
        if (Input.GetButtonUp("Fire1"))
        {
            fire1ButtonUpChange = true; // flag to push GetButtonUp to prerender stage
        }
    }

    IEnumerator PrepGrenade()
    {
        while (preppingThrow)
        {
            // Add force to private force variable
            if (curForce < maxForce)
            {
                curForce = curForce * rateForce;
                PosGrenadeTransform();
                // print(curForce);
            }
            else
            {
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    void PosGrenadeTransform()
    {
        // Get distance between two Vectors (throwArm and throwArmMax)
        float percent = curForce / maxForce;
        Vector3 dist = Vector3.Lerp(throwArm.transform.position, throwArmMax.transform.position, percent);

        // Set transform of tempGrenade as a percentage between two vectors.
        tempGrenade.transform.position = dist;
    }

    void SpawnGrenade()
    {
        // GameObject newGrenade = (GameObject)Instantiate(grenade, throwArm.transform.position, Quaternion.identity);
       
        tempGrenade.GetComponent<Rigidbody>().isKinematic = false;
        throwForce = tempForce * curForce;
        tempGrenade.GetComponent<Rigidbody>().AddForce(throwForce);
        tempGrenade.transform.parent = null;
        curForce = originalForce; // reset curForce
    }

    
}

