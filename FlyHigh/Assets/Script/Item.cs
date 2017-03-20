using UnityEngine;
using System.Collections;

public class Item : MonoBehaviour {

    private SpeedManeger speed_maneger;
    private float speed;

    [SerializeField]
    private Vector3 pos;

    void Start()
    {
        speed_maneger = GameObject.FindGameObjectWithTag("SpeedManager").GetComponent<SpeedManeger>();
        speed = speed_maneger.GetNowSpeed();
    }

	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(0, -1*speed ,0)*Time.deltaTime;
        if (this.transform.position.y <= -20f)
        {
            this.transform.position = new Vector3(-100f,-100f,0);
            Destroy(this.gameObject);
        }
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            Destroy(this.gameObject);
        }
    }

    /// <summary>
    /// スピードが増加したときの関数
    /// </summary>
    /// <param name="add">加えるすぴーど</param>
    public void AddSpeed(float add)
    {
        speed += add;
    }

}
