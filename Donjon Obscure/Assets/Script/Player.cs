using System.Collections;
using System.Collections.Generic;
using UnityEngine;
public class Player : MonoBehaviour
{
    public float MStep, LSpeed, RSpeed;
    public float LValue, QValue;
    Vector3 MStart, MEnd;
    Quaternion RStart, REnd;
    Collision PCollision;
    [SerializeField] GameObject torcheObject;
    [SerializeField] float torcheRadius;
    [SerializeField] float torcheIntensity;
    [SerializeField] bool torchIsActive;

    public bool canMove = true;
    //[SerializeField] GameObject checkGround;
    [SerializeField] GameObject gameobjectRotation;

    void Start()
    {

        MStart = this.transform.position;
        MEnd = MStart;

        RStart = this.transform.rotation;
        REnd = RStart;

        LValue = 1.0f;
        QValue = 1.0f;

        PCollision = this.GetComponent<Collision>();

    }

    // Update is called once per frame
    void Update()
    {

        if (canMove)
        {
            PlayerMove();
        }       

        ActiveTorche();
        WalkOnHole();
    }

    public void PlayerMove()
    {
        if (LValue >= 1.0f && QValue >= 1)
        {
            Vector3 _position = this.transform.position;
            float h_input = Input.GetAxis("Horizontal");
            float v_input = Input.GetAxis("Vertical");
            if (v_input > 0.0f)
            {

                if (!PCollision.CheckFront())
                {

                    LValue = 0.0f;
                    MStart = this.transform.position;
                    MEnd = this.transform.position + this.transform.forward * MStep;
                    gameobjectRotation.transform.rotation = Quaternion.Euler(0.0f, 0.0f, 0.0f);
                }
            }
            else if (v_input < 0.0f)
            {
                if (!PCollision.CheckBack())
                {
                    LValue = 0.0f;
                    MStart = this.transform.position;
                    MEnd = this.transform.position - this.transform.forward * MStep;
                    gameobjectRotation.transform.rotation = Quaternion.Euler(0.0f, 180.0f, 0.0f);
                }
            }
            else if (h_input < 0.0f)
            {
                if (!PCollision.CheckLeft())
                {
                    LValue = 0.0f;
                    MStart = this.transform.position;
                    MEnd = this.transform.position - this.transform.right * MStep;
                    gameobjectRotation.transform.rotation = Quaternion.Euler(0.0f, -90.0f, 0.0f);
                }
            }
            else if (h_input > 0.0f)
            {
                if (!PCollision.CheckRight())
                {
                    LValue = 0.0f;
                    MStart = this.transform.position;
                    MEnd = this.transform.position + this.transform.right * MStep;
                    gameobjectRotation.transform.rotation = Quaternion.Euler(0.0f, 90.0f, -0.0f);
                }
            }
        }
        LValue += LSpeed * Time.deltaTime;
        QValue += RSpeed * Time.deltaTime;
        if (LValue > 1.0f)
        {
            LValue = 1.0f;
        }
        this.transform.position = Vector3.Lerp(MStart, MEnd, LValue);
        this.transform.rotation = Quaternion.Lerp(RStart, REnd, QValue);

        //_animator.SetFloat("LValue", LValue);

    }    
    public void ActiveTorche()
    {
        if (Input.GetKeyDown("space"))
        {
            if (torchIsActive)
            {
                torcheObject.GetComponent<Light>().range = 0;
                torcheObject.GetComponent<Light>().intensity = 0;
                torchIsActive = false;
            }
            else if (!torchIsActive)
            {
                torcheObject.GetComponent<Light>().range = torcheRadius;
                torcheObject.GetComponent<Light>().intensity = torcheIntensity;
                torchIsActive = true;
            }

        }
    }    
    public void TakeDamage()
    {
        
    }    
    void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "doorend")
        {
            Debug.Log("do code doorend enter trigger");
        }
        if (other.gameObject.tag == "bonus")
        {
            
        }
    }
    public void FallInHole()
    {     
        canMove = false;
        Debug.Log("do code trigger hole");
    }
    void ApplyBonus(int bonuscheck)
    {
        if (bonuscheck == 1)
        {
            Debug.Log("do code bonustriger 1");
        }
        if (bonuscheck == 2)
        {
            Debug.Log("do code bonustriger 2");
        }
        if (bonuscheck == 3)
        {
       
        }
        if (bonuscheck == 4)
        {
       
        }
    }
}
