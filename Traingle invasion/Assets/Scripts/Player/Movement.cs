using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Rigidbody2D body;
    public GameObject bulletPrefab;
    public GameObject bigBulletPrefab;
    public GameObject homingBulletPrefab;
    public bool canDash = true;
    public bool isDashing;
    private float dashingSpeed = 10f;
    private float dashingTime = 0.2f;
    private float dashingCooldown = 1f;
    bool firstButtonPressed = false;
    float timeOfFirstButton;
    bool dashReset = false;

    float horizontal;

    public float runSpeed = 20.0f;
    public float shootSpeed = -20.0f;
    public float returnSpeed = 0.1f;

    public bool gameOverisOn = false;

    public float cooldownValue = 0.10f;
    private Image OverheatBarImage;
    private Image OverheatBarEffectImage;
    public GameObject CantPayEffectImage;
    public GameObject ManageScript;
    public GameObject refrencePos;
    public float specialEffect = 0;
    public GameObject specialEffectList;
    bool Effect1 = true;
    bool Effect2 = false;
    bool Effect3 = false;
    bool Effect4 = false;
    int TimedSpecialValue = 10;
    bool specialEffectCanPay = true;
    float effectcost = 0f;
    int homingTimedValue = 3;  
    ManageGame ManageGameScript;
    public float health = 100f;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ManageScript = GameObject.FindGameObjectWithTag("GameManager");

        Effect1 = true;
        Effect2 = false;
        Effect3 = false;
        Effect4 = false;
        effectcost = 0.2f;
    }

    void Update()
    {
        if(isDashing && gameOverisOn == false){
            return;
        }

        OverheatBarImage = ManageScript.GetComponent<ManageGame>().OverheatBarImage;
        OverheatBarEffectImage = ManageScript.GetComponent<ManageGame>().OverheatBarEffectImage;
        ManageGameScript = ManageScript.GetComponent<ManageGame>();

        horizontal = Input.GetAxisRaw("Horizontal");

        specialEffect -= Input.GetAxis("Mouse ScrollWheel");

        if(health <= 0f){
            gameOverisOn = true;
            ManageGameScript.OpenGameOverMenu();
        }

        if(specialEffect > 0.8f){
            specialEffect = 0;
        } else if(specialEffect < 0){
            specialEffect = 0.8f;
        }

        if(specialEffect > 0 && specialEffect < 0.2){
            specialEffectList.transform.GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            specialEffectList.transform.GetChild(1).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(2).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(3).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            Effect1 = true;
            Effect2 = false;
            Effect3 = false;
            Effect4 = false;
            effectcost = 0.2f;
        } else if(specialEffect > 0.2 && specialEffect < 0.4){
            specialEffectList.transform.GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(1).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            specialEffectList.transform.GetChild(2).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(3).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            Effect1 = false;
            Effect2 = true;
            Effect3 = false;
            Effect4 = false;
            effectcost = 0.5f;
        } else if(specialEffect > 0.4 && specialEffect < 0.6){
            specialEffectList.transform.GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(1).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(2).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            specialEffectList.transform.GetChild(3).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            Effect1 = false;
            Effect2 = false;
            Effect3 = true;
            Effect4 = false;
            effectcost = 0.2f;
        } else if(specialEffect > 0.6 && specialEffect < 0.8){
            specialEffectList.transform.GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(1).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(2).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(3).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            Effect1 = false;
            Effect2 = false;
            Effect3 = false;
            Effect4 = true;
            effectcost = 0.3f;
        }

        if(OverheatBarEffectImage.fillAmount < effectcost){
            specialEffectCanPay = false;
            CantPayEffectImage.SetActive(true);
        } else {
            specialEffectCanPay = true;
            CantPayEffectImage.SetActive(false);
        }


        if(canDash){
            if(Input.GetKeyDown(KeyCode.A) && firstButtonPressed){
                if(Time.time - timeOfFirstButton < 0.2f) {
                    StartCoroutine(DashLeft());
                }
    
                dashReset = true;
            }
            if(Input.GetKeyDown(KeyCode.A) && !firstButtonPressed) {
                firstButtonPressed = true;
                timeOfFirstButton = Time.time;
            }
            if(dashReset) {
                firstButtonPressed = false;
                dashReset = false;
            }
            if(Input.GetKeyDown(KeyCode.D) && firstButtonPressed){
                if(Time.time - timeOfFirstButton < 0.2f) {
                    StartCoroutine(DashRight());
                }
    
                dashReset = true;
            }
            if(Input.GetKeyDown(KeyCode.D) && !firstButtonPressed) {
                firstButtonPressed = true;
                timeOfFirstButton = Time.time;
            }
            if(dashReset) {
                firstButtonPressed = false;
                dashReset = false;
            }
        }

        if (Input.GetMouseButtonDown(0) && ManageScript.GetComponent<ManageGame>().limitIsReached == true || Input.GetKeyDown("space") && ManageScript.GetComponent<ManageGame>().limitIsReached == true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            OverheatBarImage.fillAmount = OverheatBarImage.fillAmount - cooldownValue;

            body.AddForce(transform.up * shootSpeed);
            body.drag = 10f;
        }

        if (Input.GetMouseButtonDown(1) && specialEffectCanPay == true)
        {
            if(Effect1 == true){
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(1).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(2).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(3).transform.localRotation);

                OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.2f;
                ManageGameScript.effectOverheatBarFillValue = ManageGameScript.effectOverheatBarFillValue - 0.2f;

                body.AddForce(transform.up * -2000f);
                body.drag = 10f;
            } else if(Effect2 == true){
                TimedSpecialValue = 10;
                SpecialEffectTimed();
            } else if(Effect3 == true){
                Instantiate(bigBulletPrefab, transform.position, Quaternion.identity);
                OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.2f;
                ManageGameScript.effectOverheatBarFillValue = ManageGameScript.effectOverheatBarFillValue - 0.2f;
                body.AddForce(transform.up * -3000f);
                body.drag = 10f;
            } else if(Effect4 == true){    
                homingTimedValue = 3;       
                HomingBulletTimed();
            }
        }

        if (Input.GetKeyDown(KeyCode.P))
        {
            Time.timeScale = 0;
        }

        if(gameOverisOn == true)
        {
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
                Time.timeScale = 1;
                gameOverisOn = false;
            }
        }

    }

    void HomingBulletTimed(){
        if(homingTimedValue != 0){
                Instantiate(homingBulletPrefab, transform.position, Quaternion.identity);
                OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.1f;
                ManageGameScript.effectOverheatBarFillValue = ManageGameScript.effectOverheatBarFillValue - 0.1f;
                homingTimedValue--;
                body.AddForce(transform.up * -300f);
                body.drag = 10f;
                Invoke("HomingBulletTimed", 0.15f);
        } else{

        }
    }

    void SpecialEffectTimed(){
            if(TimedSpecialValue != 0){
                OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.05f;
                ManageGameScript.effectOverheatBarFillValue = ManageGameScript.effectOverheatBarFillValue - 0.5f;
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                body.AddForce(transform.up * -200f);
                body.drag = 10f;
                TimedSpecialValue--;
                Invoke("SpecialEffectTimed", 0.15f);
            } else{

            }
    }

    private void FixedUpdate()
    {
        if(isDashing && gameOverisOn == false){
            return;
        }

        refrencePos.transform.position = new Vector2(transform.position.x, -8);
        transform.position = Vector2.MoveTowards(transform.position, refrencePos.transform.position, returnSpeed * Time.deltaTime);
        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);
    }

    private IEnumerator DashLeft(){
        canDash = false;
        isDashing = true;
        body.velocity = new Vector2(transform.localScale.x * -dashingSpeed, 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
    private IEnumerator DashRight(){
        canDash = false;
        isDashing = true;
        body.velocity = new Vector2(transform.localScale.x * dashingSpeed, 0f);
        yield return new WaitForSeconds(dashingTime);
        isDashing = false;
        yield return new WaitForSeconds(dashingCooldown);
        canDash = true;
    }
}
