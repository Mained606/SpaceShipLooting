using UnityEngine;
using TMPro;
using UnityEngine.Events;
using NUnit.Framework;
using UnityEngine.UI;
using UnityEngine.XR.Interaction.Toolkit.Inputs.Readers;
using static NUnit.Framework.Constraints.NUnitEqualityComparer;

public class KeyPadUI : MonoBehaviour, ISignal
{
    [SerializeField]
    private string correctCode = "1945"; // 정답 코드
    private string currentInput = ""; // 현재 입력된 값
    private int failCount = 0; // 실패 횟수 추적 변수

    [SerializeField] private TMP_Text displayText; // 입력값을 보여줄 TextMeshProUGUI

    [SerializeField] private Transform keyPad;

    // 키패드 배열로 받아오기
    private Button[] keyPads;

    public UnityEvent<string> OnSignal { get; } = new UnityEvent<string>();
    public UnityEvent FailSignal { get; } = new UnityEvent(); // 3번 실패 시그널

    private void Start()
    {
        // 키패드 배열로 받아오기
        keyPads = keyPad.GetComponentsInChildren<Button>();
        for(int i = 0; i < keyPads.Length; i++)
        {
            int index = i + 1;
            keyPads[i].onClick.AddListener(() => OnButtonPressed(index));
        }
        

        if (displayText == null)
        {
            Debug.LogError("No TextMeshProUGUI found in children.");
        }

    }

    public void OnButtonPressed(int number)
    {
        if (currentInput.Length >= 4) return; // 4자리 입력 제한

        currentInput += number;

        // 디스플레이 업데이트
        if (displayText != null)
        {
            displayText.text = currentInput;
        }

        // 입력값이 4자리일 경우 코루틴 실행
        if (currentInput.Length == 4)
        {
            StartCoroutine(DelayedResultCheck()); // 코루틴 호출
        }
    }

    // 성공/실패 여부를 딜레이 후 확인하는 코루틴
    private System.Collections.IEnumerator DelayedResultCheck()
    {
        float originalFontSize = displayText.fontSize; // 원래 폰트 크기 저장

        yield return new WaitForSeconds(1f); // 1초 대기 후 "체킹 중..." 표시

        if (displayText != null)
        {
            displayText.fontSize = originalFontSize * 0.5f; // 폰트 크기를 80%로 줄임
            displayText.text = "Checking";
        }

        yield return new WaitForSeconds(3f); // 추가 2초 대기

        if (currentInput == correctCode)
        {
            displayText.fontSize = originalFontSize * 0.6f;
            displayText.text = "SUCCESS!";
        }
        else
        {
            displayText.text = "FAIL!";
            failCount++; // 실패 횟수 증가
        }
        // 실패 횟수가 3번이면 실패 신호 발행
        if (failCount >= 3)
        {
            FailSignal?.Invoke();
            failCount = 0; // 실패 횟수 초기화
        }

        displayText.fontSize = originalFontSize; // 폰트 크기를 원래대로 복원
        Invoke(nameof(ClearInput), 2f); // 2초 후 입력 초기화
    }

    private void ClearInput()
    {
        currentInput = "";
        if (displayText != null)
        {
            displayText.text = "";
        }
    }

    public GameObject GetGameObject()
    {
        return gameObject;
    }

    public void ClearListeners()
    {
        OnSignal.RemoveAllListeners();
    }
}
