using System.Collections;
using System.Reflection;
using HosseinSampleGame;
using NUnit.Framework;
using UnityEditor;
using UnityEditor.SceneManagement;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.TestTools;

public class AllScriptableObjectsTests
{
    [UnitySetUp]
    public IEnumerator SetUp()
    {
        //Arrange
        LoadSceneParameters sceneParameters = new LoadSceneParameters(LoadSceneMode.Single, LocalPhysicsMode.None);
        EditorSceneManager.LoadSceneAsyncInPlayMode("Assets/HosseinSampleGame/Tests/PlayModeTests/TestScenes/EmptyTestScene.unity", sceneParameters);

        yield return null;

        var waitForScene = new WaitForSceneLoaded("EmptyTestScene");
        yield return waitForScene;

        GameObject allScriptableObjectsPrefab = AssetDatabase.LoadAssetAtPath<GameObject>("Assets/HosseinSampleGame/Prefabs/AllScriptableObjects.prefab");
        MonoBehaviour.Instantiate(allScriptableObjectsPrefab).GetComponent<AllScriptableObjects>();
    }

    [UnityTest]
    public IEnumerator NotNullFields()
    {
        //Events
        Debug.Log("**********Events**********");
        foreach (FieldInfo field in AllScriptableObjects.Events.GetType().GetFields())
        {
            Assert.IsNotNull(field.GetValue(AllScriptableObjects.Events), "missing Event field: " + field.Name);
            Debug.Log(field.Name);
        }

        //Managers
        Debug.Log("**********Managers**********");
        foreach (FieldInfo field in AllScriptableObjects.Managers.GetType().GetFields())
        {
            Assert.IsNotNull(field.GetValue(AllScriptableObjects.Managers), "missing Managers field: " + field.Name);
            Debug.Log(field.Name);
        }

        //SharedData
        Debug.Log("**********SharedData**********");
        foreach (FieldInfo field in AllScriptableObjects.SharedData.GetType().GetFields())
        {
            Assert.IsNotNull(field.GetValue(AllScriptableObjects.SharedData), "missing SharedData field: " + field.Name);
            Debug.Log(field.Name);
        }

        yield return null;
    }
}
