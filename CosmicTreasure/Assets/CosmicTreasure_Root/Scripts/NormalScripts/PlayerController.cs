using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations;
using UnityEngine.PlayerLoop;
using UnityEngine.Scripting.APIUpdating;
using static EnemyTest;

public class PlayerController : MonoBehaviour
{
    //CODEAR: QUE LA VELOCIDAD EN Y SEA LA MITAD QUE EN X

    ParticleSystem soundAreaParticle;
    
    public Rigidbody2D body;

    public SpriteRenderer spriteRenderer;

    [SerializeField] Collider2D[] enemCol;
    public bool enemyDetection;
    [SerializeField]LayerMask enemyLayer;

    public enum PlayerState { normal, stealth }

    [Header("States Player")]
    [SerializeField] PlayerState currentState;
    public bool isNormal;
    public bool isStealth;

    [Header("Normal Animation")]
    public List<Sprite> nSprites;
    public List<Sprite> neSprites;
    public List<Sprite> eSprites;
    public List<Sprite> seSprites;
    public List<Sprite> sSprites;

    [Header("Stealth Animation")]
    public List<Sprite> nCSprites;
    public List<Sprite> neCSprites;
    public List<Sprite> eCSprites;
    public List<Sprite> seCSprites;
    public List<Sprite> sCSprites;

    [Header("Player Stats")]
    public float speedX = 20f;
    public float speedY = 3f;

    public float walkSpeed;

    public float walkSpeedStealth;

    public float frameRate;

    public float frameRateStealth;

    float idleTime;

    [Header("Camera Disable")]
    public float radiusDesactivation;
    public bool isInRange;

    //Variable para la mecanica del sonido
    private Transform enemy;
    public float lineOfSite;
    public EnemyTest chasing;


    public Vector2 direction;
   

    private void Start()
    {
        isNormal = true;

        soundAreaParticle = GetComponentInChildren<ParticleSystem>();
        //currentState = PlayerState.normal;
        //enemy = GameObject.FindGameObjectWithTag("Enemy").transform;
    }

    private void Update()
    {

        direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical")/2).normalized;

        //body.velocity = direction * walkSpeed;

        PlayerStateManagement();

        if (isNormal && !isStealth) { currentState = PlayerState.normal; }
        if (!isNormal && isStealth) { currentState = PlayerState.stealth; }

        if (Input.GetMouseButtonDown(1) && isNormal)//if (Input.GetKeyDown(KeyCode.F) && isNormal)
        {
            Debug.Log("Cambio a stealth");
            isNormal = false;
            isStealth = true;
        }
        else if (Input.GetMouseButtonDown(1) && !isNormal)//else if (Input.GetKeyDown(KeyCode.F) && !isNormal)
        {
            Debug.Log("Cambio a normal");
            isNormal = true;
            isStealth = false;
        }


        CameraDisable();     //FUNCIONA?????????


        /*
        float distanceFromEnemy = Vector2.Distance(enemy.position, transform.position);    //Cuando el enemigo entra en la zona del player, pasa a chasing
        if (distanceFromEnemy < lineOfSite && isNormal && direction != Vector2.zero)
        {
            chasing.isChasing = true;   // NO LO HACE SI HAY MAS DE UN ENEMIGO
        }
        */


        //HandleSpriteFlip();
        //SetSprite();
    }

    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, lineOfSite);
        Gizmos.color = Color.red;
        Gizmos.DrawWireSphere(transform.position, radiusDesactivation);      //Gizmo para la desactivacion de camaras
    }


    void SetSprite()
    {
        List<Sprite> directionSprites = GetSpriteDirection();

        if (directionSprites != null)
        { //holding a direction

            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRate);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
        }
        else
        {
            idleTime = Time.time;
        }

    }

    void HandleSpriteFlip()
    {
        if (!spriteRenderer.flipX && direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    List<Sprite> GetSpriteDirection()
    {
        List<Sprite> selectedSprites = null;

        if (direction.y > 0) //North
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = neSprites;
            }
            else //neutral X
            {
                selectedSprites = nSprites;
            }
        }
        else if (direction.y < 0) //South
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = seSprites;
            }
            else
            {
                selectedSprites = sSprites;
            }
        }
        else //neutral
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = eSprites;
            }
        }

        return selectedSprites;

    }

    void SetSpriteStealth()
    {
        List<Sprite> directionSprites = GetSpriteDirectionStealth();

        if (directionSprites != null)
        { //holding a direction

            float playTime = Time.time - idleTime;
            int totalFrames = (int)(playTime * frameRateStealth);
            int frame = totalFrames % directionSprites.Count;

            spriteRenderer.sprite = directionSprites[frame];
        }
        else
        {
            idleTime = Time.time;
        }

    }

    void HandleSpriteFlipStealth()
    {
        if (!spriteRenderer.flipX && direction.x < 0)
        {
            spriteRenderer.flipX = true;
        }
        else if (spriteRenderer.flipX && direction.x > 0)
        {
            spriteRenderer.flipX = false;
        }

    }

    List<Sprite> GetSpriteDirectionStealth()
    {
        List<Sprite> selectedSprites = null;

        if (direction.y > 0) //North
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = neCSprites;
            }
            else //neutral X
            {
                selectedSprites = nCSprites;
            }
        }
        else if (direction.y < 0) //South
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = seCSprites;
            }
            else
            {
                selectedSprites = sCSprites;
            }
        }
        else //neutral
        {
            if (Mathf.Abs(direction.x) > 0) //east or west
            {
                selectedSprites = eCSprites;
            }
        }

        return selectedSprites;

    }

    private void Normal()
    {
        Debug.Log("Estoy en normal");
        /*
        float horizontalMovement = Input.GetAxis("Horizontal");
        transform.Translate(Vector3.right * horizontalMovement * speedX * Time.deltaTime);

        float verticalMovement = Input.GetAxis("Vertical");
        transform.Translate(Vector3.up * verticalMovement * speedY * Time.deltaTime);
        body.velocity = direction * speedX;
        body.velocity = direction * speedY;
        */
        body.velocity = direction * walkSpeed;
        HandleSpriteFlip();
        SetSprite();
        if((body.velocity.x != 0 || body.velocity.y !=0) && soundAreaParticle.isStopped) { soundAreaParticle.Play(); }
    }

    private void Stealth()
    {
        Debug.Log("Estoy en sigilo");
        body.velocity = direction * walkSpeedStealth;
        HandleSpriteFlipStealth();
        SetSpriteStealth();

    }

    void PlayerStateManagement()
    {
        switch (currentState)
        {
            case PlayerState.normal:
                Normal();
                break;

            case PlayerState.stealth:
                Stealth();
                break;

        }



    }

    private void CameraDisable()
    {


        Collider2D[] hitColliders = Physics2D.OverlapCircleAll(transform.position, radiusDesactivation);
        foreach (Collider2D col in hitColliders)
        {

            if (col.gameObject.CompareTag("Camera"))
            {
                isInRange = true;
            }
        }


    }
}
