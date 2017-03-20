using UnityEngine;
using System.Collections;

public class EnemyManeger : MonoBehaviour {

    [SerializeField]
    private GameObject[] enemy_obj;
    
    //ポジションyは完全に固定
    [SerializeField]
    private float y;
    [SerializeField]
    private float[] x;
    [SerializeField]
    private float width;

    [SerializeField]
    private int max_randdomrange;
    
    [SerializeField]
    private float wait_time;

    private int stage;

    [SerializeField]
    private SpeedManeger speed_maneger;
	
	/// <summary>
	/// 一定時間におき、敵を出現させる
	/// </summary>
	/// <returns>
    /// 秒数
    /// </returns>
    IEnumerator DeployEnemy()
    {
        for (int i = 0; i < x.Length-1; i++)
        {
            int rand = Random.Range(0, max_randdomrange);
            if (rand == 0)
            {
                GameObject obj = Instantiate(enemy_obj[0]) as GameObject;
                obj.transform.position = new Vector3(Random.Range(x[i]+width , x[i+1]-width), y , 0);
            }else if(rand == 1)
            {
                GameObject obj;
                if (stage >= 2) 
                    obj = Instantiate(enemy_obj[1]) as GameObject;
                else
                    obj = Instantiate(enemy_obj[0]) as GameObject;
                obj.transform.position = new Vector3(Random.Range(x[i] + width, x[i + 1] - width), y, 0);
            }
        }

        yield return new WaitForSeconds(wait_time);
        StartCoroutine("DeployEnemy");
    }

    public void StopDeploy(){
        StopCoroutine("DeployEnemy");
    }

    public void StartDeploy() {
        StartCoroutine("DeployEnemy");
    }

    public void SetWaitTime(float time){
        wait_time -= time;
    }

    public void SetStage(int n){
        stage = n;
    }

    public void SetRandomRange(int n){
        max_randdomrange -= n;
    }

}
