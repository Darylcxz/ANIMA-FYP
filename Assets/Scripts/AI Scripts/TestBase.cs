using UnityEngine;
using System.Collections;

public class TestBase : AIbase {

    [SerializeField] ParticleSystem explosion;
	Animator pigAnim;
	AudioSource pigSound;
	[SerializeField]
	AudioClip squealSound;
	[SerializeField]
	AudioClip sneezeSound;
    protected Vector3 asd;
	bool hasPlayed;
	// Use this for initialization
	protected override void Start () {
        agent = GetComponent<NavMeshAgent>();
        _rigidBody = GetComponent<Rigidbody>();
        origin = gameObject.transform.position;
		pigAnim = GetComponent<Animator>();
		pigSound = GetComponent<AudioSource>();
		
        
	}
	
	// Update is called once per frame
	
    protected override void ActivateAbility()
    {
		if (!explosion.isPlaying)
		{
			hasPlayed = false;
			explosion.Play();
			if (!pigSound.isPlaying && !hasPlayed)
			{
				StartCoroutine(PlaySound(sneezeSound));
			}
			StartCoroutine(PlaySound(sneezeSound));
			pigAnim.SetTrigger("tSneeze");
		}
		
    }
    protected override void PassiveAbility()
    {
      //Plays Pig Anims
		switch (AIState)
		{
 			case States.walk:
				hasPlayed = false;
				pigAnim.SetBool("isMoving", true);
				break;
			case States.idle:
				hasPlayed = false;
				pigAnim.SetBool("isMoving", false);
				break;
			case States.possessed:
				if (!pigSound.isPlaying&& !hasPlayed)
				{
					StartCoroutine(PlaySound(squealSound));
				}
				
				pigAnim.SetBool("isMoving", false);
				break;
			case States.doNothing:
				pigAnim.SetBool("isMoving", false);
				break;

		}
       
    }
	IEnumerator PlaySound(AudioClip _sound)
	{
		pigSound.clip = _sound;
		pigSound.Play();
		hasPlayed = true;

		
		yield return new WaitForEndOfFrame();
		
	}
   
}
