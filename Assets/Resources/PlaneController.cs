using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class PlaneController : MonoBehaviour {
    private WaitForSeconds disappearDuration = new WaitForSeconds(3.0f);
    public float speed = 30f;
    private static int currScore = 0;
    public GameObject cam;
    private Quaternion originDir;
    public Text countText;
    public Text winText;
	// Use this for initialization

    private static string curr_time;
    private static float beat = 0.0f;
    public GameObject childObj;
    private float oriTime;
    public AudioClip crash;
    private AudioSource audio;
    void Start () {
		Debug.Log ("Plane Controller Attached to:" + gameObject.name);
        originDir = transform.rotation;
        winText.text = "";
        curr_time = "";
        oriTime = Time.time;
        audio = GetComponent<AudioSource>();
    }
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButton("Jump")) {
            transform.position = new Vector3(-114.93f, 10.0f, -266.23f);
            speed = 3.0f;
            transform.rotation = originDir;
            currScore = 0;
            oriTime = Time.time;

        }
		transform.position += transform.forward * Time.deltaTime * speed;

		Vector3 moveCameraTo = transform.position - transform.forward * 5.0f + Vector3.up * 5.0f;

		float smoothFac = 0.95f;

		cam.transform.position = smoothFac * cam.transform.position +
		moveCameraTo * (1 - smoothFac);
		cam.transform.LookAt (transform.position + transform.forward * 30.0f);

		speed -= transform.forward.y * Time.deltaTime * 2.0f;

        if (Input.GetButton("Fire2")) {
            if (transform.position.y <= 11) speed += 0.03f;
            else speed += 0.3f;
            Debug.Log(speed);
        }

        if (Input.GetButton("Fire3"))
        {
            speed -= 0.2f;
            Debug.Log(speed);
        }

        if (speed < 5.0f && transform.position.y >=11) {
			speed = 5.0f;
		}


        if (speed < 0.0f)
        {
            speed = 0.0f;
        }

        if (speed > 100.0f)
        {
            speed = 100.0f;
        }
        Debug.Log(speed);
        transform.Rotate (-0.8f*Input.GetAxis ("Vertical"), 0.8f * Input.GetAxis("Horizontal"), 0.0f);

        //go back to 0.0 at z axis
        Vector3 backToZero = new Vector3(transform.rotation.eulerAngles.x, transform.rotation.eulerAngles.y, 0);
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.Euler( backToZero), Time.deltaTime * 3.0f);

        float terrainHeight = Terrain.activeTerrain.SampleHeight (transform.position);


        //hit the ground
		if (terrainHeight-1 > transform.position.y) {
            //transform.position = new Vector3 (transform.position.x,
            //								terrainHeight,
            //								transform.position.z);
            StartCoroutine(CrashEffect());
        }

        if (currScore >= 10) {
            if (curr_time.Length == 0) {
                float time = Time.time;
                curr_time = Mathf.Round(time-oriTime).ToString();
                beat = (100.0f - time) / 80.0f;
                beat = Mathf.Round( beat * 100 ) ;
                if (beat < 0) beat = 0;
                if (beat > 100) beat = 99;
            }
            winText.text = "You Win!" +"You Spent:" + curr_time + " Seconds " + " Beat  " + beat.ToString() + "% Players" ;
            countText.text = "";

        } else {
            winText.text = "";
            countText.text = "Score:" + currScore.ToString();
        }
    }

    public static void addScore()
    {
        currScore++;
    }

    private IEnumerator CrashEffect()
    {
        audio.PlayOneShot(crash, 0.3f);
        childObj.SetActive(true);
        
        speed = 0.0f;
        yield return disappearDuration;
        //Debug.Log("finished");
        childObj.SetActive(false);
        transform.position = new Vector3(-114.93f, 10.0f, -266.23f);
        speed = 3.0f;
        transform.rotation = originDir;
        currScore = 0;
        oriTime = Time.time;
    }

}
