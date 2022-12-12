using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Oscillator : MonoBehaviour
{
    #region Serialized fields
    [SerializeField] Vector3 movementVector;
    [SerializeField] [Range(0, 1f)] float movementFactor;
    [SerializeField] float period = 2f; // period potreban da se ispuni cijeli ciklus
    #endregion


    #region Privates
    Vector3 startingPosition;
    private float Tau { get { return Mathf.PI * 2f; } } // Tau je PI * 2 -> oko 6.28...

    #endregion


    // Start is called before the first frame update
    void Start()
    {
        startingPosition = transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        MoveOscillator();
    }


    // Petljancija da bi se dobilo prirodno kretanje, a ne "drveno"
    private void MoveOscillator()
    {
        // Prevent divide by zero                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                 >Ł˝ z                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                                             
        if (period <= Mathf.Epsilon)
            return;

        float cycles = Time.time / period; // grows continually from 0

        // Sinusioda ima jedan ciklus od 2PI...
        // Ako zelimo jedan ciklus, stavimo 2PI. Ako zelimo pola ciklusa stavimo PI. 
        // Ako zelimo 10 ciklusa, stavljamo 20PI....itd
        float rawSinWave = Mathf.Sin(cycles * Tau); // Sinusioda se krece od -1 do 1

        // Dijelimo s 2, dobijemo -0.5 do 0.5, a nama se faktor treba kretati od 0 do 1.
        // Zato dodajemo ovih 0.5
        movementFactor = rawSinWave / 2f + 0.5f;
        //SetTrashyMovementFactor();

        // npr: 14 po x-u * 0.1 = 1.4 ---> pomakni ga za toliko  
        // Dovoljno je pomnožiti cijeli vektor jer smo u inspektoru postavili vrijednost za x na 20
        Vector3 offset = movementVector * movementFactor;
        transform.position = startingPosition - offset;
    }

    //private float DivideByZero()
    //{
    //    // U 10 sekundi, obavimo 5 ciklusa, ako je period 2...
    //    float divided = Time.time / period;
    //    print(divided);
    //    if (float.IsInfinity(divided) || float.IsNaN(divided))
    //        return 0;

    //    return divided;
    //}

    //private void SetTrashyMovementFactor()
    //{
    //    float movementSpeed = 0.003f;

    //    if (movementFactor >= 1f)
    //        goNegative = true;
    //    else if (movementFactor <= 0f)
    //        goNegative = false;

    //    if (goNegative)
    //    {
    //        movementFactor -= movementSpeed;
    //    }
    //    else
    //    {
    //        movementFactor += movementSpeed;
    //    }
    //}
}
