﻿#if UNITY_EDITOR
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Summer
{
    public class AssetDatabaseLoader : I_ResourceLoad
    {
        public static AssetDatabaseLoader instance = new AssetDatabaseLoader();
        public const string EVN = "Assets/";

        public List<LoadOpertion> _load_opertions                                  //加载的请求
         = new List<LoadOpertion>(32);
        public Dictionary<string, int> _loading =
            new Dictionary<string, int>();
        #region I_ResourceLoad

        public AssetInfo LoadAsset<T>(string path) where T : UnityEngine.Object
        {
            Object obj = AssetDatabase.LoadAssetAtPath<Object>(EVN + path);

            ResLog.Assert(obj != null, "AssetDatabaseLoader 加载失败:[{0}]", path);
            AssetInfo info = new AssetInfo(obj, path);
            return info;
        }

        public LoadOpertion LoadAssetAsync<T>(string path) where T : UnityEngine.Object
        {
            AssetDatabaseAsynLoadOpertion asyn_local = new AssetDatabaseAsynLoadOpertion(EVN + path);
            _load_opertions.Add(asyn_local);
            asyn_local.OnInit();
            _loading.Add(path, 1);
            return asyn_local;
        }

        public bool HasInLoading(string res_path)
        {
            return _loading.ContainsKey(res_path);
        }

        public bool UnloadAll()
        {
            return true;
        }

        public bool UnloadAssetBundle(AssetInfo asset_info)
        {
            Object obj = asset_info.GetAsset<Object>();
            if (obj == null) return false;
            Resources.UnloadAsset(obj);
            return true;
        }

        public void OnUpdate()
        {
            int length = _load_opertions.Count - 1;
            for (int i = length; i >= 0; i--)
            {
                if (_load_opertions[i].OnUpdate())
                {
                    RemoveRequest(_load_opertions[i]);

                }
            }
        }

        #endregion

        #region private

        protected void RemoveRequest(LoadOpertion opertion)
        {
            _load_opertions.Remove(opertion);
            _loading.Remove(opertion.RequestResPath);
        }

        #endregion
    }
}
#endif
