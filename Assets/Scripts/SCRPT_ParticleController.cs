using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SCRPT_ParticleController : MonoBehaviour
{

    [SerializeField] ParticleSystem movementParticle;

    public SCRPT_Player_Movement GroundCheck;


    // Movement Particle
    [Range(0, 10)]
    [SerializeField] int occurAfterVelocity;

    [Range(0, 0.2f)]
    [SerializeField] float dustFormationPeriod;

    [SerializeField] Rigidbody2D playerRb;

    float counter;

    // Fall Particle

    public ParticleSystem fallParticle;

    // Touch Particle

    public ParticleSystem touchParticle;

    void Update()
    {
        counter += Time.deltaTime;

        if (GroundCheck.isGrounded == true && Mathf.Abs(playerRb.velocity.x) > occurAfterVelocity)
        {
            if (counter > dustFormationPeriod)
            {
                movementParticle.Play();
                counter = 0;
            }
        }

    }

    public void PlayTouchParticle()
    {
        touchParticle.Play();
    }

}
