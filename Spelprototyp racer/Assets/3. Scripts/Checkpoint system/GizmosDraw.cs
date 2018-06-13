using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GizmosDraw : MonoBehaviour
{
    public Transform target;
    public int CheckNr = 0;
    public List<GameObject> checkpoints3 = new List<GameObject>();
    private bool called = false;

    public void assignChecks(List<GameObject> get, int nr)
    {
        CheckNr = nr;
        checkpoints3 = get;
        called = true;
    }

    private void OnDrawGizmos()
    {
        if (called == true)
        {
            for (int i = 0; i < CheckNr - 1; ++i)
            {
                Gizmos.color = Color.red;
                //Gizmos.DrawWireSphere(checkpoints2[i].transform.position, 1);
                Gizmos.DrawLine(checkpoints3[i].transform.position, checkpoints3[i + 1].transform.position);
            }
        }
        //Gizmos.color = Color.red;
        //Gizmos.DrawWireSphere(transform.position, 1);
        //Gizmos.DrawLine(transform.position, target.position);
    }
}
    //public int CheckNr = 0;
    //public List<GameObject> checkpoints3 = new List<GameObject>();
    //private bool called = false;

    //public void assignChecks(List<GameObject> get, int nr)
    //{
    //    CheckNr = nr;
    //    checkpoints3 = get;
    //    called = true;
    //}


    //private void OnDrawGizmos()
    //{
    //    if (called == true)
    //    {
    //        for (int i = 0; i < CheckNr - 1; ++i)
    //        {
    //            Gizmos.color = Color.red;
    //            //Gizmos.DrawWireSphere(checkpoints2[i].transform.position, 1);
    //            Gizmos.DrawLine(checkpoints3[i].transform.position, checkpoints3[i + 1].transform.position);
    //        }
    //    }
    //}
    //}

