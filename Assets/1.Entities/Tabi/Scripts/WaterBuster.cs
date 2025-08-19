using UnityEngine;

public class WaterBuster : MonoBehaviour
{
    public Tabi tabi;
    public TabiController tabiCon;
    public GameObject bubblePrefab;

    private bool isCharging;
    private float chargeStartTime;
    void Update()
    {
        // 컨트롤러의 상태 변화를 주시하고 후처리합니다
        // 컨트롤러에 액션을 만들어 걸면 업데이트의 연산을 걷을 수 있으나 
        if (!isCharging && tabiCon.AttackBuffer)
        {
            isCharging = true;
            chargeStartTime = Time.time;
        }
        
        if (isCharging && !tabiCon.AttackBuffer)
        {
            isCharging = false;
            GameObject bubbleObj = bubblePrefab.PoolingGet();
            bubbleObj.transform.position = transform.position;
            bubbleObj.transform.eulerAngles = tabi.transform.eulerAngles;

            LinearMovement bubbleMovement = bubbleObj.GetComponent<LinearMovement>();
            bubbleMovement.Init(tabi.LookDirection, 30f);

        }
    }
}
