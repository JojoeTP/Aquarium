using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

public class ShaderManager : MonoBehaviour
{
    public static ShaderManager inst;
    [Header("Maze")]
    public Material MazeMaterial;

    private void Awake() 
    {
        inst = this;
    }

    void Start() 
    {
        ResetShader();
    }

    public void SetMazeMaterial(int enable)
    {
        MazeMaterial.SetInt("_HideMainTex",enable);
    }

    public void ResetShader()
    {
        MazeMaterial.SetInt("_HideMainTex",0);
    }
}

#if UNITY_EDITOR
    [CustomEditor(typeof(ShaderManager))]
    public class ShaderTester : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            ShaderManager actionActive = (ShaderManager)target;

            if (GUILayout.Button("Reset Shader"))
            {
                actionActive.ResetShader();
            }
        }
    }
#endif
