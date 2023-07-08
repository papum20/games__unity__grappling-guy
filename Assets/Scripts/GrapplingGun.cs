using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GrapplingGun : MonoBehaviour
{

    [Header("objects")]
    GameObject player;
    [SerializeField] Camera mainCamera;

    [Header("grapple components")]
    LineRenderer grappleRenderer;
    [SerializeField] LayerMask whatIsGrappleable;
    [SerializeField] float maxDistance;
    SpringJoint2D player_joint;
    [SerializeField] Transform gunTip;


    Vector2 grappleStart;
    Vector2 grapplePoint;
    Vector2 cursorPoint;

    float grappleLength;
    Vector2 direction;






    private void Awake()
    {
        player = transform.parent.gameObject;
        grappleRenderer = GetComponent<LineRenderer>();
    }



    private void Update()
    {
        grappleStart = gunTip.position;

        if (Input.GetButtonDown("grapple"))
            Start_Grappling();
        else if (Input.GetButtonUp("grapple"))
            End_Grappling();
    }



    private void LateUpdate()
    {
        if(isGrappling())
        {
            grappleLength = Vector3.Distance(grappleStart, grapplePoint);
            direction = (grapplePoint - grappleStart).normalized;
            Debug.Log("direction");
            Debug.Log(direction);
            Debug.Log("grapplePoint");
            Debug.Log(grapplePoint);
            Debug.Log(direction * maxDistance);
            Debug.Log(direction - direction * maxDistance);

            if (grappleLength > maxDistance)
                player.transform.position = grapplePoint - direction * maxDistance;
        }


        grappleStart = gunTip.position;
        Draw_Rope();
    }




    void Start_Grappling()
    {
        cursorPoint = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        direction = (cursorPoint - grappleStart).normalized;

        RaycastHit2D hit = Physics2D.Raycast(grappleStart, direction, maxDistance, whatIsGrappleable);
        if(hit.collider != null)
        {
            grapplePoint = hit.point;
            player_joint = player.AddComponent<SpringJoint2D>();
            player_joint.autoConfigureConnectedAnchor = false;
            player_joint.connectedAnchor = grapplePoint;

            grappleRenderer.positionCount = 2;
        }
    }



    void End_Grappling()
    {
        grappleRenderer.positionCount = 0;
        Destroy(player_joint);
    }



    void Draw_Rope()
    {
        if(isGrappling())
        {
            grappleRenderer.SetPosition(0, grappleStart);
            grappleRenderer.SetPosition(1, grapplePoint);
        }
    }





    bool isGrappling()
    {
        return player_joint != null;
    }


}
