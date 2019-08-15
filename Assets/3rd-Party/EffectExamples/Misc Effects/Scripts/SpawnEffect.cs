using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : MonoBehaviour {

    public bool weapon = false;
    public float spawnEffectTime = 2;
    public float pause = 1;
    public AnimationCurve fadeIn;
    public AnimationCurve fadeOut;
    private bool aux;
    private bool aux2 = false;
    // ParticleSystem ps;
    float timer = 0;
    Renderer _renderer;

    int shaderProperty;

    private PlayerAttack pa;

    public Material[] mat;
    
	void Start ()
    {
        pa = FindObjectOfType<PlayerAttack>();
        shaderProperty = Shader.PropertyToID("_Progress");
        _renderer = GetComponent<Renderer>();
       // ps = GetComponentInChildren <ParticleSystem>();

        //var main = ps.main;
        //main.duration = spawnEffectTime;

       // ps.Play();

    }
	
	void Update ()
    {
        if (Input.GetKeyDown(KeyCode.F) && pa.isAttacking(0))
        {
            aux = !aux;      
            timer = 0;
            aux2 = true;
        }

        //Debug.Log(_renderer.material.GetFloat(shaderProperty));

      
        if (aux2)
        {
            if (timer < spawnEffectTime + pause)
            {
                if (aux)
                {
                    if (weapon)
                    {
                        _renderer.material.shader = Shader.Find("Custom/Dissolve");
                    }
                }
                else
                {
                    if (weapon)
                    {
                        _renderer.material.shader = Shader.Find("Standard");
                    }
                }
                if (aux)
                {
                    _renderer.material.SetFloat(shaderProperty, Mathf.InverseLerp(1, 0, timer));
                }
                else
                {
                    _renderer.material.SetFloat(shaderProperty, Mathf.InverseLerp(0, 1, timer));
                }

                pa.canAttack = !aux;
                timer += Time.deltaTime*1.5f;
            }
            else
            {
            
                aux2 = false;
            }

       
        }


    }
}
