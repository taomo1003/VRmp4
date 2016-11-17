using UnityEngine;
using System.Collections;

public class RayCastShoot : MonoBehaviour {

    public int gunDamage = 1;
    public float fireRate = 0.25f;
    public float weaponRange = 50f;
    public float hitForce = 100f;
   
    private WaitForSeconds shotDuration = new WaitForSeconds(0.7f);
    private LineRenderer laserLine;
    private float nextFire;

    private AudioSource audio;
    // Use this for initialization
    void Start () {
        laserLine = GetComponent<LineRenderer>();
        
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire1") && Time.time > nextFire) {
            nextFire = Time.time + fireRate;

            StartCoroutine(ShotEffect());
        }

	}

    private IEnumerator ShotEffect() {
        audio.PlayOneShot(audio.clip, 0.7f);
        //laserLine.enabled = true;
        GameObject ro  = (GameObject) Instantiate(Resources.Load("bullet_rocket", typeof(GameObject)));
        ro.transform.position = transform.position + transform.forward * 10.0f;
        ro.transform.rotation = transform.rotation;
        yield return shotDuration;
        //laserLine.enabled = false;
    }
}
