using UnityEngine;
using UnityEngine.InputSystem;

public class Player : MonoBehaviour
{
    [SerializeField] float speed = 5f;
    [SerializeField] float screenPaddingLeft = 0.5f;
    [SerializeField] float screenPaddingRight = 0.5f;
    [SerializeField] float screenPaddingTop = 0.5f;
    [SerializeField] float screenPaddingBottom = 0.5f;
    Vector2 rawInput;
    Vector2 minBounds;
    Vector2 maxBounds;
    Shooter shooter;

    void Awake()
    {
        shooter = GetComponent<Shooter>();
    }

    void Start()
    {
        InitBounds();
    }
    void Update()
    {
        Move();
    }

    void InitBounds(){
        Camera mainCam = Camera.main;
        minBounds = mainCam.ViewportToWorldPoint(new Vector2(0, 0));
        maxBounds = mainCam.ViewportToWorldPoint(new Vector2(1, 1));
    }

    void Move()
    {
        Vector3 delta = speed * Time.deltaTime * rawInput;
        Vector2 newPos = new()
        {
            x = Mathf.Clamp(transform.position.x + delta.x, minBounds.x + screenPaddingLeft, maxBounds.x - screenPaddingRight),
            y = Mathf.Clamp(transform.position.y + delta.y, minBounds.y + screenPaddingBottom, maxBounds.y - screenPaddingTop)
        };
        transform.position = newPos;
    }

    void OnMove(InputValue value){
        rawInput = value.Get<Vector2>();
    }

    void OnAttack(InputValue value){
        if(shooter != null){
            shooter.isFiring = value.isPressed;
        } 
    }
}
