using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnimalB : MonoBehaviour
{
    
    public GameObject comedero;
    public GameObject Cama;
    private float Hambriento;
    private float Hambre;
    private float Sueño;
    private float Somnolencia;
    private bool isEating = false;
    private bool isSleeping = false;
    private float Hambreact;
    private float Sueñoact;
    public bool defecar;
    public float moveSpeed = 2.0f;
    public float eatingRange = 0.3f; 
    public float eatingRate = 0.5f; 
    public float sleepingRange = 0.3f;
    public float sleepingRate = 0.5f;
    private Animator animator;
    private Vector3 wanderTarget;
    private float wanderTimer;
    public float wanderTime = 5.0f;

    private Vector2 camaPosition = new Vector2(-3.96f, 3.79f);
    private Vector2 comederoPosition = new Vector2(5.5346f, 0.2606f);
    private Vector2 minBounds;
    private Vector2 maxBounds;

    // Start is called before the first frame update
    void Start()
    {
        Hambre = UnityEngine.Random.Range(0, 0.25f);
        Sueño = UnityEngine.Random.Range(0, 0.25f);
        wanderTimer = wanderTime;
        animator = GetComponent<Animator>();

        

        minBounds = new Vector2(Mathf.Min(camaPosition.x, comederoPosition.x), Mathf.Min(camaPosition.y, comederoPosition.y));
        maxBounds = new Vector2(Mathf.Max(camaPosition.x, comederoPosition.x), Mathf.Max(camaPosition.y, comederoPosition.y));

        SetNewWanderTarget();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Hambriento = Mathf.Pow(2, Hambre) - 1f;
        Somnolencia = (1 - Hambriento) * 0.25f + Sueño * 0.75f;

        

        if (Hambriento > 0.5f || Somnolencia > 0.5f) //BUCKET 2
        {
            if (Hambriento > Somnolencia && !isEating && !isSleeping)
            {
                MoveToComedero();
            }
            else if (Somnolencia > Hambriento && !isEating && !isSleeping)
            {
                MoveToCama();
            }
        }
        else  //BUCKET 1
        {
            Wander();
        }

        
        if (isEating)
        {
            Hambre -= eatingRate * Time.deltaTime;

            if (Hambriento <= Hambreact * 0.5f)
            {
                animator.SetBool("Eating", false);
                isEating = false;
                defecar = true;
            }
        }
        else 
        {
            Hambre += 0.00005f;
        }

        
        if (isSleeping)
        {
            Sueño -= sleepingRate * Time.deltaTime;
            
            if (Somnolencia <= Sueñoact * 0.5f)
            {
                isSleeping = false;
            }
        }
        else
        {
            Sueño += 0.0001f;
        }
        Defecando();
    }

    void MoveToComedero()
    {
        Debug.Log("Hora de comer");
        if (comedero != null)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, comedero.transform.position, step);

            if (Vector3.Distance(transform.position, comedero.transform.position) < eatingRange)
            {
                
                Hambreact = Hambriento;
                animator.SetBool("Eating", true);
                comedero.GetComponent<Comedero>().comiendo();
                isEating = true;
            }
        }
    }

    void MoveToCama()
    {
        Debug.Log("Hora de dormir");
        if (Cama != null)
        {
            float step = moveSpeed * Time.deltaTime;
            transform.position = Vector3.MoveTowards(transform.position, Cama.transform.position, step);

            if (Vector3.Distance(transform.position, Cama.transform.position) < sleepingRange)
            {
                Sueñoact = Somnolencia;                
                
                isSleeping = true;
            }
        }
    }

    void Defecando()
    {
        if (defecar)
        {
            float randomValue = UnityEngine.Random.Range(0.0f, 1.1f); 
            randomValue = Mathf.Round(randomValue * 10) / 10.0f;

            if (randomValue == 1.0f)
            {
                Debug.Log("Defecando");
                defecar = false;
            }
        }
    }

    void Wander()
    {
        wanderTimer += Time.deltaTime;

        if (wanderTimer >= wanderTime)
        {
            wanderTimer = 0;
            SetNewWanderTarget();
        }

        float step = moveSpeed * Time.deltaTime;
        transform.position = Vector3.MoveTowards(transform.position, wanderTarget, step);

        if (Vector3.Distance(transform.position, wanderTarget) < 0.1f)
        {
            SetNewWanderTarget();
        }

        Debug.Log("Paseando");
    }

    void SetNewWanderTarget()
    {
        float randomX = UnityEngine.Random.Range(minBounds.x, maxBounds.x);
        float randomY = UnityEngine.Random.Range(minBounds.y, maxBounds.y);
        wanderTarget = new Vector3(randomX, transform.position.y, randomY);
    }

}