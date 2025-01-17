using UnityEngine;

[System.Serializable]
// 추후 개선 필요
public class PlayerStatsData
{
    // public GameObject player;

    [Header("Scene Settings")]
    public int lastClearedScene = 0; // 마지막 클리어된 씬 번호

    [Header("Speed Settings")]
    public float walkingSpeed = 2.5f;   // 플레이어 기본 이동 속도
    public float runningSpeed = 5f;     // 플레이어 달리기 속도
    public float stealthSpeed = 2f;   // 플레이어 앉기 이동 속도

    [Header("Pistol Settings")]
    public float bulletDamage = 2;  // 플레이어 총기 공격력
    public float bulletlifeTime = 5f;   // 플레이어 총알 수명
    public float pistolBulletSpeed = 70;   //탄환 스피드
    public int maxAmmo = 7; // 총 보유 총알
    public int currentAmmo = 0; // 현재 장전된 탄창

    [Header("Knife Settings")]
    public float knifeDamage = 5;     // 칼 데미지

    [Header("Player State Settings")]
    public bool enableStealthMode = false; // 스텔스 모드 활성화 여부
    public bool enableRunningMode = false; // 러닝 모드 활성화 여부


    public void ResetStatData()
    {
        bulletDamage = 2f;
        maxAmmo = 7;
        knifeDamage = 5;
        currentAmmo = 0;
    }
    public void AddAmmo(int amount)
    {
        maxAmmo += amount;
    }

    public void UseAmmo(int amount)
    {
        maxAmmo -= amount;
    }

    // ToString override
    public override string ToString()
    {
        return $"Scene Settings:\n" +
               $"  Last Cleared Scene: {lastClearedScene}\n" +
               $"Speed Settings:\n" +
               $"  Walking Speed: {walkingSpeed}\n" +
               $"  Running Speed: {runningSpeed}\n" +
               $"  Stealth Speed: {stealthSpeed}\n" +
               $"Pistol Settings:\n" +
               $"  Bullet Damage: {bulletDamage}\n" +
               $"  Bullet Lifetime: {bulletlifeTime}\n" +
               $"  Pistol Bullet Speed: {pistolBulletSpeed}\n" +
               $"  Max Ammo: {maxAmmo}\n" +
               $"  Current Ammo: {currentAmmo}\n" +
               $"Knife Settings:\n" +
               $"  Knife Damage: {knifeDamage}\n" +
               $"Player State Settings:\n" +
               $"  Enable Stealth Mode: {enableStealthMode}\n" +
               $"  Enable Running Mode: {enableRunningMode}";
    }
}
