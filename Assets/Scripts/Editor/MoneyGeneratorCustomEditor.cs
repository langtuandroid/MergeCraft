#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(MoneyGenerator))]
public class MoneyGeneratorCustomEditor : Editor
{
    private List<double> _blockLevelMoney = new List<double>();
    private Vector2 _scroll;

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        MoneyGenerator moneyGenerator = (MoneyGenerator)target;

        if (GUILayout.Button("Обновить отображаемый лист наград"))
        {
            _blockLevelMoney = moneyGenerator.GetBlocksLevelMoney();
        }

        _scroll = EditorGUILayout.BeginScrollView(_scroll, GUILayout.MaxHeight(300));

        for (int i = 0; i < _blockLevelMoney.Count; i++)
        {
            if (i > 0)
            {
                EditorGUILayout.BeginHorizontal("Награды за уровень");
                EditorGUILayout.LabelField("Награда за уровень " + i);
                EditorGUILayout.LabelField(_blockLevelMoney[i].ToString());
                EditorGUILayout.EndHorizontal();
            }
        }

        EditorGUILayout.EndScrollView();
    }
}
#endif