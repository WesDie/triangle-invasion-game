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
    float vertical;

    public float runSpeed = 20.0f;

    public bool gameOverisOn = false;

    public float cooldownValue = 0.10f;
    GameObject OverheatBar;
    Image OverheatBarImage;
    GameObject ManageScript;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        OverheatBar = GameObject.Find("OverheatBar");
        OverheatBarImage = OverheatBar.GetComponent<Image>();
        ManageScript = GameObject.FindGameObjectWithTag("GameManager");
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        if (Input.GetKeyDown("space") && ManageScript.GetComponent<ManageGame>().limitIsReached == true && ManageScript.GetComponent<ManageGame>().OptionsMenuIsOpen == false)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            OverheatBarImage.fillAmount = OverheatBarImage.fillAmount - cooldownValue;
        }

        if (Input.GetMouseButtonDown(0) && ManageScript.GetComponent<ManageGame>().limitIsReached == true &&  ManageScript.GetComponent<ManageGame>().OptionsMenuIsOpen == false)
        {
            Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            OverheatBarImage.fillAmount = OverheatBarImage.fillAmount - cooldownValue;
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

    private void FixedUpdate()
    {
        body.velocity = new Vector2(horizontal * runSpeed, vertical * runSpeed);
    }
}
