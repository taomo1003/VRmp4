using UnityEngine;
using System.Collections;

public class OnHit : MonoBehaviour {
    private WaitForSeconds disappearDuration = new WaitForSeconds(5.0f);
    public GameObject childObj;
    public AudioSource gunAudio;

    void Start()
    {
        gunAudio = GetComponent<AudioSource>();
    }
    void Damage(float damageAmout) {
        Debug.Log("onHit");
        if (GetComponent<Renderer>().enabled)
        {
            StartCoroutine(ShotEffect());
        }
    }

    void OnTriggerEnter(Collider other) {
        if (GetComponent<Renderer>().enabled)
        {
            StartCoroutine(ShotEffect());
        }
    }

    private IEnumerator ShotEffect()
    {
        gunAudio.PlayOneShot(gunAudio.clip, 0.7f);
        PlaneController.addScore();
        childObj.SetActive(true);
        GetComponent<Renderer>().enabled = false;
        yield return disappearDuration;
        Debug.Log("finished");
        childObj.SetActive(false);
        GetComponent<Renderer>().enabled = true;
    }

}
