using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchableObject : MonoBehaviour
{
    private List<Vector3> points;
    private float speed;
    private bool isLaunched;
    private bool isTrajectoryDrawn;
    private Rigidbody ballRigidbody;
    private float countTime;
    private LineRenderer trajectoryRenderer;

    private Vector3 startPosition;
    private Vector3 carryPosition;
    private Vector3 totalPosition;

    private int numOfCollisions;

    private readonly float preLaunchDelay = 0.5f;
    private readonly float postLaunchDelay = 0.5f;

    public Vector3 LaunchForce;

    private void Start()
    {
        points = new List<Vector3>();

        carryPosition = new Vector3(0, 0, 0);
        totalPosition = new Vector3(0, 0, 0);
        startPosition = new Vector3(0, 0, 0);

        numOfCollisions = 0;
    }

    private void Update()
    {
        if (ballRigidbody == null || trajectoryRenderer == null)
            return;

        speed = ballRigidbody.velocity.magnitude;

        if (isLaunched)
        {
            if (speed < 0.1)
            {
                ballRigidbody.velocity = new Vector3(0, 0, 0);

                trajectoryRenderer.positionCount = points.Count;

                trajectoryRenderer.SetPositions(points.ToArray()); // See: https://answers.unity.com/questions/1226025/how-to-render-a-linerenderer-through-multiple-poin.html

                totalPosition = ballRigidbody.gameObject.transform.position;

                isLaunched = false;

                isTrajectoryDrawn = true;

                return;
            }
        }

        if (!isTrajectoryDrawn)
        {
            if (Mathf.Abs(Time.time - countTime) >= 0.05f)
            {
                points.Add(ballRigidbody.gameObject.transform.position);

                countTime = Time.time;
            }
        }
    }

    public void ReceiveCollision(Collision c)
    {
        if (ballRigidbody == null)
            return;

        if (numOfCollisions == 0)
        {
            carryPosition = ballRigidbody.gameObject.transform.position;
        }

        numOfCollisions++;
    }

    private IEnumerator Launch(Rigidbody rigidbody)
    {
        countTime = Time.time;

        if (trajectoryRenderer.positionCount > 0)
        {
            points.Clear();

            trajectoryRenderer.positionCount = 0;
            trajectoryRenderer.SetPositions(points.ToArray());
        }

        isTrajectoryDrawn = false;

        yield return new WaitForSeconds(preLaunchDelay);

        rigidbody.AddForce(LaunchForce, ForceMode.Impulse);

        yield return new WaitForSeconds(postLaunchDelay);

        isLaunched = true;
    }

    public void OnLaunch()
    {
        ballRigidbody = gameObject.GetComponentInParent<Rigidbody>();

        if (ballRigidbody == null)
            return;

        trajectoryRenderer = gameObject.transform.parent.GetComponentInChildren<LineRenderer>();

        if (trajectoryRenderer == null)
            return;

        startPosition = ballRigidbody.gameObject.transform.position;

        EventTrigger eventTrigger = gameObject.GetComponentInParent<EventTrigger>();

        if (eventTrigger != null)
        {
            eventTrigger.triggers.Clear();
        }

        StartCoroutine(Launch(ballRigidbody));
    }
}
