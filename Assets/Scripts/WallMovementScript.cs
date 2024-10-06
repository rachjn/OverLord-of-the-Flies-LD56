using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallMovement : MonoBehaviour
{
    public float shrinkSpeed = 0.0f; // Speed at which the walls shrink inward
    public Vector2 minSize = new Vector2(2f, 2f); // The minimum size the walls can shrink to
    public Transform topWall;
    public Transform bottomWall;
    public Transform leftWall;
    public Transform rightWall;

    private Vector3 originalTopWallPosition;
    private Vector3 originalBottomWallPosition;
    private Vector3 originalLeftWallPosition;
    private Vector3 originalRightWallPosition;
    // Start is called before the first frame update
    void Start()
    {
        // Store the original positions of the walls
        originalTopWallPosition = topWall.position;
        originalBottomWallPosition = bottomWall.position;
        originalLeftWallPosition = leftWall.position;
        originalRightWallPosition = rightWall.position;
    }
    
    private void Rescan()
    {
        var graphToScan = AstarPath.active.data.gridGraph;
        AstarPath.active.Scan(graphToScan);
    }

    // Update is called once per frame
    void Update()
    {
        // Move the walls inward at a constant speed
        if (Vector2.Distance(leftWall.position, rightWall.position) > minSize.x)
        {
            leftWall.position = Vector3.MoveTowards(leftWall.position, originalRightWallPosition, 0.03f * shrinkSpeed * Time.deltaTime);
            rightWall.position = Vector3.MoveTowards(rightWall.position, originalLeftWallPosition,  0.03f * shrinkSpeed * Time.deltaTime);
        }

        if (Vector2.Distance(topWall.position, bottomWall.position) > minSize.y)
        {
            topWall.position = Vector3.MoveTowards(topWall.position, originalBottomWallPosition, 0.02f * shrinkSpeed * Time.deltaTime);
            bottomWall.position = Vector3.MoveTowards(bottomWall.position, originalTopWallPosition, 0.02f * shrinkSpeed * Time.deltaTime);
        }
    }
}
