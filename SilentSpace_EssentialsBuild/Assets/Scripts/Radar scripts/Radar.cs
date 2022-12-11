using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Radar : MonoBehaviour
{
    public int distRadar;
    public int velRadar;
    public LayerMask mask;
    public Transform player;


    // Update is called once per frame
    private void Update()
    {
        transform.position = player.position;
        transform.Rotate(0, velRadar * Time.deltaTime, 0);

        RaycastHit hit;
        if (Physics.Raycast(transform.position, transform.forward, out hit, distRadar, mask))
        {
            hit.collider.GetComponentInChildren<Radar_detect>().Detectado = true;
        }
    }


    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawRay(transform.position, transform.forward * distRadar);
    }
}
