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
    public GameObject ManageScript;
    public GameObject refrencePos;

    void Start()
    {
        body = GetComponent<Rigidbody2D>();
        ManageScript = GameObject.FindGameObjectWithTag("GameManager");
    }

    void Update()
    {
        horizontal = Input.GetAxisRaw("Horizontal");

        OverheatBarImage = ManageScript.GetComponent<ManageGame>().OverheatBarImage;

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

    private void FixedUpdate()
    {
        refrencePos.transform.position = new Vector2(transform.position.x, -8);
        transform.position = Vector2.MoveTowards(transform.position, refrencePos.transform.position, returnSpeed * Time.deltaTime);
        body.velocity = new Vector2(horizontal * runSpeed, body.velocity.y);
    }
}
