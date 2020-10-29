using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public interface ICombatable
{
    int CardAttack { get; }
    int CardBarrier { get; }
    int CardHealth { get; }

    bool CanAttack { get; }
}
