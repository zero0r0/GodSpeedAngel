using UnityEngine;
using System.Collections;

public class CameraController : MonoBehaviour {


    private GameObject player_obj;
    private Vector3 player_pos;
    private Vector3 camera_pos;
    public Vector3 offset;

	// Use this for initialization
	void Start () {
        player_obj = GameObject.FindGameObjectWithTag("Player");
        camera_pos = this.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
        player_pos = player_obj.transform.position;
        this.transform.position = new Vector3(0, player_pos.y, camera_pos.z) + offset;
	}
}
