using UnityEngine;
using System.Collections;

public class ItemManeger : MonoBehaviour {

    //アイテムの出現する間隔の秒数
    [SerializeField]
    private float max_time;
    [SerializeField]
    private float min_time;


    //アイテムの出現する場所
    [SerializeField]
    private Vector3 max_pos;
    [SerializeField]
    private Vector3 min_pos;

    //アイテムの種類
    [SerializeField]
    private GameObject heal;
    [SerializeField]
    private GameObject speed_up;
    [SerializeField]
    private SpeedManeger speed_maneger;

    /// <summary>
    /// アイテムを一定周期に出現させる
    /// </summary>
    /// <returns></returns>
    IEnumerator DeployItem()
    {
        var time = Random.Range(min_time, max_time);

        DeployHeal();

        yield return new WaitForSeconds(time);

        time = Random.Range(min_time, max_time);

        DeploySpeedUp();

        yield return new WaitForSeconds(time);

        StartCoroutine("DeployItem");

    }

    /// <summary>
    /// ヒールアイテムの出現
    /// </summary>
    void DeployHeal(){
        var x = Random.Range(min_pos.x, max_pos.x);
        var y = Random.Range(min_pos.y, max_pos.y);
        var obj = Instantiate(heal) as GameObject;
        obj.transform.position = new Vector3(x, y, 0);
    }

    /// <summary>
    /// スピードアップのアイテムの出現
    /// </summary>
    void DeploySpeedUp()
    {
        var x = Random.Range(min_pos.x, max_pos.x);
        var y = Random.Range(min_pos.y, max_pos.y);
        var obj = Instantiate(speed_up) as GameObject;
        obj.transform.position = new Vector3(x, y, 0);
    }

    /// <summary>
    /// アイテム出現のストップ
    /// </summary>
    public void StopDeploy() {
        StopCoroutine("DeployItem");
    }

    /// <summary>
    /// アイテムの出現を再開
    /// </summary>
    public void StartDeploy() {
        StartCoroutine("DeployItem");
    }

}
