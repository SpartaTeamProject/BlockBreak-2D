using System.Collections;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Audio;

public class AudioManager : MonoBehaviour
{
    public static AudioManager instance = null;
    private AudioSource player;
    private Dictionary<string, AudioClip> bgms;
    private Dictionary<string, AudioClip> ses;

    private void Awake()
    {
        if (instance == null)
            instance = this;
        else
            Destroy(gameObject);
        DontDestroyOnLoad(gameObject);

        player = GetComponent<AudioSource>();
        bgms = new Dictionary<string, AudioClip>();
    }

    // Start is called before the first frame update
    void Start()
    {
        GameObject se = new GameObject("sePlayer");
        se.transform.SetParent(this.transform);
        se.AddComponent<AudioSource>();

        DontDestroyOnLoad(se);
    }

    public void PlayBGM(string name, bool loop = true)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + name+".mp3");
        if (bgms.ContainsKey(name))
        {
            this.player.clip = bgms[name];
        }
        else
        {
            bgms.Add(name, clip);
            this.player.clip = clip;
        }
        this.player.loop = loop;
        this.player.Play();

    }

    public void StopBGM()
    {
        this.player.Stop();
    }

    public void PlaySE(string name)
    {
        AudioClip clip = Resources.Load<AudioClip>("Audio/" + name);
        var obj = this.player.transform.GetChild(0).GetComponent<AudioSource>();
        if (ses.ContainsKey(name))
        {
            obj.clip = ses[name];
        }
        else
        {
            ses.Add(name, clip);
            obj.clip = clip;
        }
        obj.Play();
    }

    public void StopSE()
    {
        var obj = this.player.transform.GetChild(0).GetComponent<AudioSource>();
        obj.Stop();
    }
}
