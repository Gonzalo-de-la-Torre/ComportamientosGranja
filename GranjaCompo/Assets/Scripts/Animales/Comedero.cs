using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Comedero : MonoBehaviour
{
    // Start is called before the first frame update
    public int Comidaactual;
    void Start()
    {
        Comidaactual = 200;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void comiendo() 
    {
        Comidaactual -= 10;
    }
    public void rellenar()
    {
        Comidaactual = 100;
    }
    public bool hayComida()
    {
        return Comidaactual > 0;
    }
}
