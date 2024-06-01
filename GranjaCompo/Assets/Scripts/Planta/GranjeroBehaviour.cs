using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GranjeroBehaviour : MonoBehaviour
{
    public bool[] cosecharPlantas = new bool[3];
    public bool[] regarPlantas = new bool[3];
    public bool[] plantarPlantas = new bool[3];
    public GameObject[] plantas = new GameObject[3];
    private float moveSpeed = 1;
    public bool estoyAtareado;
    public bool bucket2 = false;
    public Queue<IEnumerator> accionesPlantas = new Queue<IEnumerator>();

    // Start is called before the first frame update
    void Start()
    {
        StartPlantas();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        CuidarPlantas();
    }


    private void StartPlantas()
    {
        PlantaBehaviour.NecesitoCosecharEvent += PlantaNecesitaCosecha;
        PlantaBehaviour.NecesitoRegarEvent += PlantaNecesitaRegar;
        PlantaBehaviour.NecesitoPlantarEvent += PlantaNecesitaPlantar;
    }

    private void PlantaNecesitaPlantar(int obj, bool boolean)
    {
        plantarPlantas[obj] = boolean;
    }

    private void PlantaNecesitaRegar(int obj, bool boolean)
    {
        regarPlantas[obj] = boolean;
    }

    private void PlantaNecesitaCosecha(int obj, bool boolean)
    {
        cosecharPlantas[obj] = boolean;
    }
    private void CuidarPlantas()
    {
        if (accionesPlantas.Count == 0)
        {
            if (plantarPlantas[0] || plantarPlantas[1] || plantarPlantas[2])
            {
                //Aqui se podria hacer que fuese a por semillas a algun lado, si no da igual
                for (int i = 0; i < plantarPlantas.Length; i++)
                {
                    if (plantarPlantas[i])
                    {
                        accionesPlantas.Enqueue(plantarPlanta(i));
                    }
                }
            }
            //Y luego prioridad a regar
            if (regarPlantas[0] || regarPlantas[1] || regarPlantas[2])
            {
                //Aqui se podria hacer que fuese a por una regadera a algun lado, si no da igual
                for (int i = 0; i < regarPlantas.Length; i++)
                {
                    if (regarPlantas[i])
                    {
                        accionesPlantas.Enqueue(regarPlanta(i));
                    }
                }
                //Aqui iria a dejar la regadera
            }
            //Dar prioridad a cosechar
            if (cosecharPlantas[0] || cosecharPlantas[1] || cosecharPlantas[2])
            {
                //Aqui se podria hacer que fuese a por una cesta a algun lado, si no da igual
                for (int i = 0; i < cosecharPlantas.Length; i++)
                {
                    if (cosecharPlantas[i])
                    {
                        accionesPlantas.Enqueue(cosecharPlanta(i));
                    }
                }
                //Aqui iria a dejar la cesta (y si da tiempo y se usa para algo interesante recibiria dinero en funcion de las plantas que entrega)
            }
        }
        if (accionesPlantas.Count > 0 && !estoyAtareado)
        {
            IEnumerator accionActual = accionesPlantas.Dequeue();
            StartCoroutine(accionActual);
        }
    }

    private IEnumerator plantarPlanta(int i)
    {
        estoyAtareado = true;
        while (Vector3.Distance(transform.position, plantas[i].transform.position) > 0.5f)
        {
            if (!bucket2)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, plantas[i].transform.position, step);
            }
            yield return null;
        }
        //Animnacion correcta
        plantas[i].GetComponent<PlantaBehaviour>().Plantar();
        estoyAtareado = false;
    }
    private IEnumerator cosecharPlanta(int i)
    {
        estoyAtareado = true;
        while (Vector3.Distance(transform.position, plantas[i].transform.position) > 0.5f)
        {
            if (!bucket2)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, plantas[i].transform.position, step);
            }
            yield return null;
        }
        //Animnacion correcta
        plantas[i].GetComponent<PlantaBehaviour>().Recoger();
        estoyAtareado = false;
    }
    private IEnumerator regarPlanta(int i)
    {
        estoyAtareado = true;
        while (Vector3.Distance(transform.position, plantas[i].transform.position) > 0.5f)
        {
            if (!bucket2)
            {
                float step = moveSpeed * Time.deltaTime;
                transform.position = Vector3.MoveTowards(transform.position, plantas[i].transform.position, step);
            }
            yield return null;
        }
        //Animnacion correcta
        plantas[i].GetComponent<PlantaBehaviour>().Regar();
        estoyAtareado = false;
    }
}