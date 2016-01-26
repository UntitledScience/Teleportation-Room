using UnityEngine;
using System.Collections;

public class grenadeLauncher : MonoBehaviour {

    public GameObject grenade;
    public GameObject throwArm;
    public GameObject throwArmMax;
    public Camera thisCamera;

    public float maxForce = 1f;
    public float rateForce = 1.3f;
    public float originalForce = 100f;

    private float curForce = 1f;
    private Vector3 throwForce;
    private bool preppingThrow = false;
    private bool swiping = false;

    private GameObject tempGrenade;

	// Use this for initialization
	void Start () {
        curForce = originalForce;
	}
	
	// Update is called once per frame
	void LateUpdate () {
	
        if (Input.GetButtonDown("Fire2"))
        {
            PreppingThrow();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            ThrowGrenade();
        }

        if (Input.GetAxis("Mouse X") < -0.3)
        {
            if (!swiping)
            {
                PreppingThrow();
                swiping = true;
            }
            
        }

        if (Input.GetAxis("Mouse X") == 0)
        {
            if (swiping)
            {
                ThrowGrenade();
                swiping = false;
            }
        }

	}

    void PreppingThrow()
    {
        // Input method to prep throwing of grenade
        preppingThrow = true;
        GameObject newGrenade = (GameObject)Instantiate(grenade, throwArm.transform.position, Quaternion.identity);
        tempGrenade = newGrenade;
        tempGrenade.GetComponent<Rigidbody>().isKinematic = true;
        tempGrenade.transform.parent = thisCamera.transform;
        StartCoroutine(PrepGrenade());
    }

    void ThrowGrenade()
    {
        // Throwing the Grenade
        preppingThrow = false;
        SpawnGrenade();
    }

    IEnumerator PrepGrenade()
    {
        while (preppingThrow)
        {
            // Add force to private force variable
            if (curForce < maxForce)
            {
                curForce = curForce * rateForce;
                SetPrepGrenadeTransform();
               // print(curForce);
            } else
            {
                yield break;
            }
            yield return new WaitForSeconds(0.05f);
        }
    }

    void SetPrepGrenadeTransform()
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
        tempGrenade.transform.parent = null;
        tempGrenade.GetComponent<Rigidbody>().isKinematic = false;  
        throwForce = (thisCamera.transform.forward) * curForce;
        tempGrenade.GetComponent<Rigidbody>().AddForce(throwForce);
        curForce = originalForce; // reset curForce
    }
}
