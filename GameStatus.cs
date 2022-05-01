using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
public class GameStatus : MonoBehaviour
{
    [Range(0.1f,1f)][SerializeField] float gameSpeed = 1f;
    [SerializeField] float currentScore = 0;
    [SerializeField] float PointsPerBlocksDestoryed = 83f;
    [SerializeField] TextMeshProUGUI scoreText;

    [SerializeField] bool isAutoPlayEnabled;
    private void Awake()
    {
        int gameStatusCount = FindObjectsOfType<GameStatus>().Length; //创建实例
        if (gameStatusCount > 1)
        {
            Destroy(gameObject);
        }
        else
        {
            DontDestroyOnLoad(gameObject); //不要销毁这个脚本的游戏对象
        }
    }
    void Start()
    {
        scoreText.text = currentScore.ToString();
    }
    void Update()
    {
        Time.timeScale = gameSpeed; //控制游戏速度
    }
    public void AddToScore() //Ball碰到砖块后调用
    {
        currentScore += PointsPerBlocksDestoryed;
        scoreText.text = currentScore.ToString();
    }
    public void ResetGame() //游戏结束按重新开始后
    {
        Destroy(gameObject);
    }
    public bool IsAutoBackEnabled() //是否自动玩游戏
    {
        return isAutoPlayEnabled;
    }
}
