using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Movement : MonoBehaviour
{
    Rigidbody2D body;
    public GameObject bulletPrefab;

    float horizontal;

    public float runSpeed = 20.0f;
    public float shootSpeed = -20.0f;
    public float returnSpeed = 0.1f;

    public bool gameOverisOn = false;

    public float cooldownValue = 0.10f;
    private Image OverheatBarImage;
    private Image OverheatBarEffectImage;
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

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ManageScript = GameObject.FindGameObjectWithTag("GameManager");
    }

    void Update()
    {
        OverheatBarImage = ManageScript.GetComponent<ManageGame>().OverheatBarImage;
        OverheatBarEffectImage = ManageScript.GetComponent<ManageGame>().OverheatBarEffectImage;

        horizontal = Input.GetAxisRaw("Horizontal");

        specialEffect -= Input.GetAxis("Mouse ScrollWheel");

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
            effectcost = 0f;
        } else if(specialEffect > 0.6 && specialEffect < 0.8){
            specialEffectList.transform.GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(1).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(2).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(3).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            Effect1 = false;
            Effect2 = false;
            Effect3 = false;
            Effect4 = true;
            effectcost = 0f;
        }

        if(OverheatBarEffectImage.fillAmount < effectcost){
            specialEffectCanPay = false;
        } else {
            specialEffectCanPay = true;
        }

        if (Input.GetKeyDown("space") && ManageScript.GetComponent<ManageGame>().limitIsReached == true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            OverheatBarImage.fillAmount = OverheatBarImage.fillAmount - cooldownValue;

            body.AddForce(transform.up * shootSpeed);
            body.drag = 10f;
        }

        if (Input.GetMouseButtonDown(0) && ManageScript.GetComponent<ManageGame>().limitIsReached == true)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            OverheatBarImage.fillAmount = OverheatBarImage.fillAmount - cooldownValue;

            body.AddForce(transform.up * shootSpeed);
            body.drag = 10f;
        }

        if (Input.GetMouseButtonDown(1) && ManageScript.GetComponent<ManageGame>().limitIsReachedEffect == true && specialEffectCanPay == true)
        {
            if(Effect1 == true){
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(1).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(2).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(3).transform.localRotation);

                OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.2f;

                body.AddForce(transform.up * -2000f);
                body.drag = 10f;
            } else if(Effect2 == true){
                TimedSpecialValue = 10;
                SpecialEffectTimed();
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

    void SpecialEffectTimed(){
            if(TimedSpecialValue != 0){
                OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.05f;
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                body.AddForce(transform.up * -200f);
                body.drag = 10f;
                TimedSpecialValue--;
                Invoke("SpecialEffectTimed", 0.15f);
            } else{

            }
    }

    void KnipperEffectBar(){

    }

    private void FixedUpdate()
    {
        refrencePos.transform.position = new Vector2(transform.position.x, -8);
        transform.position = Vector2.MoveTowards(transform.position, refrencePos.transform.position, returnSpeed * Time.deltaTime);
        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);
    }
}
