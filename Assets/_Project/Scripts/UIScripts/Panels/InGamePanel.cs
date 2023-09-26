using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.UI;

public class InGamePanel : UIPanel
{
    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    [SerializeField] private Transform stagesParent;

    [SerializeField] private GameObject stageUIPrefab;
    private List<GameObject> stagesUI;

    private int currentStage;

    public override void Open()
    {
        base.Open();

        currentLevelText.text = DataManager.Instance.GetLevel().ToString("00");
        nextLevelText.text = (DataManager.Instance.GetLevel() + 1).ToString("00");
    }

    public override void OnEnable()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        stagesUI = new List<GameObject>();

        currentStage = 0;

        EventSystem.OnNewLevelLoad += OnNewLevelLoad;
        EventSystem.OnStageExit += OnStageExit;
    }

    private void OnDisable()
    {
        EventSystem.OnNewLevelLoad -= OnNewLevelLoad;
        EventSystem.OnStageExit -= OnStageExit;
    }

    private void OnNewLevelLoad()
    {
        currentStage = 0;

        ClearStagesUI();
        FillStagesUI();
    }

    private void ClearStagesUI()
    {
        foreach (var stage in stagesUI)
        {
            Destroy(stage.gameObject);
        }

        stagesUI = new List<GameObject>();
    }

    private void FillStagesUI()
    {
        var _stages = FindObjectsOfType<StageController>();

        for (int i = 0; i < _stages.Length; i++)
        {
            var temp = Instantiate(stageUIPrefab, stagesParent);

            stagesUI.Add(temp);
        }
    }

    private void OnStageExit()
    {
        currentStage = 0;

        stagesUI[currentStage].GetComponent<Image>().color = Color.grey;

        currentStage++;
    }
}
