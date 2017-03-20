using UnityEngine;
using System.Collections;

public class ScrollObject : MonoBehaviour {

  //  public float start_speed;
    public float speed;
    public float start_position;
    public float end_position;

   // public float closenees;
    private float max_x;
    private float min_x;
    private bool is_active;

	// Use this for initialization
	void Start () {
        //speed = start_speed;
        is_active = false;
        max_x = 3f;
        min_x = -3f;
	}
	
	// Update is called once per frame
	void Update () {
        if(is_active){
            transform.Translate(0,-speed * Time.deltaTime, 0);
            if (transform.position.y <= end_position) 
                ScrollEnd();
        }
	}

    void ScrollEnd()
    {
        float rand = Random.Range(min_x,max_x);
        transform.Translate(rand,-1 * (end_position - start_position), 0);

//        SendMessage("OnScrollEnd", SendMessageOptions.DontRequireReceiver);
    }

    public void SetIsActive(bool active) {
        is_active = active;
    }

}
