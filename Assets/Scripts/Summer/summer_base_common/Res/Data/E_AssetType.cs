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
                                             
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public enum E_AssetType
    {
        NONE = 0,
        SCRIPT,         // .cs
        SHADER,         // .shader or build-in shader with name
        FONT,           // .ttf
        TEXTURE,        // .png, .jpg
        MATERIAL,       // .mat
        ANIMATION,      // .anim
        CONTROLLER,     // .controller
        FBX,            // .fbx
        TEXTASSET,      // .txt, .bytes
        PREFAB,         // .prefab
        UNITY,          // .unity
        TESHU,
        MAX
    }
}