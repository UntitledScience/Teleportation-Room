using UnityEngine;
using System.Collections;

public class grenadeLauncher : MonoBehaviour {

    public GameObject grenade;
    public GameObject throwArm;
    public float xforce = 1f;


    private Vector3 throwForce;

	// Use this for initialization
	void Start () {


	}
	
	// Update is called once per frame
	void Update () {
	
        if (Input.GetButtonDown("Fire1"))
        {
            SpawnGrenade();
        }

	}

    void SpawnGrenade()
    {
        throwForce = (Camera.main.transform.forward) * xforce;
        GameObject newGrenade = (GameObject)Instantiate(grenade, throwArm.transform.position, Quaternion.identity);
        newGrenade.GetComponent<Rigidbody>().AddForce(throwForce);
    }
}
