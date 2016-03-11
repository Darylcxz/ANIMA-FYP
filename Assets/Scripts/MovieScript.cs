using UnityEngine;
using UnityEngine.UI;
using System.Collections;


public class MovieScript : MonoBehaviour {
    [SerializeField] MovieTexture movie1;
    [SerializeField] MovieTexture movie2;
    RawImage thisimage;
    AudioSource sound;
    [SerializeField]AudioClip cutscene1;
    [SerializeField]AudioClip cutscene2;

    bool playing;
    bool firstone = true;
    float timer;
    float duration;
    VillageDialogue dialoguescript;
    AudioSource bgm;
    

	// Use this for initialization
	void Start () {

        thisimage = GetComponent<RawImage>();
        sound = GetComponent<AudioSource>();
        dialoguescript = GameObject.Find("Character").transform.GetChild(1).GetComponent<VillageDialogue>();
        bgm = GameObject.Find("BGM").GetComponent<AudioSource>();
    }

    void Update()
    {
        if(playing)
        {
            timer += Time.deltaTime;
            if(timer > duration || GamepadManager.buttonStartDown)
            {
                thisimage.enabled = false;
                playing = false;
                sound.Stop();
                if(firstone)
                {
                    dialoguescript.StartDialogue();
                    firstone = false;
                }
            }
        }
    }

    public void PlayCutscene1()
    {
        thisimage.texture = movie1 as MovieTexture;
        sound.clip = cutscene1;
        movie1.Play();
        sound.Play();
        timer = 0;
        duration = movie1.duration;
        playing = true;
    }

    public void PlayCutscene2()
    {
        thisimage.enabled = true;
        thisimage.texture = movie2 as MovieTexture;
        sound.clip = cutscene2;
        movie2.Play();
        sound.Play();
        timer = 0;
        duration = movie2.duration;
        playing = true;
        bgm.Stop();
    }
}
