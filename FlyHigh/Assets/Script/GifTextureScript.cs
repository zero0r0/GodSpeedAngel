using UnityEngine;
using System.Collections;

public class GifTextureScript : MonoBehaviour {

    [SerializeField]
    private Sprite[] sprite;
    private SpriteRenderer sprite_renderer;    
    private int frame_num;

    [SerializeField]
    private float change_time;
    private float time;
    [SerializeField]
    private SpeedManeger speed_maneger;
   
	// Use this for initialization
	void Start () {
        time = 0;
        frame_num = 0;
        sprite_renderer = gameObject.GetComponent<SpriteRenderer>();
	}
	
    /// <summary>
    /// プレイヤーの羽ばたきのアニメーション関数
    /// </summary>
	public void FlapAnim() {
        if(this.gameObject.tag == "Player")
            time += Time.deltaTime * speed_maneger.GetNowSpeed();
        else
            time += Time.deltaTime;

        if (change_time < time) {
            time = 0;
            frame_num++;
            if (frame_num >= sprite.Length)
                frame_num = 0;
        }
        sprite_renderer.sprite = sprite[frame_num];
	}

    IEnumerator SpeedUp() {
        change_time /= 3;
        yield return new WaitForSeconds(1f);
        change_time *= 3;
    }

    void OnTriggerEnter2D(Collider2D other) {
        if (other.gameObject.tag == "SpeedUp") {
                StartCoroutine("SpeedUp");            
        }
    }
}
