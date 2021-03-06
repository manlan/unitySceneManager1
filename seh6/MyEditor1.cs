using UnityEngine;
using System.Collections;
using UnityEditor;
using System.Collections.Generic;
using System.Xml;
using System.IO;
using System.Text;
using LitJson;
public class MyEditor1 : Editor
{
    //将所有游戏场景导出为JSON格式
    [MenuItem("Tools/ExportJSON1")]

    static void ExportJSON()
    {
        Debug.Log(" ExportJSON Editor1... ");

        string filepath = Application.dataPath + @"/Scripts/jsonchild1.txt";
        FileInfo t = new FileInfo(filepath);
        if (!File.Exists(filepath))
        {
            File.Delete(filepath);
        }
        StreamWriter sw = t.CreateText();
        StringBuilder sb = new StringBuilder();
        JsonWriter writer = new JsonWriter(sb);

        writer.WriteObjectStart();
        writer.WritePropertyName("GameObjects");
        writer.WriteArrayStart();

        foreach (UnityEditor.EditorBuildSettingsScene S in UnityEditor.EditorBuildSettings.scenes)
        {
            // 
            Debug.Log(S.path);

            // 测试战斗的
            if (!S.path.Equals("Assets/Scenes/TestFight.unity"))
            {
                continue;
            }

            //if (!S.path.Equals("Assets/Scenes/Game.unity"))
            //{
            //    continue;
            //}

            if (S.enabled)
            {
                string name = S.path;
                EditorApplication.OpenScene(name);
                writer.WriteObjectStart();
                writer.WritePropertyName("scenes");
                writer.WriteArrayStart();

                writer.WriteObjectStart();
                writer.WritePropertyName("name");
                writer.Write(name);
                writer.WritePropertyName("gameObject");
                writer.WriteArrayStart();

                foreach (GameObject obj in Object.FindObjectsOfType(typeof(GameObject)))
                {

                    if (obj.transform.parent == null)
                    {
                        writer.WriteObjectStart();
                        writer.WritePropertyName("name");
                        writer.Write(obj.name);

                        writer.WritePropertyName("position");
                        writer.WriteArrayStart();
                        writer.WriteObjectStart();
                        writer.WritePropertyName("x");
                        writer.Write(obj.transform.position.x.ToString("F2"));
                        writer.WritePropertyName("y");
                        writer.Write(obj.transform.position.y.ToString("F2"));
                        writer.WritePropertyName("z");
                        writer.Write(obj.transform.position.z.ToString("F2"));
                        writer.WriteObjectEnd();
                        writer.WriteArrayEnd();

                        writer.WritePropertyName("rotation");
                        writer.WriteArrayStart();
                        writer.WriteObjectStart();
                        writer.WritePropertyName("x");
                        writer.Write(obj.transform.rotation.eulerAngles.x.ToString("F2"));
                        writer.WritePropertyName("y");
                        writer.Write(obj.transform.rotation.eulerAngles.y.ToString("F2"));
                        writer.WritePropertyName("z");
                        writer.Write(obj.transform.rotation.eulerAngles.z.ToString("F2"));
                        writer.WriteObjectEnd();
                        writer.WriteArrayEnd();

                        writer.WritePropertyName("scale");
                        writer.WriteArrayStart();
                        writer.WriteObjectStart();
                        writer.WritePropertyName("x");
                        writer.Write(obj.transform.localScale.x.ToString("F2"));
                        writer.WritePropertyName("y");
                        writer.Write(obj.transform.localScale.y.ToString("F2"));
                        writer.WritePropertyName("z");
                        writer.Write(obj.transform.localScale.z.ToString("F2"));
                        writer.WriteObjectEnd();
                        writer.WriteArrayEnd();

                        if (obj.transform.childCount > 0)
                        {
                            int childCount = obj.transform.childCount;
                            Debug.Log(" obj:name = " + obj.name + " childCount: " + childCount);

                            // Childrens:begin
                            writer.WritePropertyName("Childrens");
                            // Childrens array: begin
                            writer.WriteArrayStart(); 
                            writer.WriteObjectStart();

                            // 1 children list :being
                            //writer.WritePropertyName("ChildrenList");
                            //writer.WriteArrayStart();
                            //writer.WriteObjectStart();

                            // 有子类:
                            for (int k = 0; k < childCount; ++k)
                            {
                                // 2 children list :being
                                writer.WritePropertyName("ChildrenList");
                                writer.WriteArrayStart();
                                writer.WriteObjectStart();

                                writer.WritePropertyName("childName");
                                writer.Write(obj.transform.GetChild(k).name);

                                // position:begin
                                writer.WritePropertyName("position");
                                writer.WriteArrayStart();
                                writer.WriteObjectStart();

                                writer.WritePropertyName("x");
                                writer.Write(obj.transform.position.x.ToString("F2"));
                                writer.WritePropertyName("y");
                                writer.Write(obj.transform.position.y.ToString("F2"));
                                writer.WritePropertyName("z");
                                writer.Write(obj.transform.position.z.ToString("F2"));

                                // position:end
                                writer.WriteObjectEnd();

                                writer.WriteArrayEnd();
                                //// 

                                writer.WritePropertyName("rotation");
                                writer.WriteArrayStart();
                                writer.WriteObjectStart();
                                writer.WritePropertyName("x");
                                writer.Write(obj.transform.rotation.eulerAngles.x.ToString("F2"));
                                writer.WritePropertyName("y");
                                writer.Write(obj.transform.rotation.eulerAngles.y.ToString("F2"));
                                writer.WritePropertyName("z");
                                writer.Write(obj.transform.rotation.eulerAngles.z.ToString("F2"));
                                writer.WriteObjectEnd();
                                writer.WriteArrayEnd();

                                writer.WritePropertyName("scale");
                                writer.WriteArrayStart();
                                writer.WriteObjectStart();
                                writer.WritePropertyName("x");
                                writer.Write(obj.transform.localScale.x.ToString("F2"));
                                writer.WritePropertyName("y");
                                writer.Write(obj.transform.localScale.y.ToString("F2"));
                                writer.WritePropertyName("z");
                                writer.Write(obj.transform.localScale.z.ToString("F2"));
                                writer.WriteObjectEnd();
                                writer.WriteArrayEnd();

                                ////////
                                // 2 children list :being
                                writer.WriteObjectEnd();
                                writer.WriteArrayEnd();
                            }
                            // 1 children list :being
                            //writer.WriteObjectEnd();
                            //writer.WriteArrayEnd();

                            // Childrens:end
                            writer.WriteObjectEnd();
                            // Childrens array:end
                            writer.WriteArrayEnd();
                        }

                        writer.WriteObjectEnd();
                    }
                }

                writer.WriteArrayEnd();
                writer.WriteObjectEnd();
                writer.WriteArrayEnd();
                writer.WriteObjectEnd();
            }
        }
        writer.WriteArrayEnd();
        writer.WriteObjectEnd();


        sw.WriteLine(sb.ToString());
        sw.Close();
        sw.Dispose();
        AssetDatabase.Refresh();
    }

