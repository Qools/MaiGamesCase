using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine.UI;
using DG.Tweening;

public class InGamePanel : UIPanel
{
    [SerializeField] private Color stageCompletedColor;

    public TextMeshProUGUI currentLevelText;
    public TextMeshProUGUI nextLevelText;

    [SerializeField] private Transform stagesParent;

    [SerializeField] private GameObject stageUIPrefab;
    private List<GameObject> stagesUI = new List<GameObject>();

    private int currentStage;

    public override void Open()
    {
        base.Open();

        currentLevelText.text = DataManager.Instance.GetLevel().ToString("00");
        nextLevelText.text = (DataManager.Instance.GetLevel() + 1).ToString("00");
    }

    public override void OnEnable()
    {
        base.OnEnable();

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

        DOVirtual.DelayedCall(0.1f, () =>
        {
            ClearStagesUI();
            FillStagesUI();
        });
    }

    private void ClearStagesUI()
    {
        foreach (var stage in stagesUI)
        {
            Destroy(stage);
        }

        stagesUI = new List<GameObject>();
    }

    private void FillStagesUI()
    {
        for (int i = 0; i < GameController.Instance.stages.Count; i++)
        {
            var temp = Instantiate(stageUIPrefab, stagesParent);

            stagesUI.Add(temp);
        }
    }

    private void OnStageExit()
    {
        if (stagesUI.Count > 0)
        {
            stagesUI[currentStage].GetComponent<Image>().color = stageCompletedColor;
        }

        currentStage++;
    }
}
