using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SmoothBlinker : MonoBehaviour {

    Text text;
    float a = 0, x = 0;

    public float speed;

	// Use this for initialization
	void Start () {
        text = this.gameObject.GetComponent<Text>();
	}
	
	// Update is called once per frame
	void Update () {
        x += speed * Time.deltaTime;
        a = (Mathf.Sin(x) + 1f) / 2;
        this.text.color = new Color(255, 255, 255, a);
	}

}
