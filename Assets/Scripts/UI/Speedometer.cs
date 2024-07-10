using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Speedometer : MonoBehaviour
{
    public Rigidbody2D playerRB2D;
    public Text text;
    public Image arrow;

    //private bool stoped;
    private float speed = 0;

    private void Update()
    {
        //if (playerRB2D.velocity != Vector2.zero)
        //{
        //    verticalSpeed = Mathf.Abs(playerRB2D.velocity.y);
        //    horizontalSpeed = Mathf.Abs(playerRB2D.velocity.x);
        //    WriteInfo();
        //    if(stoped)
        //    {
        //        stoped = false;
        //    }
        //}
        //else if(stoped == false)
        //{
        //    stoped = true;
        //}

        speed = (Mathf.Abs(playerRB2D.velocity.x) + Mathf.Abs(playerRB2D.velocity.y)) * 10;
        WriteInfo();
        float angle = Mathf.Atan2(playerRB2D.velocity.y, playerRB2D.velocity.x) * Mathf.Rad2Deg - 45;
        arrow.transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);
    }
    private void WriteInfo()
    {
        text.text = "" + Mathf.Round(speed);
    }
}
