using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar_detect : MonoBehaviour
{

    public bool Detectado;
    

    // Update is called once per frame
    void Update()
    {
        if(Detectado)
        {
            gameObject.GetComponent<MeshRenderer>().enabled = true;
            StartCoroutine(ping());
        }
        else
        {

            gameObject.GetComponent<MeshRenderer>().enabled = false;

        }
     
        IEnumerator ping()
        {
            yield return new WaitForSeconds(2);
            Detectado = false;
        }


    }
}
