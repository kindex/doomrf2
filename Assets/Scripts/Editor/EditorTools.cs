using System.IO;
using UnityEngine;
using UnityEditor;

public static class EditorTools
{
    [MenuItem("MadSword/Create Prefab From Sprite")]
    public static void MakePicturePrefabs()
    {
        foreach (var go in Selection.objects)
        {
            string path = AssetDatabase.GetAssetPath(go);

            TextureImporter ti = TextureImporter.GetAtPath(path) as TextureImporter;
            ti.filterMode = FilterMode.Point;
            AssetDatabase.ImportAsset(path, ImportAssetOptions.ForceUpdate);

            Sprite s = AssetDatabase.LoadAssetAtPath(path, typeof(Sprite)) as Sprite;


            string newTexPath = Path.Combine(Path.GetDirectoryName(path), "Textures");
            Directory.CreateDirectory(newTexPath);
            AssetDatabase.Refresh();

            Object prefab = PrefabUtility.CreateEmptyPrefab(path.ToLower().Replace(".png", "").Replace(".bmp", "") + ".prefab");

            GameObject tempObj = new GameObject(s.name);

            SpriteRenderer spriteRenderer = tempObj.AddComponent<SpriteRenderer>();
            spriteRenderer.sprite = s;

            tempObj.AddComponent<BoxCollider2D>();

            PrefabUtility.ReplacePrefab(tempObj, prefab, ReplacePrefabOptions.ConnectToPrefab);

            var newPath = Path.Combine(newTexPath, Path.GetFileName(path));
            AssetDatabase.MoveAsset(path, newPath);

            Object.DestroyImmediate(tempObj);

            AssetDatabase.Refresh();
        }
    }
}
