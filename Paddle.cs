using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Paddle : MonoBehaviour
{
    [SerializeField] float minX = 1F;
    [SerializeField] float maxX = 14F;
    [SerializeField] float screenWidthInUnits = 16f;

    GameStatus theGameSession; //管理UI更新（待会用上）
    Ball theBall;
    void Start()
    {
        theGameSession = FindObjectOfType<GameStatus>();
        theBall = FindObjectOfType<Ball>();
    }
    void Update()
    {       
        Vector2 paddlePos = new Vector2(transform.position.x,transform.position.y);
        paddlePos.x = Mathf.Clamp(GetXPos(), minX, maxX); //限制板子移动的X范围
        transform.position = paddlePos;        
    }

    private float GetXPos()
    {
        if (theGameSession.IsAutoBackEnabled()) //当系统AI自动操作时
        {
            return theBall.transform.position.x;
        }
        else
        {
            return Input.mousePosition.x / Screen.width * screenWidthInUnits;//相当于鼠标移动的x轴的坐标乘以整个游戏屏幕的宽度得到它的世界坐标
        }
    }
}
