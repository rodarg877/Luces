using UnityEngine;
using UnityEditor;

public class URPToBuiltInConverter : EditorWindow
{
    [MenuItem("Tools/Convert URP Materials to Built-in")]
    public static void ConvertMaterials()
    {
        string[] guids = AssetDatabase.FindAssets("t:Material");
        int count = 0;

        foreach (string guid in guids)
        {
            string path = AssetDatabase.GUIDToAssetPath(guid);
            Material mat = AssetDatabase.LoadAssetAtPath<Material>(path);

            if (mat != null && mat.shader != null)
            {
                string shaderName = mat.shader.name;

                // Reemplazos más comunes de URP a Built-in
                if (shaderName.Contains("Universal Render Pipeline/Lit"))
                {
                    mat.shader = Shader.Find("Standard");
                    count++;
                }
                else if (shaderName.Contains("Universal Render Pipeline/Simple Lit"))
                {
                    mat.shader = Shader.Find("Standard");
                    count++;
                }
                else if (shaderName.Contains("Universal Render Pipeline/Unlit"))
                {
                    mat.shader = Shader.Find("Unlit/Texture");
                    count++;
                }
                else if (shaderName.Contains("Universal Render Pipeline/Terrain/Lit"))
                {
                    mat.shader = Shader.Find("Nature/Terrain/Standard");
                    count++;
                }
            }
        }

        AssetDatabase.SaveAssets();
        Debug.Log($"✅ Conversión terminada. {count} materiales convertidos a Built-in.");
    }
}
