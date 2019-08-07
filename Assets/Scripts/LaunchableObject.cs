using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class LaunchableObject : MonoBehaviour
{
    private float _speed;
    private bool _isTrajectoryDrawn;
    private float _countTime;
    private int _numOfCollisions;
    private float _speedStopThreshold = 0.1f;
    private float _timePointSamplingThreshold = 0.05f;

    private List<Vector3> _points;
    private Rigidbody _ballRigidbody;
    private LineRenderer _trajectoryRenderer;

    private readonly float _preLaunchDelay = 3f;
    private readonly float _postLaunchDelay = 0.25f;

    public readonly float Respawn_XDeviation = 0.1f;
    public readonly float Respawn_ZDeviation = 0.15f;
    public readonly float LaunchForce_Deviation = 50f;

    public bool IsLaunched { get; private set; }

    public Vector3 StartPosition { get; private set; }
    public Vector3 CarryPosition { get; private set; }
    public Vector3 TotalPosition { get; private set; }

    public Vector3 LaunchForce;

    private void Start()
    {
        _points = new List<Vector3>();

        CarryPosition = new Vector3(0, 0, 0);
        TotalPosition = new Vector3(0, 0, 0);
        StartPosition = new Vector3(0, 0, 0);

        _numOfCollisions = 0;
    }

    private void Update()
    {
        if (_ballRigidbody == null || _trajectoryRenderer == null)
            return;

        _speed = _ballRigidbody.velocity.magnitude;

        var goLO = _ballRigidbody.gameObject.GetComponent<LaunchableObject>();
        if (goLO != null && goLO.IsLaunched)
        {
            if (_speed < _speedStopThreshold)
            {
                _ballRigidbody.velocity = new Vector3(0, 0, 0);

                _trajectoryRenderer.positionCount = _points.Count;

                _trajectoryRenderer.SetPositions(_points.ToArray()); // See: https://answers.unity.com/questions/1226025/how-to-render-a-linerenderer-through-multiple-poin.html

                goLO.TotalPosition = _ballRigidbody.gameObject.transform.position;

                goLO.IsLaunched = false;

                _isTrajectoryDrawn = true;

                TrajectoryDataContent tdc = Camera.main.GetComponentInChildren<TrajectoryDataContent>();

                var decimalDigits = tdc.DecimalDigits >= 0 ? tdc.DecimalDigits : 0;
                tdc.OnLaunchEvent(_ballRigidbody.gameObject.transform.name + " Total: " + Vector3.Distance(goLO.TotalPosition, goLO.StartPosition).ToString("n" + decimalDigits));

                OnPostLaunch();

                return;
            }
        }

        if (!_isTrajectoryDrawn)
        {
            if (Mathf.Abs(Time.time - _countTime) >= _timePointSamplingThreshold)
            {
                _points.Add(_ballRigidbody.gameObject.transform.position);

                _countTime = Time.time;
            }
        }
    }

    public void ReceiveCollision(Collision c)
    {
        var goLO = c.gameObject.GetComponent<LaunchableObject>();
        if (goLO == null || !goLO.IsLaunched)
            return;

        if (_numOfCollisions == 0)
        {
            goLO.CarryPosition = c.rigidbody.gameObject.transform.position;

            TrajectoryDataContent tdc = Camera.main.GetComponentInChildren<TrajectoryDataContent>();

            var decimalDigits = tdc.DecimalDigits >= 0 ? tdc.DecimalDigits : 0;
            tdc.OnLaunchEvent(c.rigidbody.gameObject.transform.name + " Carry: " + Vector3.Distance(goLO.CarryPosition, goLO.StartPosition).ToString("n" + decimalDigits));
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

        var goLO = rigidbody.gameObject.GetComponent<LaunchableObject>();

        StatusContent sc = Camera.main.GetComponentInChildren<StatusContent>();

        sc.OnStatusChange("Launching in", (int)_preLaunchDelay);
        
        yield return new WaitForSeconds(_preLaunchDelay);

        if (!Common.IsPointerLookingToGameObject(rigidbody.gameObject))
        {
            MakeBallLaunchable(rigidbody.gameObject);

            sc.OnStatusChange("Idle.");

            yield break;
        }

        rigidbody.AddForce(force != default(Vector3) ? force : LaunchForce, ForceMode.Impulse);

        sc.OnStartPlayerAnimation(true);
        sc.OnStatusSFXPlay(true);
        sc.OnStatusChange("Launched!");

        yield return new WaitForSeconds(_postLaunchDelay);

        sc.OnStatusChange("Idle.");

        goLO.IsLaunched = true;
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

        var goLO = go.GetComponent<LaunchableObject>();
        if (goLO == null)
            return;

        goLO.StartPosition = _ballRigidbody.gameObject.transform.position;
        goLO.CarryPosition = new Vector3(0, 0, 0);
        goLO.TotalPosition = new Vector3(0, 0, 0);

        EventTrigger eventTrigger = go.GetComponent<EventTrigger>();

        if (eventTrigger != null)
        {
            eventTrigger.triggers.Clear();
        }

        StartCoroutine(Launch(_ballRigidbody, force));
    }

    private void OnCancelLaunch(GameObject go)
    {

    }

    public void DoLaunch()
    {
        OnLaunch(gameObject); // See: looks like Unity GameObject event handlers don't like optional arguments ...
    }

    public void CancelLaunch()
    {
        OnCancelLaunch(gameObject);
    }

    private void OnPostLaunch()
    {
        var goLO = gameObject.GetComponent<LaunchableObject>();
        var cloneStartPosition = new Vector3(goLO.StartPosition.x.DeviateBy(Respawn_XDeviation), goLO.StartPosition.y, goLO.StartPosition.z.DeviateBy(Respawn_ZDeviation));

        var clonedBall = Instantiate(gameObject, cloneStartPosition, Quaternion.identity);

        MakeBallLaunchable(clonedBall);
    }

    private void MakeBallLaunchable(GameObject go)
    {
        if (go == null)
            return;

        var cbLO = go.GetComponent<LaunchableObject>();

        var clonedBallEventTrigger = go.GetComponent<EventTrigger>();
        clonedBallEventTrigger.AddListener(EventTriggerType.PointerEnter, (o) => OnLaunch(go, cbLO.LaunchForce.DeviateBy(cbLO.LaunchForce_Deviation)));
    }
}
