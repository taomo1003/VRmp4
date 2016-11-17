using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class OptionChoose : MonoBehaviour {
    public GameObject arrow;
    // Use this for initialization
    public Button Button0;
    // Use this for initialization
    void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
        if (Input.GetButtonDown("Fire2"))
        {
            Button0.onClick.Invoke();
        }
    }
}
