using UnityEngine;

public class Ball : MonoBehaviour
{
    [SerializeField] Paddle paddle1; //选择你要绑定的板子
    [SerializeField] float xPush = 2f;
    [SerializeField] float yPush = 15f;
    [SerializeField] bool hasStarted = false;

    AudioSource myAudioSource;
    [SerializeField] AudioClip[] ballSounds;

    [SerializeField] float randomFactor = 0.5f;

    Rigidbody2D myRigibody2D;
    Vector2 paddleToBallVector;

    void Start()
    {
        myRigibody2D = GetComponent<Rigidbody2D>();
        myAudioSource = GetComponent<AudioSource>();
        paddleToBallVector = transform.position - paddle1.transform.position; //获取球和板子的向量方向
    }

    void Update()
    {
        if (!hasStarted) //当游戏还没有开始时就每帧执行
        {
            LockBallToPaddle();
            LaunceOnMouseClick();
        }
    }
    private void LockBallToPaddle() //游戏刚开始要把球绑到板子中间上面随着板子移动
    {
        Vector2 paddlePos = new Vector2(paddle1.transform.position.x, paddle1.transform.position.y);
        transform.position = paddlePos + paddleToBallVector;
    }
    private void LaunceOnMouseClick()
    {
        if (Input.GetMouseButtonDown(0)) //通过点击鼠标左键发射，hasStart设置为true，并给它一个向上的力
        {
            hasStarted = true;
            GetComponent<Rigidbody2D>().velocity = new Vector2(xPush,yPush);
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        Vector2 velocityTwek = new Vector2(Random.Range(0, randomFactor), Random.Range(0, randomFactor));//随机的速度
        if (hasStarted)
        {
            AudioClip clip = ballSounds[Random.Range(0, ballSounds.Length)];
            myAudioSource.PlayOneShot(clip);
            myRigibody2D.velocity += velocityTwek; //为了防止球一直做减速移动，要在给它每次和砖块碰撞的时候添加一个随机速度
        }
    }
}
