using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScoreController : MonoBehaviour
{
    public ParticleSystem particleScore;
    private float _countdownStopParticle;
    private bool _particleIsPlaying;

    void Update()
    {
        TimeToStopParticle();
    }

    //Verifica se a bola colidiu, caso tenha colidido o jogador marcou um ponto
    void OnTriggerEnter(Collider other)
    {
        if(other.tag == "Ball")
        {
            GameManager.GetPlayer().SetNewPosition();    
            MadeAPoint();
        }
    }

    //Define a posição da bola para a posição do player, soma um ponto no score, 
    //adiciona tempo de jogo, toca a animação de ponto, toca a particula de ponto e toca o som de ponto
    void MadeAPoint()
    {
        GameManager.GetPlayer().ball.transform.position = GameManager.GetPlayer().transform.position;
        GameManager.GetPlayer().ball.rbody.velocity = Vector3.zero;
        GameManager.GetPlayer().score++;
        GameManager.GetPlayer().ball.trail.enabled = false;
        GameManager.GetTime().AddedTime(0.2f);     
        GameManager.GetUI().canStartAddedTimeAnim = true;
        particleScore.Play();  
        _particleIsPlaying = true;  
        AudioManager.instance.Play("Point");
    }


    //Apenas para dar o tempo necessário para tocar a particula
    //e evitar que ela sejá tocada em loop
    void TimeToStopParticle()
    {
        if(_particleIsPlaying)
        {
            if(_countdownStopParticle < 1)
            {
                _countdownStopParticle += Time.deltaTime / 0.5f;
            }
            else
            {
                _countdownStopParticle = 0;
                _particleIsPlaying = false;
                particleScore.Stop(); 
            }
        }
    }
}
