using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchableObject : MonoBehaviour
{
    private float _speed;
    private bool _isLaunched;
    private bool _isTrajectoryDrawn;
    private float _countTime;
    private int _numOfCollisions;

    private List<Vector3> _points;
    private Rigidbody _ballRigidbody;
    private LineRenderer _trajectoryRenderer;

    private readonly float _preLaunchDelay = 0.25f;
    private readonly float _postLaunchDelay = 0.25f;

    public readonly float Respawn_XDeviation = 0.5f;
    public readonly float Respawn_ZDeviation = 0.55f;
    public readonly float LaunchForce_Deviation = 50f;

    public Vector3 StartPosition { get; private set; }
    public Vector3 CarryPosition { get; private set; }
    public Vector3 TotalPosition { get; private set; }

    public bool LaunchedAndThenStopped { get; private set; }

    public Vector3 LaunchForce;

    private void Start()
    {
        _points = new List<Vector3>();

        CarryPosition = new Vector3(0, 0, 0);
        TotalPosition = new Vector3(0, 0, 0);
        StartPosition = new Vector3(0, 0, 0);

        LaunchedAndThenStopped = false;

        _numOfCollisions = 0;
    }

    private void Update()
    {
        if (_ballRigidbody == null || _trajectoryRenderer == null)
            return;

        _speed = _ballRigidbody.velocity.magnitude;

        if (_isLaunched)
        {
            if (_speed < 0.1)
            {
                _ballRigidbody.velocity = new Vector3(0, 0, 0);

                _trajectoryRenderer.positionCount = _points.Count;

                _trajectoryRenderer.SetPositions(_points.ToArray()); // See: https://answers.unity.com/questions/1226025/how-to-render-a-linerenderer-through-multiple-poin.html

                TotalPosition = _ballRigidbody.gameObject.transform.position;

                _isLaunched = false;

                _isTrajectoryDrawn = true;

                LaunchedAndThenStopped = true;

                TrajectoryDataContent tdc = Camera.main.GetComponentInChildren<TrajectoryDataContent>();

                var decimalDigits = tdc.DecimalDigits >= 0 ? tdc.DecimalDigits : 0;
                tdc.OnLaunchEvent(gameObject.transform.name + " Total: " + Vector3.Distance(TotalPosition, StartPosition).ToString("n" + decimalDigits));

                OnPostLaunch();

                return;
            }
        }

        if (!_isTrajectoryDrawn)
        {
            if (Mathf.Abs(Time.time - _countTime) >= 0.05f)
            {
                _points.Add(_ballRigidbody.gameObject.transform.position);

                _countTime = Time.time;
            }
        }
    }

    public void ReceiveCollision(Collision c)
    {
        if (_ballRigidbody == null)
            return;

        if (_numOfCollisions == 0)
        {
            CarryPosition = _ballRigidbody.gameObject.transform.position;

            TrajectoryDataContent tdc = Camera.main.GetComponentInChildren<TrajectoryDataContent>();

            var decimalDigits = tdc.DecimalDigits >= 0 ? tdc.DecimalDigits : 0;
            tdc.OnLaunchEvent(gameObject.transform.name + " Carry: " + Vector3.Distance(CarryPosition, StartPosition).ToString("n" + decimalDigits));
        }

        _numOfCollisions++;
    }

    private IEnumerator Launch(Rigidbody rigidbody, Vector3 force = default(Vector3))
    {
        _countTime = Time.time;

        if (_trajectoryRenderer.positionCount > 0)
        {
            _points.Clear();

            _trajectoryRenderer.positionCount = 0;
            _trajectoryRenderer.SetPositions(_points.ToArray());
        }

        _isTrajectoryDrawn = false;

        yield return new WaitForSeconds(_preLaunchDelay);

        rigidbody.AddForce(LaunchForce, ForceMode.Impulse);

        yield return new WaitForSeconds(_postLaunchDelay);

        _isLaunched = true;
    }

    private void OnLaunch(GameObject go, Vector3 force = default(Vector3))
    {
        if (go == null)
            return;

        _ballRigidbody = go.GetComponent<Rigidbody>();

        if (_ballRigidbody == null)
            return;

        _trajectoryRenderer = go.transform.GetComponentInChildren<LineRenderer>();

        if (_trajectoryRenderer == null)
            return;

        StartPosition = _ballRigidbody.gameObject.transform.position;

        EventTrigger eventTrigger = go.GetComponent<EventTrigger>();

        if (eventTrigger != null)
        {
            eventTrigger.triggers.Clear();
        }

        StartCoroutine(Launch(_ballRigidbody, force));
    }

    public void DoLaunch()
    {
        OnLaunch(gameObject); // See: looks like Unity GameObject event handlers don't like optional arguments ...
    }

    private void OnPostLaunch()
    {
        var goLO = gameObject.GetComponent<LaunchableObject>();
        var cloneStartPosition = new Vector3(goLO.StartPosition.x.DeviateBy(Respawn_XDeviation), goLO.StartPosition.y, goLO.StartPosition.z.DeviateBy(Respawn_ZDeviation));

        var clonedBall = Instantiate(gameObject, cloneStartPosition, Quaternion.identity);
        var cbLO = clonedBall.GetComponent<LaunchableObject>();
        cbLO.LaunchForce = goLO.LaunchForce.DeviateBy(goLO.LaunchForce_Deviation);

        var clonedBallEventTrigger = clonedBall.GetComponent<EventTrigger>();
        clonedBallEventTrigger.AddListener(EventTriggerType.PointerEnter, (o) => OnLaunch(clonedBall));
    }
}
