/////////////////////////////////////////
/// Script is property of Ava Taylor. ///
/// All rights reserved. ////////////////
/////////////////////////////////////////

public interface ICombatable
{
    int BaseAttack { get; }
    int CardAttack { get; }

    int BaseBarrier { get; }
    int CardBarrier { get; }

    int BaseHealth { get; }
    int CardHealth { get; }

    bool CanAttack { get; }
}
