using UnityEngine;
using System.Collections;

public class Enemy : MonoBehaviour {

    //敵の基本のスピード
    private float speed;

    //敵によってスピードを少し変えるのでその変化の最大
    [SerializeField]
    private float max_speed;//生の整数で設定

    private SpeedManeger speed_maneger;
    private GifTextureScript gif;

    void Awake()
    {
        speed_maneger = GameObject.FindGameObjectWithTag("SpeedManager").GetComponent<SpeedManeger>();
        gif = GetComponent<GifTextureScript>();
    }

    void Start()
    {
        speed = speed_maneger.GetNowSpeed() - Random.Range(1, max_speed);
    }

	// Update is called once per frame
	void Update () {
        this.transform.position += new Vector3(0, -1f, 0) * speed * Time.deltaTime;
        gif.FlapAnim();
        if (this.transform.position.y <= -20f)
        {
            Destroy(this.gameObject);
        }
	}


    public void AddSpeed(float add)
    {
        speed += add;
    }
}
