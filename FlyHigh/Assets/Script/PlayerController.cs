using UnityEngine;
using System.Collections;
using UnityEngine.UI;


public class PlayerController : MonoBehaviour
{

    [SerializeField]
    private float speed_rate;

    //ＨＰ関連
    [SerializeField]
    private float HP;
    [SerializeField]
    private float damage;
    [SerializeField]
    private float recovery_amount;
    [SerializeField]
    private Image HP_image;
    [SerializeField]
    private float HP_decrease;
    //ダメージ比
    [SerializeField]
    private float ratio;
    private Vector3 max_hp;
    public bool is_alive {  //プレイヤーの生存
        get;
        private set;
    }
    //プレイヤーが活動可能か
    public bool is_active {
        get;
        private set;
    }

    //ダメージ時の硬直時間
    [SerializeField]
    private float rigor_time;

    private bool is_rigor;

    //プレイヤーに関連するUI
    //    [SerializeField]
    //    private Image image;
    [SerializeField]
    private Sprite[] expression_sprite;
    [SerializeField]
    private Image expression_image;

    //ポジション制御関連の変数
    [SerializeField]
    private float max_x;
    [SerializeField]
    private float min_x;
    [SerializeField]
    private Vector3 velocity;
    [SerializeField]
    private float speed;

    //マネージャー
    [SerializeField]
    private ScoreManeger score_maneger;
    [SerializeField]
    private SpeedManeger speed_maneger;

    //スマホ用
    private Vector3 screenPos;
    private Vector3 worldPos;

    //AudioClip
    public AudioClip[] speedup_se;
    public AudioClip heal_se;
    public AudioClip damage_se;
   
 
    private SpriteRenderer sp_renderer;
    private bool is_flash;
    [SerializeField]
    private float flash_time;
    private float flash_timer;

    //エフェクト
    [SerializeField]
    private GameObject heal_effect;
    [SerializeField]
    private GameObject damage_effect;
    [SerializeField]
    private GameObject speedup_effect;
    [SerializeField]
    private GameObject line;

    //アニメーション
    [SerializeField]
    private GifTextureScript gif_script;
    [SerializeField]
    private float anim_speed;

    public void Init() {
        is_alive = true;
        is_rigor = true;
        is_flash = false;
        flash_timer = 0;
        HP = max_hp.x;
        expression_image.sprite = expression_sprite[0];
    }

	// Use this for initialization
	void Start () 
    {
        max_hp = HP_image.transform.localScale;
        sp_renderer = gameObject.GetComponent<SpriteRenderer>();
        Init();
	}
	
	// Update is called once per frame
	void Update () 
    {
        if (is_alive) {
            if(is_rigor)
                Move();
            if (is_active) {
                gif_script.FlapAnim();
                //HP制御
                HP_image.transform.localScale = new Vector3(HP, HP_image.transform.localScale.y, HP_image.transform.localScale.y);
                HP -=  Time.deltaTime * ratio * HP_decrease;
                if (HP > max_hp.x) {
                    HP_image.transform.localScale = max_hp;
                }
                if (HP <= 0) {
                    HP = 0;
                    HP_image.transform.localScale = new Vector3(HP, HP_image.transform.localScale.y, HP_image.transform.localScale.y);               
                    sp_renderer.color = new Color(1f, 1f, 1f, 0.25f);
                    is_alive = false;
                }

                if (is_flash) {
                    Flash();
                }
            }
        }
	}

    void Flash() {
        if (flash_timer < flash_time) {
            flash_timer += Time.deltaTime;
            float level = Mathf.Abs(Mathf.Sin(Time.time * 10));
            sp_renderer.color= new Color(1f, 1f, 1f, level);
        } else {
            sp_renderer.color = new Color(1f, 1f, 1f, 1f);
            is_flash = false;
            flash_timer = 0;
        }
    }

