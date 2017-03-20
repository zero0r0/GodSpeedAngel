using UnityEngine;
using System.Collections;

public class BackGound : MonoBehaviour {

    //背景の動くスピード
    public float speed;
    private float add_speed;

    //背景切り替えの最大速度
    [SerializeField]
    private float change_speed;
    private float acceleration;

    private bool is_active;

	// Use this for initialization
	void Start () {
        is_active = false;
        acceleration = 1f;
        add_speed = speed / 10f;
	}
	
	// Update is called once per frame
	void Update () {
        if (is_active) {
            this.transform.Translate(0, -0.1f * speed * acceleration * Time.deltaTime, 0);
            
        }
	}
    
    public void SetIsActive(bool active) {
        is_active = active;
    }

    //アイテム習得時の加速
    public void SpeedUp() {
        speed += add_speed;
    }
}