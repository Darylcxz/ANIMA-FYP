using UnityEngine;
using System.Collections;

public class NewPigScript : NewAIBA {
	[SerializeField]ParticleSystem pigExplosion; //particle system for pig sneeze
	Animator pigAnim;
	[SerializeField]AudioClip sneezeSound;
	[SerializeField]AudioClip squealSound;
	AudioSource pigAudioSource;
	bool hasPlayed;

	protected override void Start()
	{
       // spawnPoint.position = transform.position;
        pigAnim = GetComponent<Animator>();
		pigAudioSource = GetComponent<AudioSource>();
        base.Start();

    }


	protected override void ActivateAbility()
	{
		if(!pigExplosion.isPlaying)
		{
			hasPlayed = false;
			pigExplosion.Play();
			if(!pigAudioSource.isPlaying && !hasPlayed)
			{
				StartCoroutine(PlaySound(sneezeSound));
			}
			StartCoroutine(PlaySound(sneezeSound));
			pigAnim.SetTrigger("tSneeze");
		}
	}
	protected override void PassiveAbility()
	{
		//Plays pig anims
		switch(AIState)
		{
			case StateMachine.WALK:
				hasPlayed = false;
				pigAnim.SetBool("isMoving", true);
				break;
			case StateMachine.IDLE:
				hasPlayed = false;
				pigAnim.SetBool("isMoving", false);
				break;
			case StateMachine.POSSESSED:
				if(!pigAudioSource.isPlaying && !hasPlayed)
				{
					StartCoroutine(PlaySound(squealSound));
				}
				pigAnim.SetBool("isMoving", false);
				break;
			case StateMachine.WAIT:
				pigAnim.SetBool("isMoving", false);
				break;

		}
	}
    public void PlaySqueal()
    {
        StartCoroutine(PlaySound(squealSound));
    }
	IEnumerator PlaySound(AudioClip _sound)
	{
		pigAudioSource.clip = _sound;
		pigAudioSource.PlayOneShot(_sound);
		hasPlayed = true;

		yield return new WaitForEndOfFrame();
	}

}
