using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class KartController : MonoBehaviour
{
    [Header("Commands")]
    [SerializeField]
    private string _hAxisInputName, _accelerateInputName;
    private float rotationInput;

    [Header("Mesh")]
    [SerializeField]
    public Transform _carColliderAndMesh;

    [Header("Rigidbody")]
    [SerializeField]
    private Rigidbody _rb;

    [Header("Acceleration/Deceleration")]
    [SerializeField]
    private float _accelerationFactor, _decelerationFactor, _accelerationLerpInterpolator;
    private bool isAccelerating;

    [Header("Speed")]
    private float speedMax = 10, speedMaxBoost = 10, speedMin = 0, speed;
    private float rotationSpeed = 35f, maxAngle = 360;
    public bool UseBoost;
    public bool isTrapped;

    [Header("Terrain Influence")]
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField] 
    private float _terrainSpeedVariator;

    [Header("Curves")]
    [SerializeField]
    private AnimationCurve _accelerationCurve, _decelerationCurve;

    // --------- Utilisation item de type Boost ---------

    public void Boost(int ItemDelay)
    {
        if (!UseBoost)
        {
            StartCoroutine(BoostRoutine(ItemDelay));
        }
    }

    private IEnumerator BoostRoutine(int ItemDelay)
    {
        UseBoost = true;
        yield return new WaitForSeconds(ItemDelay);
        UseBoost = false;
    }

    public void Trap(float ItemDelay)
    {
        if (!isTrapped)
        {
            StartCoroutine(TrapRoutine(ItemDelay));
        }
    }

    private IEnumerator TrapRoutine(float ItemDelay)
    {
        isTrapped = true;
        yield return new WaitForSeconds(ItemDelay);
        isTrapped = false;
    }
    void Start()
    {
        _accelerationLerpInterpolator = 0f;
    }

    void Update()
    {
        // --------- Commandes ---------

        rotationInput = Input.GetAxis(_hAxisInputName);

        if (Input.GetButtonDown(_accelerateInputName))
        {
            isAccelerating = true;
        }

        if (Input.GetButtonUp(_accelerateInputName))
        {
            isAccelerating = false;
        }

        if (rotationInput != 0)
        {
            transform.eulerAngles += Vector3.up * rotationInput * rotationSpeed * Time.deltaTime;
        }

        // --------- Influence du terrain ---------

        if (Physics.Raycast(transform.position, transform.up * -1, out var info, 1, _layerMask))
        {
            GroundInfluence terrainBellow = info.transform.GetComponent<GroundInfluence>();

            if (terrainBellow != null)
            {
                _terrainSpeedVariator = terrainBellow.speedVariator;
            }
            else
            {
                _terrainSpeedVariator = 1;
            }
        }
        else
        {
            _terrainSpeedVariator = 1;
        }
    }

    // --------- Contact lors d'une Pente ---------

    private bool IsOnSlope(out RaycastHit hit, out float angle, out float angleZ)
    {
        hit = new RaycastHit();
        angle = 0f;
        angleZ = 0f;

        if (Physics.Raycast(transform.position + transform.forward * .5f, Vector3.down, out hit, 0.8f))
        {
            angle = Vector3.Angle(Vector3.up, hit.normal);
            angleZ = Vector3.Angle(Vector3.right, hit.normal);
            return angle != 0 && angle <= maxAngle;
        }
        return false;
    }

    private void FixedUpdate() // Note : FixedUpdate = lorsque l'on utilise la Physique
    {
        // --------- Acceleration et Deceleration---------

        if (isAccelerating)
        {
            _accelerationLerpInterpolator += _accelerationFactor * Time.deltaTime;
        }
        else
        {
            // La décélération est plus rapide
            _accelerationLerpInterpolator -= _decelerationFactor * 2f * Time.deltaTime;
        }

        _accelerationLerpInterpolator = Mathf.Clamp01(_accelerationLerpInterpolator);

        // --------- Utilisation d'un Boost ---------

        if (UseBoost)
        {
            speed = speedMaxBoost;
        }
        else if (isTrapped)
        {
            speed = speedMin;
        }
        else
        {
            speed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator) * speedMax * _terrainSpeedVariator;
        }

        // --------- Mouvements du Kart ---------

        //var forward = transform.forward;
        //var angle = 0f;
        //var angleZ = 0f;

        //if (IsOnSlope(out var hit, out angle, out angleZ))
        //{
        //    forward = Vector3.ProjectOnPlane(forward, hit.normal).normalized;

        //}

        //_carColliderAndMesh.eulerAngles = new Vector3(-angle, _carColliderAndMesh.eulerAngles.y, angleZ);

        var xAngle = Mathf.Clamp(transform.eulerAngles.x + 360, 320, 400);
        var yAngle = transform.eulerAngles.y;
        var zAngle = transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotationSpeed * Time.deltaTime * rotationInput, 0);
        _rb.MovePosition(transform.position + transform.forward * speed * Time.fixedDeltaTime);
    }
}
