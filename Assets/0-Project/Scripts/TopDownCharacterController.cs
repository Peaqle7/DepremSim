using UnityEngine;
using DG.Tweening;

public class TopDownCharacterController : MonoBehaviour
{
    public static TopDownCharacterController Instance;

    public float moveSpeed = 5f; // Karakterin hareket hýzý
    public float rotationSpeed = 10f; // Karakterin rotasyon hýzý
    public float maxSpeed = 10f; // Karakterin maksimum hýzý
    private Rigidbody rb; // Karakterin Rigidbody bileþeni
    private Vector3 movement; // Hareket yönü

    public int currentObjectId;
    public Transform target;
    public RepairObject currentRepairObject;

    #region animation variables
    [SerializeField]
    private Animator animator;
    const string MOVE = "move";
    const string REPAIR = "repair";

    public bool repairState;
    #endregion  

    private bool isOnRepairArea; //repair alanýnda ise
    private void Awake()
    {
        Instance = this;
    }
    void Start()
    {
        // Rigidbody bileþenini al
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input'tan hareket vektörünü al
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Hareket vektörünü belirle
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // Eðer hareket vektörü sýfýrdan farklýysa, rotasyonu ayarla
        if (movement != Vector3.zero)
        {
            Quaternion toRotation = Quaternion.LookRotation(movement, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, toRotation, rotationSpeed * Time.deltaTime);

            if (repairState)
            {
                repairState = false;
                AnimationUpdate();
            }

        }
        else
        {
            if (isOnRepairArea && !repairState)
            {
                repairState = true;
                AnimationUpdate();
            }
        }
        if (repairState && target != null)
        {
            Vector3 directionToTarget = (currentRepairObject.GetComponent<Transform>().position - transform.position).normalized;
            directionToTarget.y = 0;
            Quaternion lookRotation = Quaternion.LookRotation(directionToTarget, Vector3.up);
            transform.rotation = Quaternion.RotateTowards(transform.rotation, lookRotation, rotationSpeed * Time.deltaTime);
        }
        animator.SetFloat(MOVE, movement.magnitude);
    }

    void FixedUpdate()
    {
        // Karakteri hareket ettir
        //rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
        rb.AddForce(movement * moveSpeed);

        // Maksimum hýzý aþmamak için karakterin hýzýný sýnýrla
        if (rb.velocity.magnitude > maxSpeed)
        {
            rb.velocity = rb.velocity.normalized * maxSpeed;
        }

    }

    public void ClearRepairState()//repair oldugunda cagirilacak method
    {
        target = null;
        isOnRepairArea = false;
        repairState = false;
        currentObjectId = 0;
        currentRepairObject = null;
        AnimationUpdate();
    }
    public void OnRepair()
    {
        if (target != null)
        {
            currentRepairObject.GetComponent<RepairObject>().HammerAction();
            target.DOPunchScale(Vector3.one*.07f,.05f);
        }
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RepairObject"))
        {
            if (!other.GetComponent<RepairObject>().isRepaired)
            {
                currentObjectId = other.GetComponent<RepairObject>().ObjectId;
                isOnRepairArea = true;
                repairState = true;
                AnimationUpdate();
                target = other.GetComponent<RepairObject>().interactionObject;
                currentRepairObject = other.GetComponent<RepairObject>();
            }
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RepairObject"))
        {
            if (currentObjectId == other.GetComponent<RepairObject>().ObjectId)
            {
                isOnRepairArea = false;

            }
        }
    }

    private void AnimationUpdate()
    {
        animator.SetBool(REPAIR, repairState);
    }
}
