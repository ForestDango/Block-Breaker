using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Block : MonoBehaviour
{
    [SerializeField] AudioClip breakClip; //播放声音
    [SerializeField] GameObject impactVFX;
    Level level;

    [SerializeField] int hitsCount; //统计被击中几次

    [SerializeField] Sprite[]hitSprites; //分别是第一次被击中的Sprite，第二次，第三次
    private void Start()
    {
        CountBrealableBlocks();
    }

    private void CountBrealableBlocks()
    {
        level = FindObjectOfType<Level>();
        if (tag == "Breakable")
        {
            level.CountBreakBlocks();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (other.gameObject.CompareTag("Ball") && tag == "Breakable")
        {
            HandleHit();
        }
    }

    private void HandleHit()
    {
        hitsCount++;
        int maxHits = hitSprites.Length + 1;
        if (hitsCount >= maxHits)
        {
            DestoryBlock(); //如果被击中的次数大于它最大的承受击中次数就直接破坏
            //Debug.Log(other.gameObject.name);
        }
        else
        {
            ShowNextHitSPrite(); //显示
        }
    }

    private void ShowNextHitSPrite() //显示下一张图片也就是击碎的Sprite
    {
        int spriteIndex = hitsCount - 1;
        if (hitSprites[spriteIndex] != null)
        {
            GetComponent<SpriteRenderer>().sprite = hitSprites[spriteIndex];
        }
        else
        {
            Debug.LogError("Array out of Index");
        }
    }
    private void DestoryBlock()
    {
        FindObjectOfType<GameStatus>().AddToScore();
        AudioSource.PlayClipAtPoint(breakClip, Camera.main.transform.position);
        Destroy(gameObject);
        level.BlockDestory();
        TriggerSparklesVFX();
    }
    private void TriggerSparklesVFX()
    {
        GameObject sparkles = Instantiate(impactVFX, transform.position.normalized, transform.rotation);
        Destroy(sparkles,1f);
    }
}
