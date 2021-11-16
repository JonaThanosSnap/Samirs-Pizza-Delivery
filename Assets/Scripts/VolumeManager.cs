using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VolumeManager : MonoBehaviour
{
    [SerializeField]
    public AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    // TODO adjust to logarithmic scale of volume
    public void SetVolume(float volume) {
        audioManager.volume = Mathf.Clamp(volume, 0.0f, 1.0f);
    }
}
