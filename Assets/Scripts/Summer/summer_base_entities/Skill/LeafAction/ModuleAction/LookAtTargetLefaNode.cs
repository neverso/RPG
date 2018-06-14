﻿using UnityEngine;

namespace Summer
{
    /// <summary>
    /// 朝向目标
    /// </summary>
    public class LookAtTargetLefaNode : SkillLeafNode
    {
        public const string DES = "朝向目标";
        public bool every_frame;

        public GameObject _source;
        public GameObject _target;
        public Vector3 _look_at_pos;

        public bool debug = false;
        public Color debug_line_color = Color.yellow;
        public override void OnEnter(EntityBlackBoard blackboard)
        {
            LogEnter();
            DoLookAt();
            if (!every_frame)
            {
                Finish();
            }
        }

        public override void OnExit(EntityBlackBoard blackboard)
        {
            LogExit();
        }

        public override void OnUpdate(float dt, EntityBlackBoard blackboard)
        {
            //base.OnUpdate(dt);
            UpdateLookAtPosition();
            _source.transform.LookAt(_look_at_pos, Vector3.up);

            if (debug)
            {
                Debug.DrawLine(_source.transform.position, _look_at_pos, debug_line_color);
            }
        }

        public void DoLookAt()
        {
            if (debug)
            {
                //Debug.DrawLine(go.transform.position, lookAtPos, debugLineColor.Value);
            }
        }

        // 更新目标
        public void UpdateLookAtPosition()
        {
            _look_at_pos = _target.transform.position;
        }

        public override string ToDes() { return DES; }
    }
}

