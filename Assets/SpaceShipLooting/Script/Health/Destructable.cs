using UnityEngine;
using UnityEngine.Events;

public class Destructable : MonoBehaviour
{
    private Health health;

    // 오브젝트가 파괴되었을 때 발생하는 이벤트
    public UnityEvent<GameObject> OnObjectDestroyed { get; private set; } = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnPlayerDestroyed { get; private set; } = new UnityEvent<GameObject>();
    public UnityEvent<GameObject> OnBossDestroyed { get; private set; } = new UnityEvent<GameObject>();


    private void Start()
    {
        health = GetComponent<Health>();
        if (health == null)
        {
            Debug.LogError("Health 컴포넌트를 찾을 수 없습니다.");
            return;
        }

        // Health 이벤트 구독
        health.OnDamaged.AddListener(HandleDamaged);
        health.OnDie.AddListener(HandleDeath);

    }

    private void OnDestroy()
    {
        if (health != null)
        {
            // 이벤트 구독 해제
            health.OnDamaged?.RemoveListener(HandleDamaged);
            health.OnDie?.RemoveListener(HandleDeath);
        }
    }

    private void HandleDamaged(float damage)
    {
        // 데미지를 받을 때 발생하는 효과 처리 (VFX, 사운드 등)
        Debug.Log($"[Destructable] {gameObject.name}이(가) {damage}의 피해를 입었습니다.");
    }

    private void HandleDeath()
    {
        // 파괴 로직
        // OnObjectDestroyed?.Invoke(gameObject);
        // Debug.Log($"[Destructable] {gameObject.name}이(가) 파괴되었습니다.");

        if (gameObject.CompareTag("Enemy"))
        {
            Destroy(gameObject, 2f); // 적일 경우 2초 후 제거
        }
        else if (gameObject.CompareTag("Player"))
        {
            OnPlayerDestroyed?.Invoke(gameObject);
        }
        else if (gameObject.CompareTag("Core"))
        {
            CoreController coreController = gameObject.GetComponent<CoreController>();
            coreController.SetDestroyed(true);

            // SpaceBossController에서 관련 작업 중단 호출
            var boss = FindFirstObjectByType<SpaceBossController>();
            if (boss != null)
            {
                boss.NotifyCoreDestroyed(this.gameObject); // 새 메서드 호출
            }

            OnObjectDestroyed?.Invoke(gameObject);
            return;
        }
        else if (gameObject.CompareTag("Boss"))
        {
            Debug.Log("보스 죽음 신호 발생");
            AudioManager.Instance.Play("BossDestroy", false, 1f, 0.8f);
            OnBossDestroyed?.Invoke(gameObject);
        }
        else
        {
            Destroy(gameObject); // 기본 파괴 로직
        }
    }
}
