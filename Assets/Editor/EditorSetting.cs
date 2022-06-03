
using UnityEditor;

[CustomEditor(typeof(Event))]
public class EditorSetting : Editor
{
    private SerializedObject test;//���л�
    private SerializedProperty m_type, _notTriggerDialog, _triggerDialog, _triggerCondition, _taskMessage;
    void OnEnable()
    {
        test = new SerializedObject(target);
        m_type = test.FindProperty("eventType");//��ȡm_type
        _notTriggerDialog = test.FindProperty("_notTriggerDialog");
        _triggerDialog = test.FindProperty("_triggerDialog");
        _triggerCondition = test.FindProperty("_triggerCondition");
        _taskMessage = test.FindProperty("_taskMessage");
    }
    public override void OnInspectorGUI()
    {
        test.Update();//����test
        EditorGUILayout.PropertyField(m_type);
        if (m_type.enumValueIndex == 0)
        {//��ѡ���һ��ö������
            EditorGUILayout.PropertyField(_notTriggerDialog);
            EditorGUILayout.PropertyField(_triggerDialog);
            EditorGUILayout.PropertyField(_triggerCondition);
            EditorGUILayout.PropertyField(_taskMessage);
        }
        else if (m_type.enumValueIndex == 1)
        {
            EditorGUILayout.PropertyField(_notTriggerDialog);
            EditorGUILayout.PropertyField(_triggerDialog);
            EditorGUILayout.PropertyField(_triggerCondition);
        }
        //serializedObject.ApplyModifiedProperties();
        test.ApplyModifiedProperties();//Ӧ��
    }
}
