using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using UnityEngine.Windows.Speech;

public class Player : MonoBehaviour
{
    [SerializeField] private float speed; // Velocidade de movimento normal
    [SerializeField] private float runSpeed; // Velocidade ao correr

    private Rigidbody2D rig; // Componente de física 2D
    private PlayerItems playerItems; // Script de itens do jogador
    private HUD_Controller hud_Controller;

    private float initialSpeed; // Armazena a velocidade padrão
    private bool _isRunning; // Indica se o jogador está correndo
    private bool _isRolling; // Indica se o jogador está rolando
    private bool _isCutting; // Indica se o jogador está cortando
    private bool _isDigging; // Indica se o jogador está cavando
    private bool _isWatering; // Indica se o jogador está regando

    private Vector2 _direction; // Direção da movimentação

    [HideInInspector] public int handlingObj; // Objeto atual que o jogador está segurando

    // Encapsulamentos para verificar estados externos
    public bool isCutting { get { return _isCutting; } set { _isCutting = value; } }
    public bool isRolling { get { return _isRolling; } set { _isRolling = value; } }
    public bool isRunning { get { return _isRunning; } set { _isRunning = value; } }
    public Vector2 direction { get { return _direction; } set { _direction = value; } }
    public bool isDigging { get { return _isDigging; } set { _isDigging = value; } }
    public bool isWatering { get { return _isWatering; } set { _isWatering = value; } }

    private void Start()
    {
        rig = GetComponent<Rigidbody2D>(); // Acessa o Rigidbody do jogador
        playerItems = GetComponent<PlayerItems>(); // Acessa o script de itens
        hud_Controller = GetComponent<HUD_Controller>();
        initialSpeed = speed; // Salva a velocidade inicial
    }

    public void Update()
    {
        // Troca o item nas mãos com base na tecla pressionada
        if (Input.GetKeyDown(KeyCode.Q))
        {
            handlingObj = 0; // Ferramenta de corte
            Debug.Log("Setou 1");
        }

        if (Input.GetKeyDown(KeyCode.R))
        {
            handlingObj = 1; // Pá para cavar
            Debug.Log("Setou 2");
        }

        if (Input.GetKeyDown(KeyCode.X))
        {
            handlingObj = 2; // Regador
            Debug.Log("Setou 3");
        }

        Debug.Log("handlingObj: " + handlingObj); // Exibe o item atual

        // Chama os métodos relacionados a ações
        OnInput(); // Captura a direção de movimento
        OnRolling(); // Rolar
        OnRun(); // Correr
        OnCutting(); // Cortar
        OnDig(); // Cavar
        OnWatering(); // Regar
    }

    public void FixedUpdate()
    {
        OnMove(); // Aplica movimentação física
    }

    #region Movement

    void OnInput()
    {
        _direction = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical")); // Direção com base nas teclas WASD ou setas
    }

    void OnMove()
    {
        rig.MovePosition(rig.position + _direction * speed * Time.fixedDeltaTime); // Move o jogador
    }

    void OnRun()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift)) // Ao pressionar Shift
        {
            speed = runSpeed; // Aumenta a velocidade
            _isRunning = true;
            _isCutting = false; // Para de cortar se estiver correndo
        }

        if (Input.GetKeyUp(KeyCode.LeftShift)) // Ao soltar Shift
        {
            speed = initialSpeed; // Volta à velocidade normal
            _isRunning = false;
        }
    }

    void OnCutting()
    {
        if (handlingObj == 0) // Se estiver com ferramenta de corte
        {
            if (Input.GetMouseButtonDown(0)) // Se clicar botão esquerdo do mouse
            {
                _isCutting = true;
                speed = 0f; // Para de se mover
                _isRunning = false; // Para de correr
            }
            if (Input.GetMouseButtonUp(0))
            {
                _isCutting = false; // Não corta
                speed = initialSpeed; // Volta a se mover
            }
        }
    }

    void OnRolling()
    {
        if (Input.GetMouseButtonDown(1)) // Se clicar direito do mouse para iniciar rolagem
        {
            isRolling = true; // Pode usar 'Rolar'
        }
        if (Input.GetMouseButtonUp(1)) // Se soltar o botão direito do mouse para parar de rolar
        {
            isRolling = false; // Não pode usar 'Rolar'
        }
    }

    void OnDig()
    {
        if (handlingObj == 1) // Se estiver com a pá
        {
            if (Input.GetMouseButtonDown(0)) // Se clicou botão esquedo do mouse
            {
                isDigging = true; // Começa a cavar
                speed = 0f; // Para de se mover
            }
            if (Input.GetMouseButtonUp(0)) // Se soltou o botão esquerdo do mouse
            {
                isDigging = false; // Para de cavar
                speed = initialSpeed; // Volta a se mover
            }
        }
    }

    void OnWatering()
    {
        if (handlingObj == 2) // Se o jogador estiver segurando o regador
        {
            // Se pressionar o botão esquerdo do mouse e ainda tiver água
            if (Input.GetMouseButtonDown(0) && playerItems.currentWater > 0)
            {
                isWatering = true; // Começa a regar
                speed = 0f; // Para o movimento enquanto rega
            }

            // Se soltar o botão do mouse ou se acabar a água
            if (Input.GetMouseButtonUp(0) || playerItems.currentWater < 0)
            {
                isWatering = false; // Para de regar
                speed = initialSpeed; // Volta à velocidade inicial
            }

            // Se estiver regando(true), consome água
            if (isWatering)
            {
                playerItems.currentWater -= 0.1f; // Diminui a água gradualmente
            }
        }
    }


    #endregion
}
