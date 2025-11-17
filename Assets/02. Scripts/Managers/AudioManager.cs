using System.Collections.Generic;
using UnityEngine;
public enum AudioType
{
    BGM,
    SFX
}

public class AudioManager : MonoBehaviour
{
    private static AudioManager s_Instance;
    public static AudioManager Instance => s_Instance;

    private AudioSource _bgm;
    private List<AudioSource> _sfxList = new List<AudioSource>();

    private void Awake()
    {
        if (s_Instance == null)
        {
            s_Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        if (_bgm == null)
        {
            _bgm = gameObject.AddComponent<AudioSource>();
            _bgm.loop = true;
        }

        if (_sfxList.Count == 0)
        {
            _sfxList.Add(gameObject.AddComponent<AudioSource>());
        }
    }

    private void Start()
    {
        PlaySound("BGM", AudioType.BGM);
    }

    public void PlaySound(string fileName, AudioType audioType)
    {
        string path = "05. Sounds";
        fileName = $"{path}/{fileName}";

        if (audioType == AudioType.BGM)
        {
            _bgm.clip = Resources.Load<AudioClip>(fileName);
            _bgm.Play();
        }

        else
        {
            for (int i = 0; i < _sfxList.Count; i++)
            {
                if (_sfxList[i].isPlaying)
                {
                    continue;
                }
                else
                {
                    _sfxList[i].clip = Resources.Load<AudioClip>(fileName);
                    _sfxList[i].Play();
                    return;
                }
            }

            _sfxList.Add(gameObject.AddComponent<AudioSource>());
            _sfxList[_sfxList.Count - 1].clip = Resources.Load<AudioClip>(fileName);
            _sfxList[_sfxList.Count - 1].Play();
        }
    }
}