    /// <summary>
    /// キャラクターのキー入力
    /// ポジションの範囲の指定
    /// </summary>
    void Move()
    {
        //タップ入力
        screenPos = Input.mousePosition;
        worldPos = Camera.main.ScreenToWorldPoint(screenPos);
        if ((Input.GetKey(KeyCode.RightArrow)) || (Input.GetMouseButton(0) && worldPos.x > 0))
            this.transform.position += velocity * Time.deltaTime;
        if ((Input.GetKey(KeyCode.LeftArrow)) || (Input.GetMouseButton(0) && worldPos.x <= 0))
            this.transform.position -= velocity * Time.deltaTime;

        float y = this.transform.position.y;
        if (this.transform.position.x > max_x)
            this.transform.position = new Vector3(max_x, y, 0);
        if (this.transform.position.x < min_x)
            this.transform.position = new Vector3(min_x, y, 0);
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        if (is_alive) {
            if (other.gameObject.tag == "Enemy"){
                Damage();
            }

            if (other.gameObject.tag == "Heal") {
                Heal();
                DataManager.instance.AddGetHealNum();
            }

            if (other.gameObject.tag == "SpeedUp") {
                SpeedUp();
            }
            
            Destroy(other.gameObject);
        }
    }

    /// <summary>
    /// スピードアップマネージャーにスピードを上げるのを指示
    /// スピードアップのＳＥとエフェクトの再生
    /// </summary>
    void SpeedUp() {
        heal_effect.SetActive(false);
        damage_effect.SetActive(false);
        line.SetActive(true);
        speedup_effect.SetActive(true);
        speed_maneger.SpeedUp();
        SoundManeger.instance.SoundSE(speedup_se[Random.Range(0,speedup_se.Length-1)]);
        velocity.x += speed_rate;
    }

    /// <summary>
    /// ダメージ関数
    /// ＨＰ減少とＵＩのスプライトの変化
    /// </summary>
    void Damage() {
        SoundManeger.instance.SoundSE(damage_se);
        HP -= damage * ratio;
        expression_image.sprite = expression_sprite[1];
        heal_effect.SetActive(false);
        speedup_effect.SetActive(false);
        damage_effect.SetActive(true);
        is_flash = true;
        StartCoroutine("ReturnExpression");
        StartCoroutine("Rigor");
    }

    /// <summary>
    /// 回復関数
    /// </summary>
    void Heal(){
        if (HP < max_hp.x)
            HP += recovery_amount * ratio;
        if (HP > max_hp.x)
            HP = max_hp.x;
        SoundManeger.instance.SoundSE(heal_se);
        damage_effect.SetActive(false);
        speedup_effect.SetActive(false);
        heal_effect.SetActive(true);
    }

    /// <summary>
    /// 表情を元に戻す
    /// </summary>
    /// <returns></returns>
    IEnumerator ReturnExpression() {
        yield return new WaitForSeconds(1f);
        expression_image.sprite = expression_sprite[0];
    }

    /// <summary>
    /// 硬直
    /// 指定秒後解除
    /// </summary>
    /// <returns></returns>
    IEnumerator Rigor(){
        is_rigor = false;
        yield return new WaitForSeconds(rigor_time);
        is_rigor = true;
    }
    

    public void SetIsActive(bool active) {
        is_active = active;
    }

    /// <summary>
    /// ゲームオーバー時プレイヤーが画面外へ出るアニメーション関数
    /// </summary>
    public bool Falling() {
        if (this.transform.position.y >= -5f) {
            this.transform.Translate(0, -anim_speed * Time.deltaTime, 0);
            expression_image.sprite = expression_sprite[1];
            return false;
        } else {
            return true;
        }
    }

    /// <summary>
    /// リトライ時にプレイヤーが戻ってくるアニメーション関数 
    /// </summary>
    /// <returns>
    /// 戻り終えたかどうかを返す
    /// </returns>
    public bool Rising() {
        if (this.transform.position.y <= 0) {
            this.transform.Translate(0, anim_speed * Time.deltaTime, 0);
            return false;
        } else {
            expression_image.sprite = expression_sprite[0];
            sp_renderer.color = new Color(1f, 1f, 1f, 1f);
            return true;
        }
    }
}