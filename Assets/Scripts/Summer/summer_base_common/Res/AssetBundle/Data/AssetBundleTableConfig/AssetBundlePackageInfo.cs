﻿
//
//                            _ooOoo_
//                           o8888888o
//                           88" . "88
//                           (| -_- |)
//                           O\  =  /O
//                        ____/`---'\____
//                      .'  \\|     |//  `.
//                     /  \\|||  :  |||//  \
//                    /  _||||| -:- |||||-  \
//                    |   | \\\  -  /// |   |
//                    | \_|  ''\---/''  |   |
//                    \  .-\__  `-`  ___/-. /
//                  ___`. .'  /--.--\  `. . __
//               ."" '<  `.___\_<|>_/___.'  >'"".
//              | | :  `- \`.;`\ _ /`;.`/ - ` : | |
//              \  \ `-.   \_ __\ /__ _/   .-` /  /
//         ======`-.____`-.___\_____/___.-`____.-'======
//                            `=---='
//        ^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^^
//                 			 佛祖 保佑             


using UnityEngine;
using System.Collections.Generic;
using Object = UnityEngine.Object;
namespace Summer
{
    /// <summary>
    /// AssetBunlde包含的资源，以及引用
    /// </summary>
    public class AssetBundlePackageInfo
    {
        public string PackagePath { get { return _cnf.PackagePath; } }
        public AssetBundlePackageCnf _cnf;
        public AssetBundle _assetbundle;
        //TODO 不用Map用List的缘故是有可能出现重名但类型不一致的Object
        public List<AssetInfo> _assetMap = new List<AssetInfo>();
        public int RefCount { get; private set; }
#if LOG
        public List<string> _refList = new List<string>(8);
#endif
        public AssetBundlePackageInfo(AssetBundlePackageCnf cnf)
        {
            _cnf = cnf;
            RefCount = 0;
        }

        public bool InitAssetBundle(AssetBundle ab, Object[] objs)
        {
            ResLog.Assert(ab != null, "AssetBundlePackageInfo InitAssetBundle 初始化失败:ab为空");
            if (ab == null) return false;
            _assetbundle = ab;
            _assetMap.Clear();
            int length = objs.Length;
            for (int i = 0; i < length; i++)
            {
                AssetInfo info = new AssetInfo(objs[i], _cnf._resNames[objs[i].name]);
                _assetMap.Add(info);
            }
            return true;
        }

        public AssetInfo GetAsset<T>(string assetName) where T : Object
        {
            if (_assetbundle == null) return null;
            AssetInfo reAssetInfo = null;

            int length = _assetMap.Count;
            for (int i = 0; i < length; i++)
            {
                AssetInfo assetInfo = _assetMap[i];
                if (assetInfo.ResPath != assetName) continue;
                if (!assetInfo.CheckType<T>()) continue;
                reAssetInfo = _assetMap[i];
            }
            ResLog.Assert((reAssetInfo != null), "从资源主包[{0}]中找不到对应类型的AssetInfo:[{1}]的资源", _cnf.PackagePath, assetName);
            return reAssetInfo;
        }

        public void UnLoad()
        {
            ResLog.Log("[{0}]引用--,Ref:[{1}]", _cnf.PackagePath, RefCount);
            if (RefCount > 0) return;

            LogManager.Assert(_assetbundle != null, "AssetBundlePackageInfo UnLoad. AssetBundle不能为空:[{0}]", _cnf.PackagePath);
            if (_assetbundle == null) return;

            int length = _assetMap.Count;
            for (int i = 0; i < length; i++)
            {
                _assetMap[i].UnLoad();
            }
            _assetMap.Clear();
            _assetbundle.Unload(true);
            _assetbundle = null;
            _cnf = null;
        }

        public void RefParent(string parentPath)
        {
            if (string.IsNullOrEmpty(parentPath)) return;
#if LOG
            if (_refList.Contains(parentPath)) return;
            _refList.Add(parentPath);
#endif
            RefCount++;
        }

        public void UnRefParent(string parentPath)
        {
            if (string.IsNullOrEmpty(parentPath)) return;
#if LOG
            ResLog.Assert(_refList.Contains(parentPath), "AssetBundlePackageInfo UnRef 不包含这个引用[{0}]", parentPath);
            if (!_refList.Contains(parentPath)) return;
            _refList.Remove(parentPath);
#endif
            RefCount--;
        }
    }

    public class AbPackageFactory
    {
        public static AssetBundlePackageInfo Create(AssetBundlePackageCnf cnf)
        {
            AssetBundlePackageInfo info = new AssetBundlePackageInfo(cnf);
            return info;
        }

    }
}

