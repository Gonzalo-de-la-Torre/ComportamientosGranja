using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroB : MonoBehaviour
{
    static int Estado;
    public Comedero comedero;
    // Start is called before the first frame update
    void Start()
    {
        Estado = 0;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (comedero.hayComida())
        {
            irRellenar();
        }
        else
        {
            switch (Estado)
            {
                case 0:
                    Plantar();
                    break;
                case 1:
                    Regar();
                    break;
                case 2:
                    Abonar();
                    break;
                case 3:
                    Esperar();
                    break;
                case 4:
                    Recoger();
                    break;
                case 5:
                    Vender();
                    break;

            }
        }
    }

    void Plantar() 
    {
        Estado = 1;
    }
    void Regar() 
    {
        Estado = 2;
    }
    void Abonar() 
    {
        Estado = 3;
    }
    void Esperar() 
    {
        Estado = 4;
    }
    void Recoger() 
    {
        Estado = 5;
    }
    void Vender() 
    {
        Estado = 0;
    }
    void irRellenar() 
    {
        comedero.rellenar();
    }
}
