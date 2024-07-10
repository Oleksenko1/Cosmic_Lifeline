using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BeamScript : MonoBehaviour
{
    private LineRenderer line;
    public Transform finishPos = null;
    public Transform startPos = null;

    private void OnEnable()
    {
        line = GetComponent<LineRenderer>();
        line.positionCount = 2;
    }

    private void LateUpdate()
    {
        line.SetPosition(0, startPos.position);
        line.SetPosition(1, finishPos.position);
    }
}
