using System;
using UnityEngine;

public class MovingEnemy : KillingObject {
    public Vector3[] localWaypoints;
    public float speed;
    public bool loop;
    public string color;

    private Vector3[] globalWaypoints;
    private int fromWaypointIndex;
    private float percentBetween;

    void Awake()
    {
        SpriteRenderer renderer = GetComponentInChildren<SpriteRenderer>();
        renderer.sprite = Resources.Load("Sprites/starfish_" + color) as Sprite;
    }

    void Start () {
        globalWaypoints = new Vector3[localWaypoints.Length];
        for(int i = 0; i < localWaypoints.Length; i++)
        {
            globalWaypoints[i] = localWaypoints[i] + transform.position;
        }
	}
	
	void Update () {
        Vector3 velocity = UpdateMovement();
        transform.Translate(velocity);
	}

    private Vector3 UpdateMovement()
    {
        fromWaypointIndex %= globalWaypoints.Length;
        int toWaypointIndex = (fromWaypointIndex + 1) % globalWaypoints.Length;
        float distanceBetweenWaypoints = Vector3.Distance(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex]);

        percentBetween += Time.deltaTime * speed / distanceBetweenWaypoints;

        Vector3 pos = Vector3.Lerp(globalWaypoints[fromWaypointIndex], globalWaypoints[toWaypointIndex], percentBetween);

        if (percentBetween >= 1)
        {
            percentBetween = 0;
            fromWaypointIndex++;

            if(!loop) { 
                if(fromWaypointIndex >= globalWaypoints.Length -1)
                {
                    fromWaypointIndex = 0;
                    Array.Reverse(globalWaypoints);
                }
            }
        }

        return pos - transform.position;
    }
#if UNITY_EDITOR
    private void OnDrawGizmos()
    {
        if (localWaypoints != null)
        {
            Gizmos.color = Color.red;
            float size = 0.5f;

            for (int i = 0; i < localWaypoints.Length; i++)
            {
                Vector3 global = (Application.isPlaying) ? globalWaypoints[i] : localWaypoints[i] + transform.position;
                Gizmos.DrawLine(global - Vector3.up * size, global + Vector3.up * size);
                Gizmos.DrawLine(global - Vector3.left * size, global + Vector3.left * size);
            }
        }
    }
#endif
}
