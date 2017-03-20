using UnityEngine;
using System.Collections;
 
public class Blinker : MonoBehaviour {
	private float nextTime;
	public float interval = 1.0f;	// 点滅周期

    public GameObject parent_obj;
    public float pos_y;

    Renderer render;

	// Use this for initialization
	void Start() {
		nextTime = Time.time;
        parent_obj = this.gameObject.transform.parent.gameObject;
        render = this.GetComponent<Renderer>();
	}
	
	// Update is called once per frame
	void Update() {
        if (parent_obj.transform.position.y < pos_y)
        {
            this.gameObject.transform.position = new Vector3(-100f,-100f,0);
        }

		if ( Time.time > nextTime ) {
			render.enabled = !render.enabled;

			nextTime += interval;
		}
	}

}
