using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class Choose : MonoBehaviour {
    public GameObject arrow;
    // Use this for initialization
    public Button Button0;
    public Button Button1;
    public Button Button2;


    private float[] positions;
    private int position;
    private Button[] Buttons;
	void Start () {
        positions = new float[3];
        positions[0] = 112.0f;
        positions[1] = 53.0f;
        positions[2] = -10.0f;
        position = 0;
        Buttons = new Button[3];
        Buttons[0] = Button0;
        Buttons[1] = Button1;
        Buttons[2] = Button2;

    }
	
	// Update is called once per frame
	void Update () {       
        if (Input.GetButtonDown("Fire1")) {
            position++;
            position = position % 3;
            arrow.transform.localPosition = new Vector3(arrow.transform.localPosition.x, positions[position], 0.0f);
        }

        if (Input.GetButtonDown("Fire2"))
        {
            Buttons[position].onClick.Invoke();
        }
    }

}
