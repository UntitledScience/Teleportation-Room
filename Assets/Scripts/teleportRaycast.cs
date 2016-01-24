using UnityEngine;
using System.Collections;

public class teleportRaycast : MonoBehaviour {

    public float charRadius = 0.75f;
    public GameObject teleportReticle;
    public GameObject teleportReticleRed;
    public GameObject rightArm;
    public float teleportMaxDist = 6f;
    public GameObject PlayerGO;
    public Vector3 resetPosition = new Vector3(50, 50, 50);

    private bool canTeleport = false;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {

        // position teleportReticle where you desire to be teleported
        if (Input.GetButton("Fire2"))
        {
            PositionReticle();
        }

        if (Input.GetButtonUp("Fire2"))
        {
            TeleportToPoint();
        }
	
	}

    void PositionReticle()
    {
		// Vector3 fwd = Camera.main.transform.TransformDirection(transform.forward);
        RaycastHit hit;

        // if raycast hit, position reticle at raycast
		if (Physics.Raycast(rightArm.transform.position, Camera.main.transform.forward, out hit, teleportMaxDist))
        {
			Debug.DrawLine(Camera.main.transform.position, hit.point, Color.red);
            Vector3 dir = hit.normal;
            teleportReticle.transform.position = hit.point + (dir * charRadius);
            teleportReticleRed.transform.position = resetPosition;
            canTeleport = true;

        } else {
            // if not, position reticle at teleportMaxDist away
           // Vector3 dir = (Camera.main.transform.position - fwd).normalized;
           // print(dir);
			teleportReticleRed.transform.position = rightArm.transform.position + (Camera.main.transform.forward.normalized * teleportMaxDist);
            teleportReticle.transform.position = resetPosition;
            canTeleport = false;
        }

    }

    void TeleportToPoint()
    {
        if (canTeleport)
        {
            PlayerGO.transform.position = teleportReticle.transform.position;
            teleportReticle.transform.position = new Vector3(50f, 50f, 50f);
        } else
        {
            teleportReticle.transform.position = resetPosition;
            teleportReticleRed.transform.position = resetPosition;
        }

    }
}
