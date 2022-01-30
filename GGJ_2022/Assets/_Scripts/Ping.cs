using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class Ping : MonoBehaviour
{
    [SerializeField]
    AnimationCurve alphaCurve;

    SpriteRenderer r;
    Color col;

    float duration, timer;

    private void Start()
    {
        r = gameObject.GetComponent<SpriteRenderer>();
    }

    private void FixedUpdate()
    {
        if (timer > 0)
        {
            float a = alphaCurve.Evaluate(timer / duration);
            Color newCol = new Color(col.r, col.g, col.b, a);
            r.color = newCol;
            timer -= Time.fixedDeltaTime;
        }
    }

    public void StartPing(int playerColor, Sprite sprite, Vector2 pos, float duration)
    {
        r.sprite = sprite;
        Color col = playerColor == 0 ? Color.black : Color.white;
        r.color = col;
        gameObject.transform.position = pos;
        timer = duration;
        this.duration = duration;
    }
}
