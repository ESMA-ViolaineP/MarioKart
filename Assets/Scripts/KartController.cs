using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.UIElements;

public class KartController : MonoBehaviour
{
    [Header("Commands")]
    [SerializeField]
    private string _hAxisInputName = "Horizontal", _accelerateInputName = "Accelerate";
    [SerializeField]
    private float _rotationInput;

    [Header("Mesh")]
    [SerializeField]
    public Transform _carColliderAndMesh;

    [Header("Rigidbody")]
    [SerializeField]
    private Rigidbody _rb;

    [Header("Acceleration/Deceleration")]
    [SerializeField]
    private float _accelerationFactor, _decelerationFactor, _accelerationLerpInterpolator, _decelerationLerpInterpolator;
    private float accelerationSpeed, decelerationSpeed;
    private bool isAccelerating;

    [Header("Speed")]
    private float _rotationSpeed = 0.5f, _speedMax, _speedMaxBoost = 10, _speed;
    private float maxAngle = 360;
    public bool UseBoost;

    [Header("Terrain Influence")]
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField] 
    private float _terrainSpeedVariator;

    [Header("Curves")]
    [SerializeField]
    private AnimationCurve _accelerationCurve, _decelerationCurve;

    // --------- Utilisation item de type Boost ---------

    public void ItemBoost(int ItemDelay)
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

    void Update()
    {
        // --------- Commandes ---------

        _rotationInput = Input.GetAxis(_hAxisInputName);

        if (Input.GetButtonDown(_accelerateInputName))
        {
            isAccelerating = true;
        }

        if (Input.GetButtonUp(_accelerateInputName))
        {
            isAccelerating = false;
        }

        // --------- Influence du terrain ---------

        if (Physics.Raycast(transform.position, transform.up * -1, out var info, 1, _layerMask))
        {

            Ground terrainBellow = info.transform.GetComponent<Ground>();
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
            _accelerationLerpInterpolator += _accelerationFactor;
            _decelerationLerpInterpolator -= _decelerationFactor * 2;
        }
        else
        {
            _decelerationLerpInterpolator += _decelerationFactor;
            _accelerationLerpInterpolator -= _accelerationFactor * 2;
        }

        _accelerationLerpInterpolator = Mathf.Clamp01(_accelerationLerpInterpolator);
        _decelerationLerpInterpolator = Mathf.Clamp01(_decelerationLerpInterpolator);

        accelerationSpeed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator) * _speedMax;
        decelerationSpeed = _decelerationCurve.Evaluate(_decelerationLerpInterpolator) * _speedMax;

        _speed = isAccelerating ? accelerationSpeed : decelerationSpeed;

        // --------- Utilisation d'un Boost ---------

        if (UseBoost)
        {
            _speed = _speedMaxBoost;
        }
        else
        {
            _speed = _accelerationCurve.Evaluate(_accelerationLerpInterpolator) * _speedMax * _terrainSpeedVariator;
        }

        // --------- Mouvements du Kart ---------

        var forward = transform.forward;
        var angle = 0f;
        var angleZ = 0f;

        if (IsOnSlope(out var hit, out angle, out angleZ))
        {
            forward = Vector3.ProjectOnPlane(forward, hit.normal).normalized;

        }

        _carColliderAndMesh.eulerAngles = new Vector3(-angle, _carColliderAndMesh.eulerAngles.y, angleZ);
        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + _rotationSpeed * Time.deltaTime * _rotationInput, 0);
        _rb.MovePosition(transform.position + forward * _speed * Time.fixedDeltaTime);
    }
}