    public void savePosRotSca(JsonWriter writer, GameObject obj)
    {
        writer.WritePropertyName("position");
        writer.WriteArrayStart();
        writer.WriteObjectStart();
        writer.WritePropertyName("x");
        writer.Write(obj.transform.position.x.ToString("F2"));
        writer.WritePropertyName("y");
        writer.Write(obj.transform.position.y.ToString("F2"));
        writer.WritePropertyName("z");
        writer.Write(obj.transform.position.z.ToString("F2"));
        writer.WriteObjectEnd();
        writer.WriteArrayEnd();

        writer.WritePropertyName("rotation");
        writer.WriteArrayStart();
        writer.WriteObjectStart();
        writer.WritePropertyName("x");
        writer.Write(obj.transform.rotation.eulerAngles.x.ToString("F2"));
        writer.WritePropertyName("y");
        writer.Write(obj.transform.rotation.eulerAngles.y.ToString("F2"));
        writer.WritePropertyName("z");
        writer.Write(obj.transform.rotation.eulerAngles.z.ToString("F2"));
        writer.WriteObjectEnd();
        writer.WriteArrayEnd();

        writer.WritePropertyName("scale");
        writer.WriteArrayStart();
        writer.WriteObjectStart();
        writer.WritePropertyName("x");
        writer.Write(obj.transform.localScale.x.ToString("F2"));
        writer.WritePropertyName("y");
        writer.Write(obj.transform.localScale.y.ToString("F2"));
        writer.WritePropertyName("z");
        writer.Write(obj.transform.localScale.z.ToString("F2"));
        writer.WriteObjectEnd();
        writer.WriteArrayEnd();
    }
}
