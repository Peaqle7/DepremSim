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

    public ParticleSystem hitParticle;
    public Transform hammer;

    public AudioSource source;
    public AudioClip hammerClip, walkClip;

    public TimerManager timerManager;
    private void Awake()
    {
        Instance = this;
        Application.targetFrameRate = 64;
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
        animator.SetFloat(MOVE, rb.velocity.magnitude);
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
        UIManager.Instance.GetTick(currentRepairObject.transform.position + Vector3.up);
        target = null;
        isOnRepairArea = false;
        repairState = false;
        currentObjectId = 0;
        RepairObjectManager.Instance.DetectColliders(currentRepairObject);
        currentRepairObject = null;
        AnimationUpdate();
        RepairObjectManager.Instance.repairObjectCount--;
        if (RepairObjectManager.Instance.repairObjectCount <= 0)
        {
            timerManager.timer = 0;
        }
    }
    public void OnRepair()
    {
        if (target != null)
        {
            currentRepairObject.GetComponent<RepairObject>().HammerAction();
            target.DOPunchScale(Vector3.one * .07f, .05f);
            hitParticle.transform.position = hammer.position;
            hitParticle.Play();
            source.volume = 0.45f;
            source.pitch = Random.Range(1, 1.4f);
            source.PlayOneShot(hammerClip);
            
        }
    }
    public void OnWalk()
    {
        if (rb.velocity.magnitude < 0.4F)
            return;
        source.volume = 0.2f;
        source.pitch = Random.Range(1, 1.4f);
        source.PlayOneShot(walkClip);
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
            else
            {
                UIManager.Instance.GetTick(other.transform.position + Vector3.up);
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
            //if (other.GetComponent<RepairObject>().isRepaired) 
            //{
            //    UIManager.Instance.OutTick(other.transform.position + Vector3.up);
            //}
        }
    }

    private void AnimationUpdate()
    {
        animator.SetBool(REPAIR, repairState);
    }
}
