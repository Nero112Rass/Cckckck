using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Bar : MonoBehaviour
{

    public float barValue;

    public float materiaRegen;

    public Slider materia;

    public float maxMateria;

    void Start()
    {
        


    }

    // Update is called once per frame
    void Update()
    {
        materia.value = barValue;
        if (materia.value<maxMateria)
            barValue += materiaRegen * Time.deltaTime;
    }
}
