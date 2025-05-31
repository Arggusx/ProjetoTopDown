using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Player : MonoBehaviour
{

    [SerializeField] private float speed; // Para aparecer no inspector da Unity mesmo sendo 'private'
    [SerializeField] private float runSpeed;

    // Manipulação da física
    private Rigidbody2D rig;
    private PlayerItems playerItems;

    private float initialSpeed;
    private bool _isRunning;
    private bool _isRolling;
    private bool _isCutting;
    private bool _isDigging;
    private bool _isWatering;

    // Direção da movimentação
    private Vector2 _direction;

    public int handlingObj;

    public bool isCutting // Encapsulamento
    {
        get { return _isCutting; }
        set { _isCutting = value; }
    }

    public bool isRolling
    {
        get { return _isRolling; }
        set { _isRolling = value; }
    }

    public bool isRunning
    {
        get { return _isRunning; }
        set { _isRunning = value; }
    }

    public Vector2 direction
    {
        get { return _direction; }
        set { _direction = value; }
    }

    public bool isDigging
    {
        get { return _isDigging; }
        set { _isDigging = value; }
    }

    public bool isWatering
    {
        get { return _isWatering; }
        set { _isWatering = value; }
    }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>();
        playerItems = GetComponent<PlayerItems>();

        initialSpeed = speed; // Para quando o boneco parar de correr, voltar à velocidade inicial
    }

    // "Captura" inputs, normalmente
    public void Update()
    {

        if (Input.GetKeyDown(KeyCode.Q))
        {
            handlingObj = 1;
            Debug.Log("Setou 1");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            handlingObj = 2;
            Debug.Log("Setou 2");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            handlingObj = 3;
            Debug.Log("Setou 3");
        }
        Debug.Log("handlingObj: " + handlingObj);


        // Eixos X e Y
        OnInput();
        OnRolling();
        OnRun();
        OnCutting();
        OnDig();
        OnWatering();
    }

    // Coisas relacionadas a física
    public void FixedUpdate()
    {
        OnMove();
    }

    #region Movement

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime);
    }
    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Quando o botão é apertado
        {
            speed = runSpeed;
            _isRunning = true;
            _isCutting = false; // Modificado
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) // Quando "solta" o botão
        {
            speed = initialSpeed;
            _isRunning = false;
        }
    }

    void OnCutting()
    {
        if (handlingObj == 1)
        {
            if (Input.GetMouseButtonDown(0))
            {
                _isCutting = true;
                speed = 0f;
                _isRunning = false; // Modificado
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isCutting = false;
                speed = initialSpeed;
            }
        }

    }

    void OnRolling()
    {
        if (Input.GetMouseButtonDown(1))
        {
            isRolling = true;
        }
        if (Input.GetMouseButtonUp(1))
        {
            isRolling = false;
        }
    }

    void OnDig()
    {

        if (handlingObj == 2)
        {
            if (Input.GetMouseButtonDown(0))
            {
                isDigging = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0))
            {
                isDigging = false;
                speed = initialSpeed;
            }
        }
    }

    void OnWatering()
    {

        if (handlingObj == 3)
        {
            if (Input.GetMouseButtonDown(0) && playerItems.currentWater > 0)
            {
                isWatering = true;
                speed = 0f;
            }
            if (Input.GetMouseButtonUp(0) || playerItems.currentWater < 0)
            {
                isWatering = false;
                speed = initialSpeed;
            }
            if (isWatering)
            {
                playerItems.currentWater -= 0.1f;
            }
        }
    }

    #endregion
}