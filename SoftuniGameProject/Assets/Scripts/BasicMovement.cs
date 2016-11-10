using UnityEngine;
using System.Collections;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.UI;

public class BasicMovement : MonoBehaviour {
    public GameObject GameManager;
    public GameObject PlayerBullet;
    public GameObject bulletPosition01;
    public GameObject bulletPosition02;
    public GameObject Explosion;

    public Text LivesUIText;

    const int MaxLives = 3;
    int lives;

    public float speed;

    public void Init()
    {
        lives = MaxLives;

        LivesUIText.text = lives.ToString();

        transform.position = new Vector2(0, 0);

        gameObject.SetActive(true);
    }
    	
	// Update is called once per frame
	void Update () {
        if (Input.GetKeyDown("space"))
        {
            GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
            bullet01.transform.position = bulletPosition01.transform.position;
            GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
            bullet02.transform.position = bulletPosition02.transform.position;
        }
#if UNITY_ANDROID
        // TO DO;
#endif
        float horizontalMovement = Input.GetAxis("Horizontal");
        float verticalMovement = Input.GetAxis("Vertical");

#if UNITY_ANDROID
        horizontalMovement = CrossPlatformInputManager.GetAxis("Horizontal");
        verticalMovement = CrossPlatformInputManager.GetAxis("Vertical");
#endif
        Vector2 direction = new Vector2(horizontalMovement, verticalMovement).normalized;

        Move(direction);
    }

    void Move(Vector2 direction)
    {
        Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));
        Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

        max.x = max.x - 0.225f;
        min.x = min.x + 0.225f;

        max.y = max.y - 0.285f;
        min.y = min.y + 0.285f;

        Vector2 currentPosition = transform.position;
        currentPosition += direction * speed * Time.deltaTime;

        currentPosition.x = Mathf.Clamp(currentPosition.x, min.x, max.x);
        currentPosition.y = Mathf.Clamp(currentPosition.y, min.y, max.y);

        transform.position = currentPosition;
    }

    public void Shoot()
    {
        GameObject bullet01 = (GameObject)Instantiate(PlayerBullet);
        bullet01.transform.position = bulletPosition01.transform.position;
        GameObject bullet02 = (GameObject)Instantiate(PlayerBullet);
        bullet02.transform.position = bulletPosition02.transform.position;
    }

    void OnTriggerEnter2D(Collider2D col)
    {
        if ((col.tag == "EnemyShipTag") || (col.tag == "EnemyBulletTag"))
        {
            PlayExplosion();

            lives--;
            LivesUIText.text = lives.ToString();
            if (lives ==0)
            {
                GameManager.GetComponent<GameManager>().SetGameManagerState(global::GameManager.GameManagerState.GameOver);

                gameObject.SetActive(false);
            }           
        }
    }
    void PlayExplosion()
    {
        GameObject explosion = (GameObject)Instantiate(Explosion);

        explosion.transform.position = transform.position;
    }
}
