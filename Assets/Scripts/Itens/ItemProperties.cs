using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemProperties : MonoBehaviour {

    //change emission color
    public bool changeColor;
    private Color baseColor;
    private Color endColor;
    private Color resultColor;

    private Quaternion color1;
    private Quaternion color2;
    private Quaternion colorEnd;

    private float lerpTime;
    private int auxLerp;

    //Visible on/off
    public bool invisible;
    public float minInvisible;
    public float maxInvisible;
    private Renderer render;
    private int shaderProperty;
    private bool aux;
    private bool aux2;
    private PlayerAttack pa;

    public float invisibleTimer;
    private float invisibleSpeed = 5f;

    void Start()
    {
        baseColor = Color.black;
        endColor = Color.blue;
        pa = FindObjectOfType<PlayerAttack>();
        shaderProperty = Shader.PropertyToID("_Progress");
        render = GetComponent<Renderer>();

    }

    void Update ()
    {
        Invisibility();
        var teste = invisibleTimer >= 1;
      //  Debug.Log("item properties: " + !aux +" aux - timer "+invisibleTimer);
        ChangeEmissionColor(changeColor);   
    }

    private void Invisibility()
    {
        //if (Input.GetKey(KeyCode.F) && pa.isAttacking(0) && !aux2)
        if (pa.gettingDamage)
        {

            aux = !aux;
            if (!aux)
            {
                invisibleTimer = 0f;
            }
            else
            {
                invisibleTimer = minInvisible;
            }
            aux2 = true;

                    if (aux2)
        {

            if (invisibleTimer >= 1)
            {

                aux2 = false;
            }
            else
            {
                invisibleTimer += Time.deltaTime * invisibleSpeed;
            }

            if (!aux)
            {
                for (int i = 0; i < render.materials.Length; i++)
                {
                    render.materials[i].SetFloat(shaderProperty, Mathf.Lerp(minInvisible, maxInvisible, invisibleTimer));
                }
            }
            else
            {
                for (int i = 0; i < render.materials.Length; i++)
                {
                    render.materials[i].SetFloat(shaderProperty, Mathf.Lerp(maxInvisible, minInvisible, invisibleTimer));
                }
            }

        }
        }
        else{
            for (int i = 0; i < render.materials.Length; i++)
                {
                    render.materials[i].SetFloat(shaderProperty, 1);
                }
        }


        //pa.canAttack = !aux && invisibleTimer >= 1;



    }

    private void ChangeEmissionColor(bool change)
    {
        if(change)
        {
            lerpTime += Time.deltaTime * 1.5f;
            //mat = GetComponent<Renderer>().material;
            render.material.EnableKeyword("_EMISSION");
            if (auxLerp == 0)
            {


                render.material.SetColor("_EmissionColor", ColorLerp(baseColor, endColor, lerpTime));
                StartCoroutine(timerColorChange(1));
            }
            else
            {
                render.material.SetColor("_EmissionColor", ColorLerp(endColor, baseColor, lerpTime));

                // mat.color = ColorLerp(endColor, baseColor, lerpTime);
                StartCoroutine(timerColorChange(0));
            }
        }
       
    }


    // (0f , 0f, 0f, 0f)
    private Color ColorLerp(Color start, Color end, float speed)
    {
        color1 = new Quaternion(start.r, start.g, start.b, start.a);
        color2 = new Quaternion(end.r, end.g, end.b, end.a);

        colorEnd = Quaternion.Slerp(color1, color2, speed);

        return new Color(colorEnd.x, colorEnd.y, colorEnd.z, colorEnd.w);
    }

    IEnumerator timerColorChange(int a)
    {
        yield return new WaitUntil(() => lerpTime >= 1);
        lerpTime = 0;
        auxLerp = a;
    }
}
