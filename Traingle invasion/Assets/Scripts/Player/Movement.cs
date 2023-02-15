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

    float horizontal;

    public float runSpeed = 20.0f;
    public float shootSpeed = -20.0f;
    public float returnSpeed = 0.1f;

    public bool gameOverisOn = false;

    public float floatHeight;
    public float liftForce;    
    public float damping; 

    public GameObject playerGun;

    public float cooldownValue = 0.10f;
    private Image OverheatBarImage;
    private Image OverheatBarEffectImage;
    public GameObject CantPayEffectImage;
    public GameObject ManageScript;
    public GameObject refrencePos;
    public float specialEffect = 0;
    public GameObject specialEffectList;
    public int currentEffectIndex = 0;
    int TimedSpecialValue = 10;
    bool specialEffectCanPay = true;
    float effectcost = 0f;
    int homingTimedValue = 3;  
    ManageGame ManageGameScript;
    public float health = 100f;
    Backpack backpackScript;
    string currentAbillityName;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ManageScript = GameObject.FindGameObjectWithTag("GameManager");
        ManageGameScript = ManageScript.GetComponent<ManageGame>();
        backpackScript = GameObject.FindGameObjectWithTag("Player").GetComponent<Backpack>();

        effectcost = 0.2f;
    }

    void Update()
    {

        OverheatBarImage = ManageScript.GetComponent<ManageGame>().OverheatBarImage;
        OverheatBarEffectImage = ManageScript.GetComponent<ManageGame>().OverheatBarEffectImage;

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
            if(backpackScript.AbillitiesEquipedInfo[0].AbillitieEquipedsCost != 0){
                effectcost = backpackScript.AbillitiesEquipedInfo[0].AbillitieEquipedsCost;
                currentAbillityName = backpackScript.AbillitiesEquipedInfo[0].AbillitiesEquipedName;

            } else{
                effectcost = 2;
                currentAbillityName = null;
            }
            currentEffectIndex = 0;
        } else if(specialEffect > 0.2 && specialEffect < 0.4){
            specialEffectList.transform.GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(1).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            specialEffectList.transform.GetChild(2).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(3).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            if(backpackScript.AbillitiesEquipedInfo[1].AbillitieEquipedsCost != 0){
                effectcost = backpackScript.AbillitiesEquipedInfo[1].AbillitieEquipedsCost;
                currentAbillityName = backpackScript.AbillitiesEquipedInfo[1].AbillitiesEquipedName;
            } else{
                effectcost = 2;
                currentAbillityName = null;
            }
            currentEffectIndex = 1;
        } else if(specialEffect > 0.4 && specialEffect < 0.6){
            specialEffectList.transform.GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(1).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(2).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            specialEffectList.transform.GetChild(3).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            if(backpackScript.AbillitiesEquipedInfo[2].AbillitieEquipedsCost != 0){
                effectcost = backpackScript.AbillitiesEquipedInfo[2].AbillitieEquipedsCost;
                currentAbillityName = backpackScript.AbillitiesEquipedInfo[2].AbillitiesEquipedName;
            } else{
                effectcost = 2;
                currentAbillityName = null;
            }
            currentEffectIndex = 2;
        } else if(specialEffect > 0.6 && specialEffect < 0.8){
            specialEffectList.transform.GetChild(0).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(1).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(2).GetComponent<Image>().color = new Color(255f, 255f, 255f, 0.5f);
            specialEffectList.transform.GetChild(3).GetComponent<Image>().color = new Color(255f, 255f, 255f, 1f);
            if(backpackScript.AbillitiesEquipedInfo[3].AbillitieEquipedsCost != 0){
                effectcost = backpackScript.AbillitiesEquipedInfo[3].AbillitieEquipedsCost;
                currentAbillityName = backpackScript.AbillitiesEquipedInfo[3].AbillitiesEquipedName;
            } else{
                effectcost = 2;
                currentAbillityName = null;
            }
            currentEffectIndex = 3;
        }

        if(OverheatBarEffectImage.fillAmount < effectcost){
            specialEffectCanPay = false;
            CantPayEffectImage.SetActive(true);
        } else {
            specialEffectCanPay = true;
            CantPayEffectImage.SetActive(false);
        }

        if (Input.GetMouseButtonDown(0) && ManageScript.GetComponent<ManageGame>().limitIsReached == true && ManageGameScript.inCombat == true || Input.GetKeyDown("space") && ManageScript.GetComponent<ManageGame>().limitIsReached == true && ManageGameScript.inCombat == true)
        {
            Instantiate(bulletPrefab, playerGun.transform.position, playerGun.transform.rotation);

            OverheatBarImage.fillAmount = OverheatBarImage.fillAmount - cooldownValue;

            body.AddForce(transform.up * shootSpeed);
            body.drag = 10f;
        }

        if (Input.GetMouseButtonDown(1) && specialEffectCanPay == true && ManageGameScript.inCombat == true && currentAbillityName != null)
        {
            if(currentAbillityName == "Multiple"){
                Instantiate(bulletPrefab, transform.position, Quaternion.identity);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(1).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(2).transform.localRotation);
                Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(3).transform.localRotation);

                OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.2f;
                ManageGameScript.effectOverheatBarFillValue = ManageGameScript.effectOverheatBarFillValue - 0.2f;

                body.AddForce(transform.up * -2000f);
                body.drag = 10f;
            } else if(currentAbillityName == "Fast"){
                TimedSpecialValue = 10;
                SpecialEffectTimed();
            }else if(currentAbillityName == "Big"){
                Instantiate(bigBulletPrefab, transform.position, Quaternion.identity);
                OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.2f;
                ManageGameScript.effectOverheatBarFillValue = ManageGameScript.effectOverheatBarFillValue - 0.2f;
                body.AddForce(transform.up * -3000f);
                body.drag = 10f;
            }else if(currentAbillityName == "Homing"){
                homingTimedValue = 3;       
                HomingBulletTimed();
            }
            
            // if(currentEffectIndex == 0 && GetComponent<Backpack>().AbillitiesEquipedInfo[currentEffectIndex].AbillitiesEquipedName == "Multiple"){
            //     Instantiate(bulletPrefab, transform.position, Quaternion.identity);
            //     Instantiate(bulletPrefab, gameObject.transform.GetChild(0).transform.position, gameObject.transform.GetChild(0).transform.localRotation);
            //     Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(1).transform.localRotation);
            //     Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(2).transform.localRotation);
            //     Instantiate(bulletPrefab, gameObject.transform.GetChild(1).transform.position, gameObject.transform.GetChild(3).transform.localRotation);

            //     OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.2f;
            //     ManageGameScript.effectOverheatBarFillValue = ManageGameScript.effectOverheatBarFillValue - 0.2f;

            //     body.AddForce(transform.up * -2000f);
            //     body.drag = 10f;
            // } else if(currentEffectIndex == 1){
            //     TimedSpecialValue = 10;
            //     SpecialEffectTimed();
            // } else if(currentEffectIndex == 2){
            //     Instantiate(bigBulletPrefab, transform.position, Quaternion.identity);
            //     OverheatBarEffectImage.fillAmount = OverheatBarEffectImage.fillAmount - 0.2f;
            //     ManageGameScript.effectOverheatBarFillValue = ManageGameScript.effectOverheatBarFillValue - 0.2f;
            //     body.AddForce(transform.up * -3000f);
            //     body.drag = 10f;
            // } else if(currentEffectIndex == 3){    
            //     homingTimedValue = 3;       
            //     HomingBulletTimed();
            // }
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
            }
    }

    public LayerMask groundLayer;
    public float maxRayLength = 300;
    public Transform[] anchors = new Transform[2];
    public RaycastHit2D[] hits = new RaycastHit2D[2];

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);

        for (int i = 0; i < 2; i++){
            ApplyF(anchors[i], hits[i]);
        }

        // var dir = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        // var angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        // playerGun.transform.rotation = Quaternion.AngleAxis(angle -90, Vector3.forward);

    }

    void ApplyF(Transform anchor, RaycastHit2D hit){

        hit = Physics2D.Raycast(anchor.position, Vector3.down, maxRayLength, groundLayer.value);
    
        if (hit) {
            float distance = Mathf.Abs(hit.point.y - transform.position.y);

            float heightError = floatHeight - distance;

            float force = liftForce * heightError - body.velocity.y * damping;

            body.AddForceAtPosition(Vector3.up * force, anchor.position, ForceMode2D.Force);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.tag == "CombatArea")
        {
            ManageGameScript.inCombat = true;
        }
    }
}
