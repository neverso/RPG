﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Summer
{
    public interface I_EntityAction
    {
        void OnAction(BaseEntity entity, EventSetData param);
    }
}
