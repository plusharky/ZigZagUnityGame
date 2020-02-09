using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
public class FPSInput : MonoBehaviour
{
    public Text StartGame; // Tap to start
    public Text Over; // Game Over
    public Text ScoreLabel; // счёт
    public GameObject Player; // игрок
    public float speed = 5f;// скорость игрока
    public float gravity = -2.0f; //  гравитация
    float deltaX = 0;
    float deltaZ = 0;
    int click = 0;
    private CharacterController _charController;
    // Start is called before the first frame update
    void Start()
    {
        Over.color = new Color(1, 1, 1, 0);
        _charController = GetComponent<CharacterController>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Player.transform.position.y < 0f)
        {
            Over.color = new Color(0, 0, 0, 255);
        }
        if (Player.transform.position.y < -6f) 
        {
            ScoreLabel.text = 0.ToString();
            click = 0;
            SceneManager.LoadScene("MainScene", LoadSceneMode.Single);
        }
        if (Input.GetMouseButtonDown(0))
        {
            click++;
        }
        if (click > 0)
        {
            StartGame.color = new Color(1, 1, 1, 0);
            if (click % 2 == 0)
            {
                deltaX = speed;
                deltaZ = 0;
            }
            else
            {
                deltaZ = speed;
                deltaX = 0;
            }
            Vector3 movement = new Vector3(deltaX, 0, deltaZ);
            movement = Vector3.ClampMagnitude(movement, speed);
            movement.y = gravity;
            movement *= Time.deltaTime;
            movement = transform.TransformDirection(movement);
            _charController.Move(movement);
        }
    }
}
