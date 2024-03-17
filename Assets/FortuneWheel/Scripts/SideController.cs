using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SideController : MonoBehaviour
{
    public int controlledSide;
    private Vector3 startRay;
    private Vector3 endRay;
    public int CheckSide()
    {
        startRay = Camera.main.ViewportToWorldPoint(new Vector3(0f, 0f, 0f));
        endRay = startRay * 100;
        Debug.Log("startray=" + startRay);
        Ray ray = new Ray(transform.position, transform.forward);
        
        RaycastHit hit = new RaycastHit();
        if (Physics.Raycast(ray, out hit))
        {
            if (hit.collider.TryGetComponent<Side>(out Side side))
            {
                if (side.numberOfSide==0)
                {
                    Debug.Log("Side=0");
                    return 0;
                }
                else
                {
                    Debug.Log("Side=1");
                    return 1;
                }
            }
        }
        Debug.Log("Side=000");
        return 0;

    }
    private void Update()
    {
        Debug.DrawRay(transform.position, transform.forward * 100f,Color.yellow);
    }
}
