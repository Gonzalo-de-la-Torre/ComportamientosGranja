using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using Random = UnityEngine.Random;

public class PlantaBehaviour : MonoBehaviour
{
    public int id;
    public bool estoyPlantado;
    public bool necesitoPlantar;
    public bool estoyRegado;
    public bool necesitoRegar;
    public bool estoyMaduro;
    public bool necesitoCosechar;
    public bool esDeDia = true;
    public const int MADUREZ_MAX=100;
    public float madurezActual;
    public float riego;
    public const int RIEGO_MAX=100;
    public Sprite[] sprites= new Sprite[5];
    public static event Action<int, bool> NecesitoRegarEvent;
    public static event Action<int, bool> NecesitoPlantarEvent;
    public static event Action<int, bool> NecesitoCosecharEvent;
    float nextUpdate;
    SpriteRenderer spriteRenderer;

    // Start is called before the first frame update
    void Start()
    {
        nextUpdate = Time.deltaTime;
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void FixedUpdate()
    {
            if (!estoyPlantado)
            {
                NecesitoPlantar();
            }
            if (!estoyRegado && !estoyMaduro && estoyPlantado)
            {
                NecesitoRegar();
            }
            if(!estoyMaduro && estoyRegado && esDeDia)
            {
                Crecer();
            }
            if (estoyMaduro)
            {
                NecesitoCosechar();
            }
        
    }

    void Crecer()
    {
        madurezActual+=Random.Range(0.01f, 0.03f);
        riego-=Random.Range(0.02f, 0.05f);
        Debug.Log("Crezco: "+madurezActual);
        if (madurezActual >= MADUREZ_MAX)
        {
            estoyMaduro=true;
            estoyRegado = false;
            //cambiar sprite
            spriteRenderer.sprite = sprites[4];
            return;
        }
        if (riego <= 0)
        {
            estoyRegado = false;
        }
        

    }
    void NecesitoPlantar()
    {
        if (!necesitoPlantar)
        {
            necesitoPlantar = true;
            NecesitoPlantarEvent(id,true);
            Debug.Log("Necesito plantar");
        }
        //listener de necesito plantarme
    }

    void NecesitoRegar()
    {
        if (!necesitoRegar)
        {
            necesitoRegar = true;
            NecesitoRegarEvent(id,true);
            Debug.Log("Necesito regar");
        }
    }
    void NecesitoCosechar()
    {
        if (!necesitoCosechar)
        {
            necesitoCosechar = true;
            NecesitoCosecharEvent(id,true);
            Debug.Log("Necesito regar");
        }
    }
    public void Plantar()
    {
        //Cambio de estado
        estoyPlantado = true;
        necesitoPlantar = false;
        Debug.Log("Me plantan");
        //Cambiar sprite
        spriteRenderer.sprite = sprites[1];
        //se reinician los campos
        NecesitoPlantarEvent(id, false);
    }
    public void Recoger()
    {
        estoyPlantado=false;
        necesitoCosechar=false;
        estoyMaduro = false;
        Debug.Log("Me recogen");
        riego = 0;
        madurezActual = 0;
        //Cambiar sprite
        spriteRenderer.sprite = sprites[0];
        NecesitoCosecharEvent(id, false);

    }
    public void Regar(int riego=100)
    {
        this.riego = riego;
        estoyRegado=true;
        necesitoRegar=false;
        NecesitoRegarEvent(id, false);
        Debug.Log("Me riegan");
        //Cambiar sprite? si se quiere se puede poner la tierra más oscura o poner un medidor de agua
        spriteRenderer.sprite = sprites[3];
    }
}
