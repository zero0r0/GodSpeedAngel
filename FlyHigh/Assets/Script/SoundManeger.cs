using UnityEngine;
using System.Collections;

public class SoundManeger : MonoBehaviour {

    [SerializeField]
    private AudioSource se_source;
    [SerializeField]
    private AudioSource bgm_source;
    public static SoundManeger instance = null;

	// Use this for initialization
	void Awake () {
        if (instance == null)
			instance = this;
		else if (instance != this)
			Destroy (gameObject);

        DontDestroyOnLoad(this.gameObject);
	}

    /// <summary>
    ///　BGMの再生
    /// </summary>
    /// <param name="clip">
    /// AudioClip
    /// </param>
    public void SoundSE(AudioClip clip) {
        se_source.clip = clip;
        se_source.Play();
    }

    /// <summary>
    /// BGMの再生
    /// </summary>
    /// <param name="clip">
    /// AudioClip
    /// </param>
    public void SoundBGM(AudioClip clip) {
        bgm_source.clip = clip;
        bgm_source.Play();
    }

    /// <summary>
    /// BGMのストップ
    /// </summary>
    public void StopBGM() {
        bgm_source.Stop();
    }

}
