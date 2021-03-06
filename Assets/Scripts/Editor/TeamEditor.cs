using System.Reflection;
using UnityEditor;
using System.Linq;

[CustomEditor(typeof(Team))]
public class TeamEditor : Editor
{
    private SerializedProperty teamType;
    private SerializedProperty goalType;
    private SerializedProperty pilotedType;
    private SerializedProperty goalPoint;

    int teamIndex = 0;
    int goalIndex = 0;
    int pilotedIndex = 0;

    private string[] brainTypes;

    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        System.Type[] types = Assembly.GetAssembly(typeof(PlayerBrain)).GetTypes();
        System.Type[] possible = (from System.Type type in types where type.IsSubclassOf(typeof(PlayerBrain)) select type).ToArray();

        brainTypes = possible.Select(type => type.Name).ToArray();
            
        teamType = serializedObject.FindProperty("ateamBrainType");
        goalType = serializedObject.FindProperty("agoalBrainType");
        pilotedType = serializedObject.FindProperty("aPilotedBrainType");

        for (int i = 0; i < brainTypes.Length; ++i)
        {
            if (brainTypes[i] == teamType.stringValue)
                teamIndex = i;

            if (brainTypes[i] == goalType.stringValue)
                goalIndex = i;

            if (brainTypes[i] == pilotedType.stringValue)
                pilotedIndex = i;
        }

        serializedObject.Update();

        teamIndex = EditorGUILayout.Popup("Team Brain Type", teamIndex, brainTypes);
        goalIndex = EditorGUILayout.Popup("Goal Brain Type", goalIndex, brainTypes);
        pilotedIndex = EditorGUILayout.Popup("Piloted Brain Type", pilotedIndex, brainTypes);

        teamType.stringValue = brainTypes[teamIndex];
        goalType.stringValue = brainTypes[goalIndex];
        pilotedType.stringValue = brainTypes[pilotedIndex];

        serializedObject.ApplyModifiedProperties();
    }
}
