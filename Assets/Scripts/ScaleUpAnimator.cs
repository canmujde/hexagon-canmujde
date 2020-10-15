using UnityEngine;

public class ScaleUpAnimator : MonoBehaviour
{
    private bool isInit;
    private float scaleSpeed;
    private Vector2 initScale = Vector2.zero;
    private Vector2 targetScale = Vector2.one;

    private void Start()
    {
        Init();
    }
    private void FixedUpdate()
    {
        if (!isInit) return;
        ScaleUp();

    }
    private void Init()
    {
        isInit = true;
        transform.localScale = initScale;
        scaleSpeed = Random.Range(1.5f, 2.4f);
    }

    private void ScaleUp()
    {


        transform.localScale = Vector2.Lerp(transform.localScale, targetScale, Time.fixedDeltaTime * scaleSpeed);
        if (transform.localScale.x >= targetScale.x - 0.01f)
        {
            transform.localScale = targetScale;
            isInit = false;
        }
    }
}
