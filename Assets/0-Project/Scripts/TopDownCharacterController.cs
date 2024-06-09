using UnityEngine;

public class TopDownCharacterController : MonoBehaviour
{
    public float moveSpeed = 5f; // Karakterin hareket h�z�
    public float rotationSpeed = 10f; // Karakterin rotasyon h�z�
    private Rigidbody rb; // Karakterin Rigidbody bile�eni
    private Vector3 movement; // Hareket y�n�

    #region animation variables
    [SerializeField]
    private Animator animator;
    const string MOVE = "move";
    const string REPAIR = "repair";

    public bool repairState;
    #endregion  

    private bool isOnRepairArea; //repair alan�nda ise
    void Start()
    {
        // Rigidbody bile�enini al
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        // Input'tan hareket vekt�r�n� al
        float moveHorizontal = Input.GetAxis("Horizontal");
        float moveVertical = Input.GetAxis("Vertical");

        // Hareket vekt�r�n� belirle
        movement = new Vector3(moveHorizontal, 0.0f, moveVertical);

        // E�er hareket vekt�r� s�f�rdan farkl�ysa, rotasyonu ayarla
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

        animator.SetFloat(MOVE, movement.magnitude);
    }

    void FixedUpdate()
    {
        // Karakteri hareket ettir
        rb.MovePosition(rb.position + movement * moveSpeed * Time.fixedDeltaTime);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("RepairObject"))
        {
            isOnRepairArea = true;
            repairState = true;
            AnimationUpdate();
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("RepairObject"))
        {
            isOnRepairArea = false;
        }
    }

    private void AnimationUpdate()
    {
        animator.SetBool(REPAIR, repairState);

    }
}
