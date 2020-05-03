using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioSource : MonoBehaviour
{
    private UnityEngine.AudioSource src;

    private void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("Music");
        if (objs.Length > 1)
            Destroy(this.gameObject);
        DontDestroyOnLoad(this.gameObject);
        src = this.GetComponent<UnityEngine.AudioSource>();
    }

    public void PlayMusic()
    {
        if (src.isPlaying) return;
        src.Play();
    }

    public void StopMusic()
    {
        src.Stop();
    }
}
