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
    private float _accelerationFactor, _decelerationFactor;
    public bool IsAccelerating;
    public float AccelerationLerpInterpolator;

    [Header("Speed")]
    private float speedMax = 10, speedMin = 0, speedShrunk = 5;
    private float rotationSpeed = 35f;
    public float Speed, SpeedMaxBoost = 15;

    [Header("Items Effects")]
    public bool UseBoost;
    private bool isTrapped;
    private bool isShrunken;
    private Vector3 originalScale;
    private float originalYPosition;


    [Header("Terrain Influence")]
    [SerializeField]
    private LayerMask _layerMask;
    [SerializeField] 
    private float _terrainSpeedVariator;

    [Header("Curves")]
    [SerializeField]
    private AnimationCurve _accelerationCurve, _decelerationCurve;

    // --------- Utilisation items ---------

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
    
    public void LightningEffect(int ShrinkFactor, int ItemDelay)
    {
        if (!isShrunken)
        {
            StartCoroutine(LightningRoutine(ShrinkFactor, ItemDelay));
        }
    }

    private IEnumerator LightningRoutine(int ShrinkFactor, int ItemDelay)
    {
        isShrunken = true;

        originalYPosition = transform.position.y;
        originalScale = transform.localScale;
        transform.localScale = originalScale / ShrinkFactor;

        float newScale = (originalScale.y - transform.localScale.y) / 2;
        transform.position = new Vector3(transform.position.x, originalYPosition - newScale, transform.position.z);

        yield return new WaitForSeconds(ItemDelay);

        transform.localScale = originalScale;
        transform.position = new Vector3(transform.position.x, originalYPosition, transform.position.z);

        isShrunken = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (isShrunken)
        {
            if (collision.gameObject.CompareTag("Player"))
            {
                var newScale = transform.localScale;
                newScale.y = 0.1f;
                transform.localScale = newScale;
            }
        }
    }

    void Start()
    {
        AccelerationLerpInterpolator = 0f;
    }

    void Update()
    {
        // --------- Commandes ---------

        rotationInput = Input.GetAxis(_hAxisInputName);

        if (Input.GetButtonDown(_accelerateInputName))
        {
            IsAccelerating = true;
        }

        if (Input.GetButtonUp(_accelerateInputName))
        {
            IsAccelerating = false;
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
                _terrainSpeedVariator = terrainBellow.SpeedVariator;
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

    private void FixedUpdate() // Note : FixedUpdate = lorsque l'on utilise la Physique
    {
        // --------- Acceleration et Deceleration---------

        if (IsAccelerating)
        {
            AccelerationLerpInterpolator += _accelerationFactor * Time.deltaTime;
        }
        else
        {
            AccelerationLerpInterpolator -= _decelerationFactor * 2f * Time.deltaTime;
        }

        AccelerationLerpInterpolator = Mathf.Clamp01(AccelerationLerpInterpolator);

        // --------- Utilisation/Impact d'un Item ---------

        if (UseBoost)
        {
            Speed = SpeedMaxBoost;
        }
        else if (isTrapped)
        {
            Speed = speedMin;
        }
        else if (isShrunken)
        {
            Speed = _accelerationCurve.Evaluate(AccelerationLerpInterpolator) * speedShrunk * _terrainSpeedVariator;
        }
        else
        {
            Speed = _accelerationCurve.Evaluate(AccelerationLerpInterpolator) * speedMax * _terrainSpeedVariator;
        }

        var xAngle = Mathf.Clamp(transform.eulerAngles.x + 360, 320, 400);
        var yAngle = transform.eulerAngles.y;
        var zAngle = transform.eulerAngles.z;
        transform.eulerAngles = new Vector3(xAngle, yAngle, zAngle);

        transform.eulerAngles = new Vector3(0, transform.eulerAngles.y + rotationSpeed * Time.deltaTime * rotationInput, 0);
        _rb.MovePosition(transform.position + transform.forward * Speed * Time.fixedDeltaTime);
    }
}
