using UnityEngine;
using System.Collections;

public class BackGroundManager : MonoBehaviour {

    //背景の動くスピード
    public float speed;
    private float add_speed;

    private bool is_active;

    [SerializeField]
    private GameObject[] back_grounds;
    [SerializeField]
    private int wave;

    // Use this for initialization
    void Start()
    {
        is_active = false;
        add_speed = speed / 10f;
        wave = 0;
    }


    // Update is called once per frame
    void Update()
    {
        if (is_active)
        {
            back_grounds[wave].transform.Translate(0, -0.1f * speed * Time.deltaTime, 0);
            if (wave == back_grounds.Length - 1 && back_grounds[wave].transform.position.y <= 0)
                back_grounds[wave].transform.position = new Vector3(0,0,0);
        }
    }

    public void SetIsActive(bool active)
    {
        is_active = active;
    }

    //アイテム習得時の加速
    public void SpeedUp()
    {
        speed += add_speed;
    }

    public void SetWave(int w)
    {
        if(w < back_grounds.Length)
            wave = w;
    }
}
